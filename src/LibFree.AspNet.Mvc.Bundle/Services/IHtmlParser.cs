using System.Collections.Generic;

namespace LibFree.AspNet.Mvc.Bundle.Services
{
	public interface IHtmlParser
    {
		IEnumerable<string> ParseCssBundle(string html);
    }
}