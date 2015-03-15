using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace WeddingDressesWebsite.Controllers
{	
	public class DataController : ApiController
	{
		private const string imageExtension = ".jpg";

		[Route("data/main-page-images")]
		public IEnumerable<string> GetImagesForMainPage()
		{
			const string urlPrefix = "/content/images/main/";
            const string mainPageImagesPath = "~/Content/Images/Main";

			var fileUrls = Directory.EnumerateFiles(HostingEnvironment.MapPath(mainPageImagesPath))
								    .Where(path => path.EndsWith(imageExtension, StringComparison.InvariantCultureIgnoreCase))
								    .Select(path => Uri.EscapeUriString(urlPrefix + new FileInfo(path).Name));
			
            return fileUrls;
		}

		[Route("data/catalog-page-images")]
		public IEnumerable<dynamic> GetImagesForCatalog()
		{
			const string urlPrefix = "/content/images/catalog/";
			const string mainPageImagesPath = "~/Content/Images/Catalog";

			var fileInfos = Directory.EnumerateFiles(HostingEnvironment.MapPath(mainPageImagesPath))
									.Where(path => path.EndsWith(imageExtension, StringComparison.InvariantCultureIgnoreCase))
									.Select(path => new FileInfo(path));

			return from fileInfo in fileInfos
					let bitmap = new Bitmap(fileInfo.FullName)
					select new 
					{
						Url = Uri.EscapeUriString(urlPrefix + fileInfo.Name),
						Width = bitmap.Width,
						Height = bitmap.Height,
						Title = bitmap.PropertyItems.Any(p => p.Id == 0x0320) ? 
									Encoding.UTF8.GetString(bitmap.PropertyItems.First(p => p.Id == 0x0320).Value) : null
					};
		}
	}
}