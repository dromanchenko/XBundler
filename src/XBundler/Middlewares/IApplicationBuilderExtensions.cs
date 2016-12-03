using Microsoft.AspNetCore.Builder;
using XBundler.Core;
using XBundler.Core.Abstractions;
using XBundler.Core.Middlewares;

namespace XBundler.Middlewares
{
	public static class IApplicationBuilderExtensions
    {
		public static IApplicationBuilder UseBundle(this IApplicationBuilder app)
		{
			return app.UseMiddleware<BundleMiddleware>();
		}

		public static IApplicationBuilder AddCssBundle(this IApplicationBuilder app, BundleDescription bundleDescription)
		{
			AddBundle(app, BundleType.Css, bundleDescription);
			return app;
		}

		public static IApplicationBuilder AddJsBundle(this IApplicationBuilder app, BundleDescription bundleDescription)
		{
			AddBundle(app, BundleType.Js, bundleDescription);
			return app;
		}

		private static IApplicationBuilder AddBundle(this IApplicationBuilder app, BundleType bundleType, BundleDescription bundleDescription)
		{
			var bundleRuntime = (IBundleRuntime)app.ApplicationServices.GetService(typeof(IBundleRuntime));
			bundleRuntime.CreateBundle(bundleType, bundleDescription.VirtualPath, bundleDescription.Environments, bundleDescription.Files, typeof(IApplicationBuilderExtensions).Name);
			return app;
		}
	}
}