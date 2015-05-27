using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.Framework.Logging;
using System.Collections.Generic;

namespace LibFree.AspNet.Mvc.Bundle.Core.TagHelpers
{
	[TargetElement("cssbundle")]
	internal sealed class CssBundleTagHelper : BundleTagHelper
	{
		private IHtmlParser _htmlParser;

		public CssBundleTagHelper(IHtmlParser htmlParser, ILoggerFactory loggerFactory, IBundleRuntime bundleRuntime)
			: base(loggerFactory, bundleRuntime)
		{
			_htmlParser = htmlParser;
		}

		protected override ILogger GetLogger(ILoggerFactory loggerFactory)
		{
			return loggerFactory.CreateLogger<CssBundleTagHelper>();
		}

		protected override string GetLoggerMessagesPrefix()
		{
			return "CssBundleTagHelper";
		}

		protected override IEnumerable<string> ParseHtml(string content)
		{
			return _htmlParser.ParseCssBundle(content);
		}

		protected override void SetContent(TagHelperOutput output, Bundles.Bundle bundle)
		{
			output.Content.SetContent(string.Format("<link href='{0}' rel='stylesheet' type='text/css'/>", bundle.GeneratedVirtualPath));
		}

		protected override BundleType GetBundleType()
		{
			return BundleType.Css;
		}
	}
}