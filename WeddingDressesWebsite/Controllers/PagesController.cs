using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingDressesWebsite.Controllers
{
	public class PagesController : Controller
	{
		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (ConfigurationManager.AppSettings.AllKeys.Contains("ShopName") == false)
				throw new ApplicationException("No shop name configured, please check your config.");

			ViewBag.StoreName = ConfigurationManager.AppSettings["ShopName"];

			if (ConfigurationManager.AppSettings.AllKeys.Contains("Keywords") == false)
				throw new ApplicationException("No keywords configured, please check your config.");

			ViewBag.Keywords = ConfigurationManager.AppSettings["Keywords"];

			if (ConfigurationManager.AppSettings.AllKeys.Contains("MainPageImageChangeInterval") == false)
				throw new ApplicationException("No image change interval configured, please check your config.");

			ViewBag.MainPageImageChangeInterval = ConfigurationManager.AppSettings["MainPageImageChangeInterval"];
			
            if (ConfigurationManager.AppSettings.AllKeys.Contains("EmailAddress") == false)
				throw new ApplicationException("No email address configured, please check your config.");

			ViewBag.EmailAddress = ConfigurationManager.AppSettings["EmailAddress"];
			
            if (ConfigurationManager.AppSettings.AllKeys.Contains("Phone") == false)
				throw new ApplicationException("No phone configured, please check your config.");

			ViewBag.Phone = ConfigurationManager.AppSettings["Phone"];

			if (ConfigurationManager.AppSettings.AllKeys.Contains("Address") == false)
				throw new ApplicationException("No address configured, please check your config.");

			ViewBag.Address = ConfigurationManager.AppSettings["Address"];
		}
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Catalog()
		{
			return View();
		}


		public ActionResult About()
		{
			return View();
		}
	}
}