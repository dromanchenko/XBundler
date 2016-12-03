using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using XBundler.Core.Abstractions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace XBundler.Core.TagHelpers
{
	[HtmlTargetElement("cssbundle")]
	public sealed class CssBundleTagHelper : BundleTagHelper
	{
		private static readonly Regex _regExp = new Regex("href\\s*=\\s*('|\")(?<path>.*?)('|\")");

		public CssBundleTagHelper(ILoggerFactory loggerFactory, IBundleRuntime bundleRuntime)
			: base(loggerFactory, bundleRuntime)
		{
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
			return BundleType.Css;
		}
	}
}