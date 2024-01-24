using project03.Models.entiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace project03.Controllers
{
    public class RegisterController : Controller
    {
        Model1 model = new Model1();
        // GET: Register1
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost()]
        public ActionResult index(TAIKHOAN taikhoan)
        {
               var checktk = model.TAIKHOANs.Where(s => (s.TENTAIKHOAN).ToUpper() == (taikhoan.TENTAIKHOAN).ToUpper());
               if(checktk==null)
              {
                TAIKHOAN newac = new TAIKHOAN();
                newac.TENTAIKHOAN = taikhoan.TENTAIKHOAN;
                newac.MATKHAU = taikhoan.MATKHAU;
                newac.DIACHI = taikhoan.DIACHI;
                newac.EMAIL = taikhoan.EMAIL;
                newac.SODIENTHOAI = taikhoan.SODIENTHOAI;
                model.TAIKHOANs.Add(newac);
                model.SaveChanges();
                Response.Write("<script>alert('đăng kí thành công')</script>");
               
              }   
               else
             {
                Response.Write("<script>alert('tài khoản đã tồn tại vui lòng đăng kí lại')</script>");
              
             }
            return View();
        }

        public ActionResult Laymatkhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Laymatkhau(TAIKHOAN taikhoan)
        {
            var checktk = model.TAIKHOANs.FirstOrDefault(s => (s.TENTAIKHOAN).ToUpper() == (taikhoan.TENTAIKHOAN).ToUpper() && s.EMAIL==taikhoan.EMAIL);
            if (checktk == null)
            {
                Response.Write("<script>alert('Thông tin đăng kí không đúng')</script>");
            }
            else
            {              
                Random random = new Random();
                // Sinh một số ngẫu nhiên trong khoảng từ 10000000 đến 99999999
                int randomNumber = random.Next(10000000, 99999999);
                // Chuyển số nguyên thành chuỗi và thêm vào model hoặc hiển thị trực tiếp trong view
                string randomString = randomNumber.ToString();
                checktk.MATKHAU = randomString;
                model.SaveChanges();
                  try
                  {
                      string email_nguoinhan = taikhoan.EMAIL.ToString();
                      string fromEmail = email_nguoinhan;
                      MailMessage mailMessage = new MailMessage(fromEmail, email_nguoinhan, "Mật Khẩu Từ webssite Thắng Nga", randomString);
                      mailMessage.IsBodyHtml = true;
                      SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                      smtpClient.EnableSsl = true;
                      smtpClient.UseDefaultCredentials = false;
                      smtpClient.Credentials = new NetworkCredential("nguyenvanthang2k000123@gmail.com", "hzvnthbdjdxushew");
                      smtpClient.Send(mailMessage);
                       Response.Write("<script>alert('Hãy kiểm tra mail')</script>");
                }
                  catch (Exception ex)
                  {                    
                      Console.WriteLine(ex.Message.ToString());
                  }              
            }            
            return View();
        }

    }
}