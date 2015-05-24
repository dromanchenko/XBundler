# LibFree.AspNet.Mvc.Bundle
A library to bundle (minify/concat) css and js for Asp.Net Mvc 6

## Installation
In nuget console:

    Install-Package LibFree.AspNet.Mvc.Bundle -Pre
    Install-Package LibFree.AspNet.Mvc.Bundle.HtmlAgilityPack -Pre
    Install-Package LibFree.AspNet.Mvc.Bundle.YUICompressor -Pre

## Startup.cs

```c#

public void ConfigureServices(IServiceCollection services)
{
    ...
	services.UserYUICompressor();
	services.UserHtmlAgilityPackParser();
	...
}