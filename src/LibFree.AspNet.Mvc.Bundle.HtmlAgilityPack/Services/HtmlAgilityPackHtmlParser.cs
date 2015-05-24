using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace LibFree.AspNet.Mvc.Bundle.Services
{
	public sealed class HtmlAgilityPackHtmlParser : IHtmlParser
	{
		public IEnumerable<string> ParseCssBundle(string html)
		{
			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);
			var cssNodes = htmlDocument.DocumentNode.SelectNodes("/css");
			return cssNodes.Select(n => n.Attributes["href"].Value).ToArray();
		}

		public IEnumerable<string> ParseJsBundle(string html)
		{
			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);
			var jsNodes = htmlDocument.DocumentNode.SelectNodes("/script");
			return jsNodes.Select(n => n.Attributes["src"].Value).ToArray();
		}
	}
}