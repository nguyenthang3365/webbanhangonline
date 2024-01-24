using project03.Models.entiy;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace project03.Controllers
{
    public class LoginController : Controller
    {
        Model1 model = new Model1();
        [HttpGet]
        public  ActionResult Index()
        {
            Thread.Sleep(5000);
            return View();
        }
        [HttpPost]
        public ActionResult Index(TAIKHOAN account)
        {
            var user = account.TENTAIKHOAN;
            var pass = account.MATKHAU;
            var count = model.TAIKHOANs.FirstOrDefault(s => s.TENTAIKHOAN == user && s.MATKHAU == pass);
          
                if (count == null)
                {
                    if(user!=null && pass!=null)
                    {
                        ViewBag.err = "SAI TÊN TÀI KHOẢN HOẶC MẬT KHẨU";
                    }
                    return View();
                }
            
            Session["id_account"] = count.IDTAIKHOAN;
            Session["name_account"] = count.TENTAIKHOAN;
            Session["danhdau"] = null;
            Response.Write("<script>alert('Đăng nhập thành công')</script>");
          //  Thread.Sleep(5000);
            return Redirect("Shop/Index");
        }

        public ActionResult logout()
        {
            Thread.Sleep(5000);
            Session.Clear();
            Response.Write("<script>alert('Đăng xuất thành công')</script>");
            return RedirectToAction("Index","shop");          
        }
    }

}