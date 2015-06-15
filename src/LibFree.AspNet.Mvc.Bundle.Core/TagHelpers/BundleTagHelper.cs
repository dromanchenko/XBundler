﻿using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.Framework.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibFree.AspNet.Mvc.Bundle.Core.TagHelpers
{
	public abstract class BundleTagHelper : TagHelper
	{
		private ILoggerFactory _loggerFactory;
		private IBundleRuntime _bundleRuntime;

		[HtmlAttributeName("virtualPath")]
		public string VirtualPath { get; set; }

		[HtmlAttributeName("environments")]
		public string TargetEnvironments { get; set; }

		public BundleTagHelper(ILoggerFactory loggerFactory, IBundleRuntime bundleRuntime)
		{
			_loggerFactory = loggerFactory;
			_bundleRuntime = bundleRuntime;
        }

		protected abstract ILogger GetLogger(ILoggerFactory loggerFactory);
		protected abstract string GetLoggerMessagesPrefix();
		protected abstract IEnumerable<string> ParseHtml(string content);
		protected abstract BundleType GetBundleType();

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			var logger = GetLogger(_loggerFactory);
			var loggerMessagesPrefix = GetLoggerMessagesPrefix();
			logger.LogVerbose("{0}: running", loggerMessagesPrefix);

			Bundles.Bundle bundle;
			if (_bundleRuntime.Bundles.ContainsKey(VirtualPath))
			{
				bundle = _bundleRuntime.Bundles[VirtualPath];
			}
			else
			{
				var bundleType = GetBundleType();
				var childContent = await context.GetChildContentAsync();
				bundle = _bundleRuntime.CreateBundle(bundleType, VirtualPath, TargetEnvironments, ParseHtml(childContent.GetContent()), loggerMessagesPrefix);
			}

			output.SuppressOutput();

			if (bundle.ShouldBundle)
			{
				output.Content.SetContent(bundle.GetLHtmlTags());
			}
			else
			{
				var childContent = await context.GetChildContentAsync();
				output.Content.SetContent(childContent);
			}
		}
	}
}