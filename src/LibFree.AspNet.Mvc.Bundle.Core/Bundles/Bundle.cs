using Microsoft.AspNet.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibFree.AspNet.Mvc.Bundle.Core.Bundles
{
	public abstract class Bundle
	{
		public string RequestedVirtualPath { get; private set; }
		public string GeneratedVirtualPath { get; private set; }

		protected IEnumerable<string> _filePaths { get; set; }

		private string _content;
		private SemaphoreSlim _contentSyncLock = new SemaphoreSlim(1);

		private IHostingEnvironment _hostingEnvironment;

		internal Bundle(string virtualPath, IEnumerable<string> filePaths, IHostingEnvironment hostingEnvironment)
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
			_hostingEnvironment = hostingEnvironment;

			GenerateVirtualPath();
		}

		public async Task<string> GetContent()
		{
			if (_content != null)
			{
				return _content;
			}
			else
			{
				if (_content != null)
				{
					return _content;
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
			var combinedContent = new StringBuilder();
			foreach (var filePath in _filePaths)
			{
				var normalizedFilePath = filePath;
				if (filePath[0] == '/' || filePath[0] == '\\')
				{
					normalizedFilePath = filePath.Remove(0, 1);
				}

				var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, normalizedFilePath);
				string fileContent;
				using (var fileStream = File.OpenRead(physicalPath))
				using (var streamReader = new StreamReader(fileStream))
				{
					fileContent = await streamReader.ReadToEndAsync();
				}

				combinedContent.Append(Minify(fileContent));
			}

			return combinedContent.ToString();
		}

		protected abstract string Minify(string content);

		private void GenerateVirtualPath()
		{
			GeneratedVirtualPath = RequestedVirtualPath + "?v=" + Guid.NewGuid().ToString().Replace("-", string.Empty);
		}
	}
}