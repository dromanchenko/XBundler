using Yahoo.Yui.Compressor;

namespace LibFree.AspNet.Mvc.Bundle.Services
{
	public sealed class YUIJsCompressorMinifier : IJsMinifier
	{
		public string Minify(string css)
		{
			var jsComperssor = new JavaScriptCompressor();
			return jsComperssor.Compress(css);
		}
	}
}