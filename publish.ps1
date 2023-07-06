$csproj = gc .\src\BogoLib\BogoLib.csproj
$versionText = $csproj | % {
    if ($_.Contains("PackageVersion"))
    {
        $_
    }
}

$version = ""
$flag = 0
for ($i = 0; $i -lt $versionText.Length; $i++)
{
    $char = $versionText[$i]

    if ($flag -eq 1)
    {
        if ($char -eq "<")
        {
            break
        }

        $version += $char
    }

    if ($char -eq ">")
    {
        $flag = 1
    }
}

dotnet pack -c Release
$file = ".\src\BogoLib\bin\Release\BogoLib." + $version + ".nupkg"
cp $file BogoLib.nupkg

$key = gc .\.env

dotnet nuget push BogoLib.nupkg --api-key $key --source https://api.nuget.org/v3/index.json
rm .\BogoLib.nupkg