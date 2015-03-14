using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace WeddingDressesWebsite.Controllers
{	
	public class DataController : ApiController
	{
		[Route("data/main-page-images")]
		public IEnumerable<string> GetImagesForMainPage()
		{
			const string urlPrefix = "/content/images/main/";
			const string imageExtension = ".jpg";
            const string mainPageImagesPath = "~/Content/Images/Main";

			var fileUrls = Directory.EnumerateFiles(HostingEnvironment.MapPath(mainPageImagesPath))
								    .Where(path => path.EndsWith(imageExtension, StringComparison.InvariantCultureIgnoreCase))
								    .Select(path => Uri.EscapeUriString(urlPrefix + new FileInfo(path).Name));
			
            return fileUrls;
		}
	}
}