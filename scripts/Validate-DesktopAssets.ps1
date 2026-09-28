param(
    [string]$Configuration = 'Debug'
)

$ErrorActionPreference = 'Stop'
$workspace = Split-Path $PSScriptRoot -Parent
$desktop = Join-Path $workspace 'QuemPegouOVeiculo'
$output = Join-Path $desktop "bin/$Configuration"
$desktopAssembly = [Reflection.Assembly]::LoadFrom((Join-Path $output 'QuemPegouOVeiculo.exe'))
$modelsAssembly = [Reflection.Assembly]::LoadFrom(
    (Join-Path $output 'QuemPegouOVeiculo.Desktop.Models.dll'))
$resources = $desktopAssembly.GetManifestResourceNames()

$formResources = @(Get-ChildItem -LiteralPath $desktop -Recurse -Filter '*.resx' -File |
    Where-Object { $_.FullName -notlike '*\Properties\*' })
foreach ($file in $formResources) {
    $resource = "QuemPegouOVeiculo.$($file.BaseName).resources"
    if ($resources -notcontains $resource) {
        throw "Recurso do formulário não encontrado: $resource"
    }
}

$reportFiles = @(Get-ChildItem -LiteralPath (Join-Path $desktop 'Reports/Templates') -Filter '*.rdlc' -File)
$dataSources = @(Get-ChildItem -LiteralPath (Join-Path $desktop 'Properties/DataSources') -Filter '*.datasource' -File)

foreach ($file in $reportFiles) {
    $resource = "QuemPegouOVeiculo.Reports.Templates.$($file.Name)"
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
    if ($null -eq $modelsAssembly.GetType($typeName, $false)) {
        throw "Modelo do relatório não encontrado: $typeName ($($file.Name))"
    }
}

Write-Output "Recursos validados: $($formResources.Count) formularios/controles, $($reportFiles.Count) RDLC e $($dataSources.Count) fontes de dados."
