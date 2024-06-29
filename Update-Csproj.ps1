param (
    [string]$CsprojPath,
    [string]$DisplayVersion,
    [string]$VersionCode
)

[xml]$xml = Get-Content $CsprojPath

$namespace = @{ "msbuild" = "http://schemas.microsoft.com/developer/msbuild/2003" }

$applicationDisplayVersionNode = $xml.Project.PropertyGroup.ChildNodes | Where-Object { $_.LocalName -eq "ApplicationDisplayVersion" }
if (-not $applicationDisplayVersionNode) {
    $applicationDisplayVersionNode = $xml.CreateElement("ApplicationDisplayVersion", $namespace.msbuild)
    $xml.Project.PropertyGroup.AppendChild($applicationDisplayVersionNode) | Out-Null
}
$applicationDisplayVersionNode.InnerText = $DisplayVersion

$applicationVersionNode = $xml.Project.PropertyGroup.ChildNodes | Where-Object { $_.LocalName -eq "ApplicationVersion" }
if (-not $applicationVersionNode) {
    $applicationVersionNode = $xml.CreateElement("ApplicationVersion", $namespace.msbuild)
    $xml.Project.PropertyGroup.AppendChild($applicationVersionNode) | Out-Null
}
$applicationVersionNode.InnerText = $VersionCode

$xml.Save($CsprojPath)