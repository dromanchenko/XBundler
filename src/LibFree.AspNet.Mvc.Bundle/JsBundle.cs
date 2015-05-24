using LibFree.AspNet.Mvc.Bundle.Services;
using Microsoft.AspNet.Hosting;
using System.Collections.Generic;

namespace LibFree.AspNet.Mvc.Bundle
{
	internal sealed class JsBundle : Bundle
    {
		private IJsMinifier _jsMinifier;

		public JsBundle(string virtualPath, IEnumerable<string> filePaths, IJsMinifier jsMinifier, IHostingEnvironment hostingEnvironment)
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