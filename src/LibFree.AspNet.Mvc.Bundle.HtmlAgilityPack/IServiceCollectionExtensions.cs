using LibFree.AspNet.Mvc.Bundle.Services;
using Microsoft.Framework.DependencyInjection;

namespace LibFree.AspNet.Mvc.Bundle.HtmlAgilityPack
{
	public static class IServiceCollectionExtensions
    {
		public static void UserHtmlAgilityPackParser(this IServiceCollection services)
		{
			services.AddTransient<IHtmlParser, HtmlAgilityPackHtmlParser>();
		}
    }
}