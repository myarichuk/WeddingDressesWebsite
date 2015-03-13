using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingDressesWebsite.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult MainPageImages()
		{
			var files = Directory.EnumerateFiles(Server.MapPath("~/Content/Images/MainPage"))
								 .Where(file => file.ToLower().EndsWith(".jpg"));

			return Json(files);
		}
	}
}