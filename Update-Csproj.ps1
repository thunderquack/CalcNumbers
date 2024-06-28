param (
    [string]$CsprojPath,
    [string]$DisplayVersion,
    [string]$VersionCode
)

[xml]$xml = Get-Content $CsprojPath
$namespace = @{ "msbuild" = "http://schemas.microsoft.com/developer/msbuild/2003" }

$xml.Project.PropertyGroup.ApplicationDisplayVersion = $DisplayVersion
$xml.Project.PropertyGroup.ApplicationVersion = $VersionCode

$xml.Save($CsprojPath)