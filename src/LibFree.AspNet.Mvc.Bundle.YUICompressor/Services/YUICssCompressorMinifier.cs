using Yahoo.Yui.Compressor;

namespace LibFree.AspNet.Mvc.Bundle.Services
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