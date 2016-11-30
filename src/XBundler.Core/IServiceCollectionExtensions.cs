using Microsoft.Extensions.DependencyInjection;
using XBundler.Core.Abstractions;

namespace XBundler.Core
{
	public static class IServiceCollectionExtensions
    {
		public static void AddBundle(this IServiceCollection services)
		{
			services.AddSingleton<IBundleRuntime, BundleRuntime>();
		}
    }
}