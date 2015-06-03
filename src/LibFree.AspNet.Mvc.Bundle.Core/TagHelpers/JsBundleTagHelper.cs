using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.Framework.Logging;
using System.Collections.Generic;

namespace LibFree.AspNet.Mvc.Bundle.Core.TagHelpers
{
	[TargetElement("jsbundle")]
	public sealed class JsBundleTagHelper : BundleTagHelper
	{
		private IHtmlParser _htmlParser;

		private static string _scriptCache;
		private static readonly object _scriptCacheLockObject = new object();

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
			string script;
			if (_scriptCache != null)
			{
				script = _scriptCache;
			}
			else
			{
				lock (_scriptCacheLockObject)
				{
					if (_scriptCache != null)
					{
						script = _scriptCache;
					}
					else
					{
						_scriptCache = string.Format("<script src='{0}'></script>", bundle.GeneratedVirtualPath);
						script = _scriptCache;
					}
				}
			}

			output.Content.SetContent(script);
        }

		protected override BundleType GetBundleType()
		{
			return BundleType.Js;
		}
	}
}