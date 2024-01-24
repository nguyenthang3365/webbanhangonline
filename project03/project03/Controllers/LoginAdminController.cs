using project03.Models.entiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project03.Controllers
{
    public class LoginAdminController : Controller
    {
        // GET: LoginAdmin
        Model1 model = new Model1();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(TAIKHOAN account)
        {
            var user = account.TENTAIKHOAN;
            var pass = account.MATKHAU;
            var count = model.TAIKHOANs.FirstOrDefault(s => s.TENTAIKHOAN == user && s.MATKHAU == pass && s.QUYEN == "admin");

            if (count == null)
            {
                if (user != null && pass != null)
                {
                    ViewBag.err = "SAI TÊN TÀI KHOẢN HOẶC MẬT KHẨU";
                }
                return View();
            }

            Session["id_admin"] = count.IDTAIKHOAN;
            Session["name_admin"] = count.TENTAIKHOAN;
            /* Session["danhdau"] = null;*/
            return RedirectToAction("Dashboard","Admin");
        }


        public ActionResult Logout()
        {
            Session["name_admin"] = null;
            return RedirectToAction("index");

        }
    }
}