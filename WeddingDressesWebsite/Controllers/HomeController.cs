using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingDressesWebsite.Controllers
{
	public class HomeController : Controller
	{
		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (ConfigurationManager.AppSettings.AllKeys.Contains("ShopName") == false)
				throw new ApplicationException("No shop name configured, please check your config.");

            var storeName = ConfigurationManager.AppSettings["ShopName"];
			ViewBag.StoreName = storeName;
        }
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}		
	}
}