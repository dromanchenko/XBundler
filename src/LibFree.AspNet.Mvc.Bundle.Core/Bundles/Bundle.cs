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
		private string _requestedVirtualPath;
		private string _generatedVirtualPath;

		private IEnumerable<string> _filePaths { get; set; }

		private string _content;
		private SemaphoreSlim _contentSyncLock = new SemaphoreSlim(1);

		private string _linkCache;
		private readonly object _linkCacheLockObject = new object();

		private IHostingEnvironment _hostingEnvironment;
		private IEnumerable<string> _targetEnvironments;

		internal bool ShouldBundle
		{
			get
			{
				return _targetEnvironments == null
					|| !_targetEnvironments.Any()
					|| _targetEnvironments.Contains(_hostingEnvironment.EnvironmentName);
			}
		}

		internal Bundle(string virtualPath, IEnumerable<string> filePaths, IHostingEnvironment hostingEnvironment, IEnumerable<string> targetEnvironments = null)
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

			_requestedVirtualPath = virtualPath;
			_filePaths = filePaths;
			_hostingEnvironment = hostingEnvironment;
			_targetEnvironments = targetEnvironments;

			_generatedVirtualPath = _requestedVirtualPath + "?v=" + Guid.NewGuid().ToString().Replace("-", string.Empty);
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

				combinedContent.Append(Minify(normalizedFilePath, fileContent));
			}

			return combinedContent.ToString();
		}

		protected abstract string Minify(string filePath, string content);
		protected abstract string BuildHtmlLink(string path);

		public string GetLHtmlTags()
		{
			if (_linkCache == null)
			{
				lock (_linkCacheLockObject)
				{
					if (_linkCache == null)
					{
						_linkCache = BuildHtmlLink(_generatedVirtualPath);
						/*if (_targetEnvironments == null)
						{
							_linkCache = BuildHtmlLink(_generatedVirtualPath);
                        }
						else
						{
							if (_targetEnvironments.Contains(_hostingEnvironment.EnvironmentName))
							{
								_linkCache = BuildHtmlLink(_generatedVirtualPath);
							}
							else
							{
								var stringBuilder = new StringBuilder();
								foreach (var filePath in _filePaths)
								{
									stringBuilder.Append(BuildHtmlLink(filePath)).Append("\r\n");
								}

								_linkCache = stringBuilder.ToString();
							}
						}*/
					}
				}
			}

			return _linkCache;
		}
	}
}