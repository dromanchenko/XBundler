using System.Collections.Generic;

namespace LibFree.AspNet.Mvc.Bundle.Core.Middlewares
{
	public sealed class BundleDesc
    {
		public string VirtualPath { get; private set; }

		public IEnumerable<string> Environments { get; set; }

		public List<string> Files { get; private set; }

		public BundleDesc(string virtualPath, IEnumerable<string> environments = null)
		{
			VirtualPath = virtualPath;
			Environments = environments;
			Files = new List<string>();
		}

		public BundleDesc AddFile(string file)
		{
			Files.Add(file);
			return this;
		}
	}
}