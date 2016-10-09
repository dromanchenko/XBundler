using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

namespace LibFree.AspNet.Mvc.Bundle.Core.Bundles
{
	public sealed class JsBundle : Bundle
    {
		private IJsMinifier _jsMinifier;

		internal JsBundle(string virtualPath, IEnumerable<string> filePaths, IJsMinifier jsMinifier, IHostingEnvironment hostingEnvironment, IEnumerable<string> targetEnvironments = null)
			: base(virtualPath, filePaths, hostingEnvironment, targetEnvironments: targetEnvironments)
		{
			_jsMinifier = jsMinifier;
        }

		protected override string BuildHtmlLink(string path)
		{
			return string.Format("<script src='{0}'></script>", path);
		}

		protected override string Minify(string filePath, string content)
		{
			return filePath.EndsWith(".min.js") ? content : _jsMinifier.Minify(content);
        }
	}
}