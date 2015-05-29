using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.AspNet.Hosting;
using System.Collections.Generic;

namespace LibFree.AspNet.Mvc.Bundle.Core.Bundles
{
	public sealed class JsBundle : Bundle
    {
		private IJsMinifier _jsMinifier;

		internal JsBundle(string virtualPath, IEnumerable<string> filePaths, IJsMinifier jsMinifier, IHostingEnvironment hostingEnvironment)
			: base(virtualPath, filePaths, hostingEnvironment)
		{
			_jsMinifier = jsMinifier;
        }

		protected override string Minify(string content)
		{
			return _jsMinifier.Minify(content);
        }
	}
}