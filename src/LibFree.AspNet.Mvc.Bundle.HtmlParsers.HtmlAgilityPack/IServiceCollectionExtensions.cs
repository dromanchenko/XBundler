using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace LibFree.AspNet.Mvc.Bundle.HtmlParsers
{
	public static class IServiceCollectionExtensions
    {
		public static void AddHtmlAgilityPackParser(this IServiceCollection services)
		{
			services.AddSingleton<IHtmlParser, HtmlAgilityPackHtmlParser>();
		}
    }
}