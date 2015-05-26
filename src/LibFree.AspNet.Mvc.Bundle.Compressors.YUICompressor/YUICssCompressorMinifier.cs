using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Yahoo.Yui.Compressor;

namespace LibFree.AspNet.Mvc.Bundle.Compressors
{
	public sealed class YUICssCompressorMinifier : ICssMinifier
	{
		public string Minify(string css)
		{
			var cssComperssor = new CssCompressor();
			return cssComperssor.Compress(css);
		}
	}
}