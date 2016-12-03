using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using XBundler.Core.Abstractions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace XBundler.Core.TagHelpers
{
	[HtmlTargetElement("jsbundle")]
	public sealed class JsBundleTagHelper : BundleTagHelper
	{
		private static readonly Regex _regExp = new Regex("src\\s*=\\s*('|\")(?<path>.*?)('|\")");

		public JsBundleTagHelper(ILoggerFactory loggerFactory, IBundleRuntime bundleRuntime)
			: base(loggerFactory, bundleRuntime)
		{
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
			var matches = _regExp.Matches(content);
			var paths = new List<string>(matches.Count);
			foreach (Match match in matches)
			{
				paths.Add(match.Groups["path"].Value);
			}
			return paths;
		}

		protected override BundleType GetBundleType()
		{
			return BundleType.Js;
		}
	}
}