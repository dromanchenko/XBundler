using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.AspNet.Hosting;
using System.Collections.Generic;

namespace LibFree.AspNet.Mvc.Bundle.Core.Bundles
{
	public sealed class CssBundle : Bundle
    {
		private ICssMinifier _cssMinifier;

		internal CssBundle(string virtualPath, IEnumerable<string> filePaths, ICssMinifier cssMinifier, IHostingEnvironment hostingEnvironment)
			: base(virtualPath, filePaths, hostingEnvironment)
		{
			_cssMinifier = cssMinifier;
        }

		protected override string Minify(string filePath, string content)
		{
			return filePath.EndsWith(".min.css") ? content : _cssMinifier.Minify(content);
        }
	}
}