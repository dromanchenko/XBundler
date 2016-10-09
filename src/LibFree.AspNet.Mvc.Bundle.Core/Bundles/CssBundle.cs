using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

namespace LibFree.AspNet.Mvc.Bundle.Core.Bundles
{
	public sealed class CssBundle : Bundle
    {
		private ICssMinifier _cssMinifier;

		internal CssBundle(string virtualPath, IEnumerable<string> filePaths, ICssMinifier cssMinifier, IHostingEnvironment hostingEnvironment, IEnumerable<string> targetEnvironments = null)
			: base(virtualPath, filePaths, hostingEnvironment, targetEnvironments: targetEnvironments)
		{
			_cssMinifier = cssMinifier;
        }

		protected override string BuildHtmlLink(string path)
		{
			return string.Format("<link href='{0}' rel='stylesheet' type='text/css'/>", path);
		}

		protected override string Minify(string filePath, string content)
		{
			return filePath.EndsWith(".min.css") ? content : _cssMinifier.Minify(content);
        }
	}
}