using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.Framework.DependencyInjection;

namespace LibFree.AspNet.Mvc.Bundle.HtmlParsers
{
	public static class IServiceCollectionExtensions
    {
		public static void UseHtmlAgilityPackParser(this IServiceCollection services)
		{
			services.AddSingleton<IHtmlParser, HtmlAgilityPackHtmlParser>();
		}
    }
}