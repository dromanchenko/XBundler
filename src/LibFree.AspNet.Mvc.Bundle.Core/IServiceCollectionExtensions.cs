using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace LibFree.AspNet.Mvc.Bundle.Core
{
	public static class IServiceCollectionExtensions
    {
		public static void AddBundle(this IServiceCollection services)
		{
			services.AddSingleton<IBundleRuntime, BundleRuntime>();
		}
    }
}