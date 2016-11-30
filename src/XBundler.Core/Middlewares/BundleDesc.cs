using System.Collections.Generic;

namespace XBundler.Core.Middlewares
{
	public sealed class BundleDescription
    {
		public string VirtualPath { get; private set; }

		public IEnumerable<string> Environments { get; set; }

		public List<string> Files { get; private set; }

		public BundleDescription(string virtualPath, IEnumerable<string> environments = null)
		{
			VirtualPath = virtualPath;
			Environments = environments;
			Files = new List<string>();
		}

		public BundleDescription AddFile(string file)
		{
			Files.Add(file);
			return this;
		}
	}
}