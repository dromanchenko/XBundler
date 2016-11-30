using Microsoft.Extensions.DependencyInjection;
using XBundler.Core.Abstractions;

namespace XBundler.NUglify
{
	public static class IServiceCollectionExtensions
    {
		public static void AddNUglifyMinifier(this IServiceCollection services)
		{
			services.AddSingleton<IJsMinifier, NUglifyJsMinifier>();
			services.AddSingleton<ICssMinifier, NUglifyCssMinifier>();
		}
    }
}
