param (
    [string]$CsprojPath,
    [string]$DisplayVersion,
    [string]$VersionCode
)

Write-Host "VersionCode: $VersionCode"
Write-Host "DisplayVersion: $DisplayVersion"

[xml]$xml = Get-Content $CsprojPath
$namespace = @{ "msbuild" = "http://schemas.microsoft.com/developer/msbuild/2003" }

$xml.Project.PropertyGroup.ApplicationDisplayVersion = $DisplayVersion
$xml.Project.PropertyGroup.ApplicationVersion = $VersionCode

$xml.Save($CsprojPath)