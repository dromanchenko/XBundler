using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.AspNet.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace LibFree.AspNet.Mvc.Bundle.Core.TagHelpers
{
	[HtmlTargetElement("jsbundle")]
	public sealed class JsBundleTagHelper : BundleTagHelper
	{
		private IHtmlParser _htmlParser;

		public JsBundleTagHelper(IHtmlParser htmlParser, ILoggerFactory loggerFactory, IBundleRuntime bundleRuntime)
			: base(loggerFactory, bundleRuntime)
		{
			_htmlParser = htmlParser;
		}

		protected override ILogger GetLogger(ILoggerFactory loggerFactory)
		{
			return loggerFactory.CreateLogger<JsBundleTagHelper>();
		}

		protected override string GetLoggerMessagesPrefix()
		{
			return "JsBundleTagHelper";
		}

		protected override IEnumerable<string> ParseHtml(string content)
		{
			return _htmlParser.ParseJsBundle(content);
		}

		protected override BundleType GetBundleType()
		{
			return BundleType.Js;
		}
	}
}