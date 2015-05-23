using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibFree.AspNet.Mvc.Bundle
{
	internal abstract class Bundle
	{
		internal string RequestedVirtualPath { get; private set; }
		internal string GeneratedVirtualPath { get; private set; }

		protected IEnumerable<string> _filePaths { get; set; }

		public string Content { get; protected set; }

		internal Bundle(string virtualPath, IEnumerable<string> filePaths)
		{
			if (virtualPath == null)
			{
				throw new NullReferenceException("virtualPath cannot be null");
			}

			if (virtualPath == string.Empty)
			{
				throw new ArgumentException("virtualPath cannot be empty");
			}

			if (filePaths == null)
			{
				throw new NullReferenceException("filePaths cannot be null");
			}

			if (!filePaths.Any())
			{
				throw new ArgumentException("filePaths cannot be empty");
			}

			RequestedVirtualPath = virtualPath;
			_filePaths = filePaths;

			GenerateVirtualPath();
		}

		internal virtual async Task BuildAsync()
		{
			await Task.Yield();
		}

		private void GenerateVirtualPath()
		{
			GeneratedVirtualPath = RequestedVirtualPath + "?v=" + Guid.NewGuid().ToString().Replace("-", string.Empty);
		}
	}
}