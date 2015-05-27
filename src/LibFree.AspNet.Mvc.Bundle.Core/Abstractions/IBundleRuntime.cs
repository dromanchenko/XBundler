using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LibFree.AspNet.Mvc.Bundle.Core.Abstractions
{
    internal interface IBundleRuntime
    {
		ReadOnlyDictionary<string, Bundles.Bundle> Bundles { get; }
		Bundles.Bundle GetOrCreateBundle(BundleType bundleType, string virtualPath, IEnumerable<string> filePaths, string loggerMessagesPrefix);
    }
}