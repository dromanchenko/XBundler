using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.AspNet.Hosting;
using System.Collections.Generic;

namespace LibFree.AspNet.Mvc.Bundle.Core.Bundles
{
	internal sealed class CssBundle : Bundle
    {
		private ICssMinifier _cssMinifier;

		public CssBundle(string virtualPath, IEnumerable<string> filePaths, ICssMinifier cssMinifier, IHostingEnvironment hostingEnvironment)
			: base(virtualPath, filePaths, hostingEnvironment)
		{
			_cssMinifier = cssMinifier;
        }

		protected override string Minify(string content)
		{
			return _cssMinifier.Minify(content);
        }
	}
}