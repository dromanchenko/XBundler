using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Yahoo.Yui.Compressor;

namespace LibFree.AspNet.Mvc.Bundle.Compressors
{
	public sealed class YUIJsCompressorMinifier : IJsMinifier
	{
		public string Minify(string js)
		{
			var jsComperssor = new JavaScriptCompressor();
			return jsComperssor.Compress(js);
		}
	}
}