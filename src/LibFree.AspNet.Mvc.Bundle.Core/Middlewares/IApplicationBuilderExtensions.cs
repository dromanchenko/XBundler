using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.AspNet.Builder;

namespace LibFree.AspNet.Mvc.Bundle.Core.Middlewares
{
	public static class IApplicationBuilderExtensions
    {
		public static IApplicationBuilder UseBundle(this IApplicationBuilder app)
		{
			return app.UseMiddleware<BundleMiddleware>();
		}

		public static IApplicationBuilder AddCssBundle(this IApplicationBuilder app, BundleDesc bundleDesc)
		{
			AddBundle(app, BundleType.Css, bundleDesc);
			return app;
		}

		public static IApplicationBuilder AddJsBundle(this IApplicationBuilder app, BundleDesc bundleDesc)
		{
			AddBundle(app, BundleType.Js, bundleDesc);
			return app;
		}

		private static IApplicationBuilder AddBundle(this IApplicationBuilder app, BundleType bundleType, BundleDesc bundleDesc)
		{
			var bundleRuntime = (IBundleRuntime)app.ApplicationServices.GetService(typeof(IBundleRuntime));
			bundleRuntime.CreateBundle(bundleType, bundleDesc.VirtualPath, bundleDesc.Environments,  bundleDesc.Files, typeof(IApplicationBuilderExtensions).Name);
			return app;
		}
	}
}