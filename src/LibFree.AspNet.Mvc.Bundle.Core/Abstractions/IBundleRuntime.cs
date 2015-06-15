using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LibFree.AspNet.Mvc.Bundle.Core.Abstractions
{
	public interface IBundleRuntime
    {
		ReadOnlyDictionary<string, Bundles.Bundle> Bundles { get; }
		Bundles.Bundle CreateBundle(BundleType bundleType, string virtualPath, string targetEnvironments, IEnumerable<string> filePaths, string loggerMessagesPrefix);
		Bundles.Bundle CreateBundle(BundleType bundleType, string virtualPath, IEnumerable<string> targetEnvironments, IEnumerable<string> filePaths, string loggerMessagesPrefix);
	}
}