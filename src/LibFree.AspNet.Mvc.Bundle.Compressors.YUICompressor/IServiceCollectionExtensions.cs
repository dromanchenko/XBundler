using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.Framework.DependencyInjection;

namespace LibFree.AspNet.Mvc.Bundle.Compressors
{
	public static class IServiceCollectionExtensions
    {
		public static void UseYUICompressor(this IServiceCollection services)
		{
			services.AddSingleton<ICssMinifier, YUICssCompressorMinifier>();
			services.AddSingleton<IJsMinifier, YUIJsCompressorMinifier>();
		}
    }
}
