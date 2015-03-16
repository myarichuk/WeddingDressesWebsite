using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Web.Http;

namespace WeddingDressesWebsite.Controllers
{
	public class DataController : ApiController
	{
		private const string imageExtension = ".jpg";
		private const string imageExtension2 = ".jpeg";

		[Route("data/main-page-images")]
		public IEnumerable<string> GetImagesForMainPage()
		{
			const string urlPrefix = "/content/images/main/";
            const string mainPageImagesPath = "~/Content/Images/Main";

			var fileUrls = Directory.EnumerateFiles(HostingEnvironment.MapPath(mainPageImagesPath),"*",SearchOption.AllDirectories)
								    .Where(path => path.EndsWith(imageExtension, StringComparison.InvariantCultureIgnoreCase) ||
												   path.EndsWith(imageExtension2, StringComparison.InvariantCultureIgnoreCase))
								    .Select(path => Uri.EscapeUriString(urlPrefix + new FileInfo(path).Name));
			
            return fileUrls;
		}

		[Route("images/catalog")]
		public HttpResponseMessage GetCatalogImage(string url, int? width)
		{
			if (string.IsNullOrWhiteSpace(url))	//cannot translate absolute uri to path
				return new HttpResponseMessage(HttpStatusCode.BadRequest);

			var basePath = HostingEnvironment.MapPath("~/");
			var imagePath = basePath + Path.DirectorySeparatorChar + url.Replace('/', Path.DirectorySeparatorChar);

			var fileInfo = new FileInfo(imagePath);
			if (fileInfo.Exists == false)
				return new HttpResponseMessage(HttpStatusCode.NotFound);

			var imageBytes = File.ReadAllBytes(imagePath);
			var result = new HttpResponseMessage(HttpStatusCode.OK);
			using (var inStream = new MemoryStream(imageBytes))
			{
				using (var outStream = new MemoryStream())
				{
					using (var imageFactory = new ImageFactory())
					{
						imageFactory.Load(inStream)
									.Resize(new Size(width ?? 0,0))
									.Format(new JpegFormat())
									.Quality(70)
									.Save(outStream);
					}

					result.Content = new ByteArrayContent(outStream.ToArray());
					result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
				}
			}

			return result;
		}

		[Route("data/catalog-page-images")]
		public IEnumerable<dynamic> GetImagesForCatalog()
		{
			const string mainPageImagesPath = "~/Content/Images/Catalog";

			var fileInfos = Directory.EnumerateFiles(HostingEnvironment.MapPath(mainPageImagesPath), "*", SearchOption.AllDirectories)
									.Where(path => path.EndsWith(imageExtension, StringComparison.InvariantCultureIgnoreCase))
									.Select(path => new FileInfo(path))
									.ToList();

			List<ImageData> imageData = null;
            try
			{
				imageData = (from fileInfo in fileInfos
								let bitmap = new Bitmap(fileInfo.FullName)
								let pathCutoffIndex = fileInfo.FullName.IndexOf("\\Content\\", StringComparison.InvariantCultureIgnoreCase)
								let imagePath = fileInfo.FullName.Substring(pathCutoffIndex)
								select new ImageData
								{
									Url = Uri.EscapeUriString(imagePath.Replace("\\","/")),
									Width = bitmap.Width,
									Height = bitmap.Height,
									Title = bitmap.PropertyItems.Any(p => p.Id == 270) ?
												Encoding.UTF8.GetString(bitmap.PropertyItems.First(p => p.Id == 270).Value).Replace("\u0000", String.Empty) : null,
									@Bitmap = bitmap
								}).ToList();

				return imageData.Select(item => new
				{
					item.Url,item.Width,item.Height,item.Title
				});
			}
			finally
			{
				imageData.ForEach(item => item.Dispose());
            }
		}

		private class ImageData : IDisposable
		{
			public string Url { get; set; }

			public int Width { get; set; }

			public int Height { get; set; }

			public string Title { get; set; }

			public Bitmap @Bitmap { get; set; }

			public void Dispose()
			{
				if(Bitmap != null)
				Bitmap.Dispose();
			}
		}
	}
}