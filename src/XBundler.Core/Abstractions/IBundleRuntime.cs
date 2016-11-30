using XBundler.Core.Bundles;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XBundler.Core.Abstractions
{
	public interface IBundleRuntime
    {
		ReadOnlyDictionary<string, Bundle> Bundles { get; }
		Bundle CreateBundle(BundleType bundleType, string virtualPath, string targetEnvironments, IEnumerable<string> filePaths, string loggerMessagesPrefix);
		Bundle CreateBundle(BundleType bundleType, string virtualPath, IEnumerable<string> targetEnvironments, IEnumerable<string> filePaths, string loggerMessagesPrefix);
	}
}