using LibFree.AspNet.Mvc.Bundle.Services;
using Microsoft.Framework.DependencyInjection;

namespace LibFree.AspNet.Mvc.Bundle.YUICompressor
{
	public static class IServiceCollectionExtensions
    {
		public static void UserYUICompressor(this IServiceCollection services)
		{
			services.AddTransient<ICssMinifier, YUICompressorMinifier>();
		}
    }
}
