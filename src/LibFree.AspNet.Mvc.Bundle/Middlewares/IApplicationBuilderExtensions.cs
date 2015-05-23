using Microsoft.AspNet.Builder;

namespace LibFree.AspNet.Mvc.Bundle.Middlewares
{
	public static class IApplicationBuilderExtensions
    {
		public static IApplicationBuilder UseBundle(this IApplicationBuilder app)
		{
			return app.UseMiddleware<BundleMiddleware>();
		}
	}
}