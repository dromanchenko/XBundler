Remove-Item artifacts\nuget\* -Recurse

$projectNames = "LibFree.AspNet.Mvc.Bundle.Core", "LibFree.AspNet.Mvc.Bundle.Compressors.YUICompressor", "LibFree.AspNet.Mvc.Bundle.HtmlParsers.HtmlAgilityPack"

foreach ($projectName in $projectNames)
{
	dnu build src\$projectName\ --configuration RELEASE --out artifacts\nuget\$projectName
	Rename-Item artifacts\nuget\$projectName\RELEASE lib
	Copy-Item src\$projectName\.nuget\nuget.nuspec artifacts\nuget\$projectName
	nuget pack artifacts\nuget\$projectName\nuget.nuspec -OutputDirectory artifacts\nuget\$projectName
}