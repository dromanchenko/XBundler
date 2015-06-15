# LibFree.AspNet.Mvc.Bundle
A library to bundle (minify/concat) css and js for Asp.Net Mvc 6

### Installation
In nuget console:

    Install-Package LibFree.AspNet.Mvc.Bundle.Core -Pre
    Install-Package LibFree.AspNet.Mvc.Bundle.HtmlParsers.HtmlAgilityPack -Pre
    Install-Package LibFree.AspNet.Mvc.Bundle.Compressors.YUICompressor -Pre

### Startup.cs

```csharp

using LibFree.AspNet.Mvc.Bundle.Compressors;
using LibFree.AspNet.Mvc.Bundle.Core;
using LibFree.AspNet.Mvc.Bundle.Core.Middlewares;
using LibFree.AspNet.Mvc.Bundle.HtmlParsers;
```

```csharp

public void ConfigureServices(IServiceCollection services)
{
    ...
	services.AddBundle();
    services.AddYUICompressor();
	services.AddHtmlAgilityPackParser();
	...
}
```

```csharp

public void Configure(IApplicationBuilder app)
{
	...
	app.UseBundle();
	...
}
```

if you want to use a bundle in multiple views you can confgiire it like followed:

```csharp

public void Configure(IApplicationBuilder app)
{
	...
	app.UseBundle()
		.AddCssBundle(new BundleDesc("/assets/css/", "Production")
			.AddFile("/assets/css/bootstrap.css")
			.AddFile("/assets/css/main.css"))
		.AddJsBundle(new BundleDesc("/assets/js/", "Production")
			.AddFile("/assets/js/jquery-2.1.3.js")
			.AddFile("/assets/js/bootstrap.js"));
	...
}
```

### _ViewImports.cshtml

```
...
@addTagHelper "*, LibFree.AspNet.Mvc.Bundle.Core"
...
```

### Views

```html
<cssbundle virtualpath="/assets/css/" environments="Production">
	<css href="/assets/css/bootstrap.css"></css>
	<css href="/assets/css/main.css"></css>
</cssbundle>

<jsbundle virtualpath="/assets/js/" environments="Production">
	<script src="/assets/js/jquery-2.1.3.js"></script>
	<script src="/assets/js/bootstrap.js"></script>
</jsbundle>
```

or just

```html
<cssbundle virtualpath="/assets/css/" />
<jsbundle virtualpath="/assets/js/" />
```

if you have configured the bundles in Startup.cs