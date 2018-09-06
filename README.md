# XBundler
A in-memory css and js bundler for Asp.Net Core

### Installation
In nuget console:

    Install-Package XBundler

### Startup.cs

```csharp

using XBundler.Core;
using XBundler.NUglify;
using XBundler.Middlewares;
```

```csharp

public void ConfigureServices(IServiceCollection services)
{
    ...
	services.AddBundle();
	services.AddNUglifyMinifier();
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

### _ViewImports.cshtml

```
...
@addTagHelper "*, XBundler.Core"
...
```

### Views

```html
<cssbundle virtualpath="/assets/css/" environments="Production">
	<link rel="stylesheet" href="/assets/css/bootstrap.css">
	<link rel="stylesheet" href="/assets/css/main.css">
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
