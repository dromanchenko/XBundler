using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using LibFree.AspNet.Mvc.Bundle.Core.Bundles;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.Framework.Logging;
using System.Threading.Tasks;

namespace LibFree.AspNet.Mvc.Bundle.Core.TagHelpers
{
	[TargetElement("jsbundle")]
	public class JsBundleTagHelper : BundleTagHelper
	{
		[Activate]
		[HtmlAttributeNotBound]
		public IJsMinifier JsMinifier { get; set; }

		protected override ILogger GetLogger()
		{
			return LoggerFactory.CreateLogger<JsBundleTagHelper>();
		}

		protected override string GetLoggerMessagesPrefix()
		{
			return "JsBundleTagHelper";
		}

		internal override async Task<Bundles.Bundle> CreateBundle(TagHelperContext context)
		{
			var jsTagsContent = await context.GetChildContentAsync();
			var filePaths = HtmlParser.ParseJsBundle(jsTagsContent.GetContent());
			return new JsBundle(VirtualPath, filePaths, JsMinifier, HostingEnvironment);
		}

		internal override void SetContent(TagHelperOutput output, Bundles.Bundle bundle)
		{
			output.Content.SetContent(string.Format("<script src='{0}'></script>", bundle.GeneratedVirtualPath));
        }
	}
}