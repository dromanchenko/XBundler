using NUglify;
using XBundler.Core.Abstractions;

namespace XBundler.NUglify
{
	public sealed class NUglifyJsMinifier : IJsMinifier
	{
		public string Minify(string js)
		{
			return Uglify.Js(js).Code;
		}
	}
}