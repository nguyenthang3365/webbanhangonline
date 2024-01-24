using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project03.Controllers
{
    public class HomeController : Controller
    {
        public  string Index()
        {
            // Session["user"] = "nam";
          var a=  Newtonsoft.Json.JsonConvert.SerializeObject(new { foo = "bar" });
            return (a);
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
    }
}