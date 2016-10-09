using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace LibFree.AspNet.Mvc.Bundle.Core.TagHelpers
{
	[HtmlTargetElement("cssbundle")]
	public sealed class CssBundleTagHelper : BundleTagHelper
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

		protected override BundleType GetBundleType()
		{
			return BundleType.Css;
		}
	}
}