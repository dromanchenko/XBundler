using LibFree.AspNet.Mvc.Bundle.Services;
using Microsoft.AspNet.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LibFree.AspNet.Mvc.Bundle
{
    internal sealed class CssBundle : Bundle
    {
		private ICssMinifier _cssMinifier;
		private IHostingEnvironment _hostingEnvironment;

		public CssBundle(string virtualPath, IEnumerable<string> filePaths, ICssMinifier cssMinifier, IHostingEnvironment hostingEnvironment)
			: base(virtualPath, filePaths)
		{
			_cssMinifier = cssMinifier;
			_hostingEnvironment = hostingEnvironment;
        }

		protected override async Task<string> BuildContentAsync()
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

				combinedContent.Append(_cssMinifier.Minify(fileContent));
			}

			return await Task.FromResult(combinedContent.ToString());
		}
	}
}