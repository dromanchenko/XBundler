using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibFree.AspNet.Mvc.Bundle
{
	internal abstract class Bundle
	{
		internal string RequestedVirtualPath { get; private set; }
		internal string GeneratedVirtualPath { get; private set; }

		protected IEnumerable<string> _filePaths { get; set; }

		private string _content;
		private SemaphoreSlim _contentSyncLock = new SemaphoreSlim(1);

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

		public async Task<string> GetContent()
		{
			if (_content != null)
			{
				return await Task.FromResult(_content);
			}
			else
			{
				if (_content != null)
				{
					return await Task.FromResult(_content);
				}
				else
				{
					await _contentSyncLock.WaitAsync();
					try
					{
						_content = await BuildContentAsync();
						return _content;
					}
					finally
					{
						_contentSyncLock.Release();
					}
				}
			}
		}

		protected virtual async Task<string> BuildContentAsync()
		{
			return await Task.FromResult<string>(null);
		}

		private void GenerateVirtualPath()
		{
			GeneratedVirtualPath = RequestedVirtualPath + "?v=" + Guid.NewGuid().ToString().Replace("-", string.Empty);
		}
	}
}