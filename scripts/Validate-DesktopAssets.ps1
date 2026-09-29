param(
    [string]$Configuration = 'Debug'
)

$ErrorActionPreference = 'Stop'
$workspace = Split-Path $PSScriptRoot -Parent
$desktop = Join-Path $workspace 'FleetManagement'
$output = Join-Path $desktop "bin/$Configuration"
$desktopAssembly = [Reflection.Assembly]::LoadFrom((Join-Path $output 'FleetManagement.exe'))
$modelsAssembly = [Reflection.Assembly]::LoadFrom(
    (Join-Path $output 'FleetManagement.Desktop.Models.dll'))
$clientAssembly = [Reflection.Assembly]::LoadFrom(
    (Join-Path $output 'FleetManagement.Desktop.Client.dll'))
$resources = $desktopAssembly.GetManifestResourceNames()

$formResources = @(Get-ChildItem -LiteralPath $desktop -Recurse -Filter '*.resx' -File |
    Where-Object { $_.FullName -notlike '*\Properties\*' })
foreach ($file in $formResources) {
    $resource = "FleetManagement.$($file.BaseName).resources"
    if ($resources -notcontains $resource) {
        throw "Recurso do formulário não encontrado: $resource"
    }
}

$reportFiles = @(Get-ChildItem -LiteralPath (Join-Path $desktop 'Reports/Templates') -Filter '*.rdlc' -File)
$dataSources = @(Get-ChildItem -LiteralPath (Join-Path $desktop 'Properties/DataSources') -Filter '*.datasource' -File)

foreach ($file in $reportFiles) {
    $resource = "FleetManagement.Reports.Templates.$($file.Name)"
    if ($resources -notcontains $resource) {
        throw "RDLC incorporado não encontrado: $resource"
    }
    if (-not (Test-Path -LiteralPath (Join-Path $output "Reports/Templates/$($file.Name)"))) {
        throw "RDLC não copiado para a saída: $($file.Name)"
    }
}

foreach ($file in @($reportFiles) + @($dataSources)) {
    $document = [xml][IO.File]::ReadAllText($file.FullName)
    $node = $document.SelectSingleNode(
        "//*[local-name()='ObjectDataSourceType' or local-name()='TypeInfo']")
    if ($null -eq $node) {
        throw "Metadados do modelo ausentes: $($file.Name)"
    }

    $typeName = $node.InnerText.Split(',')[0].Trim()
    $modelType = $modelsAssembly.GetType($typeName, $false)
    if ($null -eq $modelType) {
        throw "Modelo do relatório não encontrado: $typeName ($($file.Name))"
    }

    if ($file.Extension -eq '.rdlc') {
        $fields = @($document.SelectNodes("//*[local-name()='DataSet']/*[local-name()='Fields']/*[local-name()='Field']"))
        $fieldNames = @($fields | ForEach-Object { $_.GetAttribute('Name') })
        foreach ($field in $fields) {
            $fieldName = $field.GetAttribute('Name')
            $dataField = $field.SelectSingleNode("*[local-name()='DataField']")
            if ($null -eq $modelType.GetProperty($fieldName) -or
                $null -eq $dataField -or $dataField.InnerText -ne $fieldName) {
                throw "Campo RDLC sem propriedade correspondente: $fieldName ($($file.Name))"
            }
        }

        $references = [regex]::Matches($document.OuterXml, 'Fields!([A-Za-z_][A-Za-z_0-9]*)\.Value')
        foreach ($reference in $references) {
            if ($fieldNames -notcontains $reference.Groups[1].Value) {
                throw "Expressão RDLC referencia campo ausente: $($reference.Groups[1].Value) ($($file.Name))"
            }
        }
    }
}

$mapperType = $clientAssembly.GetType('FleetManagement.Desktop.Client.Infrastructure.Api.LegacyTableMapper', $true)
$createTable = $mapperType.GetMethod('CreateTable', [Reflection.BindingFlags]'NonPublic, Static')
foreach ($binding in @(
    @{ File = 'Shared/Controls/DriverControl.Designer.cs'; Resource = 'drivers' },
    @{ File = 'Shared/Controls/VehicleControl.Designer.cs'; Resource = 'vehicles' },
    @{ File = 'Shared/Controls/VehicleDriverSelector.Designer.cs'; Resource = 'drivers' },
    @{ File = 'Shared/Controls/VehicleDriverSelector.Designer.cs'; Resource = 'vehicles' }
)) {
    $table = [Data.DataTable]$createTable.Invoke($null, @($binding.Resource))
    $source = [IO.File]::ReadAllText((Join-Path $desktop $binding.File))
    $prefix = if ($binding.Resource -eq 'drivers') { 'CbxDriver' } else { 'CbxVehicle' }
    foreach ($member in @('DisplayMember', 'ValueMember')) {
        $pattern = [regex]::Escape("this.$prefix.$member = `"") + '([^" ]+)"'
        $match = [regex]::Match($source, $pattern)
        if (-not $match.Success -or -not $table.Columns.Contains($match.Groups[1].Value)) {
            throw "Vínculo do seletor inválido: $prefix.$member ($($binding.File))"
        }
    }
}

Write-Output "Recursos validados: $($formResources.Count) formularios/controles, $($reportFiles.Count) RDLC, $($dataSources.Count) fontes de dados e 4 vinculos de seletores."
