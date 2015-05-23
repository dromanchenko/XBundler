using System.Collections.Generic;
using System.Threading;

namespace LibFree.AspNet.Mvc.Bundle
{
	internal static class BundleRuntime
    {
		public static readonly SemaphoreSlim BundlesSyncLock = new SemaphoreSlim(1);

		public static Dictionary<string, Bundle> Bundles { get; private set; }

		static BundleRuntime()
		{
			Bundles = new Dictionary<string, Bundle>();
		}
	}
}