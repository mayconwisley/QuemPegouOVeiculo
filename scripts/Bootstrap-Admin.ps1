param(
    [ValidateSet('Development', 'Production')]
    [string]$Environment = 'Development',

    [ValidateNotNullOrEmpty()]
    [string]$Username = 'admin'
)

$ErrorActionPreference = 'Stop'
$apiDirectory = (Resolve-Path (Join-Path $PSScriptRoot '..\Api\FleetManagement.Api')).Path
$previousEnvironment = [Environment]::GetEnvironmentVariable('ASPNETCORE_ENVIRONMENT', 'Process')
$securePassword = $null
$pointer = [IntPtr]::Zero

try {
    $securePassword = Read-Host 'Senha inicial (12 a 128 caracteres)' -AsSecureString
    if ($securePassword.Length -lt 12 -or $securePassword.Length -gt 128) {
        throw 'A senha deve ter entre 12 e 128 caracteres.'
    }

    $pointer = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($securePassword)
    $env:ASPNETCORE_ENVIRONMENT = $Environment
    $env:FLEET_BOOTSTRAP_USERNAME = $Username
    $env:FLEET_BOOTSTRAP_PASSWORD = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($pointer)

    Push-Location $apiDirectory
    try {
        & dotnet run --no-launch-profile -- --bootstrap-admin
        if ($LASTEXITCODE -ne 0) {
            throw "Falha ao criar o administrador inicial em $Environment."
        }
    }
    finally {
        Pop-Location
    }
}
finally {
    if ($pointer -ne [IntPtr]::Zero) {
        [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($pointer)
    }
    if ($null -ne $securePassword) {
        $securePassword.Dispose()
    }
    Remove-Item Env:FLEET_BOOTSTRAP_PASSWORD, Env:FLEET_BOOTSTRAP_USERNAME -ErrorAction SilentlyContinue
    if ($null -eq $previousEnvironment) {
        Remove-Item Env:ASPNETCORE_ENVIRONMENT -ErrorAction SilentlyContinue
    }
    else {
        $env:ASPNETCORE_ENVIRONMENT = $previousEnvironment
    }
}
