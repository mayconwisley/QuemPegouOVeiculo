param(
    [string[]]$Forms = @(
        'VehicleForm',
        'DriverForm',
        'VehicleMovementForm',
        'RefuelingForm',
        'FineForm',
        'MaintenanceForm',
        'VehicleStatusForm',
        'LicenseExpirationForm'
    ),
    [switch]$WaitForData,
    [int]$TimeoutMs = 5000
)

$ErrorActionPreference = 'Stop'
if ($Forms.Count -eq 1 -and $Forms[0].Contains(',')) {
    $Forms = $Forms[0].Split(',')
}
$outputDirectory = Join-Path (Split-Path -Parent $PSScriptRoot) 'FleetManagement\bin\Debug'
$applicationPath = Join-Path $outputDirectory 'FleetManagement.exe'
if (-not (Test-Path -LiteralPath $applicationPath)) {
    throw "Compile a solução em Debug antes de medir: $applicationPath"
}

Add-Type -AssemblyName System.Windows.Forms
$assembly = [System.Reflection.Assembly]::LoadFrom($applicationPath)
Add-Type -Path (Join-Path $outputDirectory 'FleetManagement.Desktop.Client.dll')
[System.Windows.Forms.Application]::EnableVisualStyles()
$username = $env:FLEET_MEASURE_USERNAME
$password = $env:FLEET_MEASURE_PASSWORD
if ([string]::IsNullOrWhiteSpace($username) -or [string]::IsNullOrWhiteSpace($password)) {
    throw 'Defina FLEET_MEASURE_USERNAME e FLEET_MEASURE_PASSWORD para medir telas autenticadas.'
}
[FleetManagement.Desktop.Client.Infrastructure.Api.AccessClient]::LoginAsync($username, $password).GetAwaiter().GetResult() | Out-Null

function Find-Grid([System.Windows.Forms.Control]$root) {
    foreach ($child in $root.Controls) {
        if ($child -is [System.Windows.Forms.DataGridView]) {
            return $child
        }
        $grid = Find-Grid $child
        if ($null -ne $grid) {
            return $grid
        }
    }
    return $null
}

foreach ($name in $Forms) {
    $type = $assembly.GetType("FleetManagement.$name", $true)
    $total = [System.Diagnostics.Stopwatch]::StartNew()
    $form = [System.Activator]::CreateInstance($type)
    $constructedMs = $total.ElapsedMilliseconds
    try {
        $form.ShowInTaskbar = $false
        $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
        $form.Location = [System.Drawing.Point]::new(-32000, -32000)
        $form.Show()
        [System.Windows.Forms.Application]::DoEvents()
        $openMs = $total.ElapsedMilliseconds
        $loadedMs = $null
        if ($WaitForData) {
            $grid = Find-Grid $form
            while ($null -ne $grid -and $null -eq $grid.DataSource -and $total.ElapsedMilliseconds -lt $TimeoutMs) {
                [System.Windows.Forms.Application]::DoEvents()
                Start-Sleep -Milliseconds 10
            }
            if ($null -ne $grid -and $null -ne $grid.DataSource) {
                $loadedMs = $total.ElapsedMilliseconds
            }
        }
        [pscustomobject]@{
            Form = $name
            ConstructorMs = $constructedMs
            OpenMs = $openMs
            DataLoadedMs = $loadedMs
        }
    }
    finally {
        $form.Close()
        $form.Dispose()
        $total.Stop()
    }
}
