using System.Collections.Generic;
using XBundler.Core.Bundles;

namespace XBundler.Core.Abstractions
{
	public interface IBundleRuntime
    {
		Bundle GetBundle(string virtualPath);
		Bundle CreateBundle(BundleType bundleType, string virtualPath, string targetEnvironments, IEnumerable<string> filePaths, string loggerMessagesPrefix);
		Bundle CreateBundle(BundleType bundleType, string virtualPath, IEnumerable<string> targetEnvironments, IEnumerable<string> filePaths, string loggerMessagesPrefix);
	}
}