using NUglify;
using XBundler.Core.Abstractions;

namespace XBundler.NUglify
{
	public sealed class NUglifyCssMinifier : ICssMinifier
	{
		public string Minify(string css)
		{
			return Uglify.Css(css).Code;
		}
	}
}