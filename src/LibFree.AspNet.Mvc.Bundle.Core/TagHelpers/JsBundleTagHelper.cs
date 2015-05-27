using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.Framework.Logging;
using System.Collections.Generic;

namespace LibFree.AspNet.Mvc.Bundle.Core.TagHelpers
{
	[TargetElement("jsbundle")]
	internal sealed class JsBundleTagHelper : BundleTagHelper
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

		protected override void SetContent(TagHelperOutput output, Bundles.Bundle bundle)
		{
			output.Content.SetContent(string.Format("<script src='{0}'></script>", bundle.GeneratedVirtualPath));
        }

		protected override BundleType GetBundleType()
		{
			return BundleType.Js;
		}
	}
}