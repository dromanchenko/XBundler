using System.Collections.Generic;

namespace LibFree.AspNet.Mvc.Bundle.Core.Abstractions
{
	public interface IHtmlParser
    {
		IEnumerable<string> ParseCssBundle(string html);
		IEnumerable<string> ParseJsBundle(string html);
	}
}