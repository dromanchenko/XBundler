using System.Collections.Generic;
using System.Threading;

namespace LibFree.AspNet.Mvc.Bundle.Core
{
	internal static class BundleRuntime
    {
		public static readonly SemaphoreSlim BundlesSyncLock = new SemaphoreSlim(1);

		public static Dictionary<string, Bundles.Bundle> Bundles { get; private set; }

		static BundleRuntime()
		{
			Bundles = new Dictionary<string, Bundles.Bundle>();
		}
	}
}