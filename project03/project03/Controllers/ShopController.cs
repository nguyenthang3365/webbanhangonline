using project03.Models.entiy;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;

namespace project03.Controllers
{
    public class ShopController : Controller
    {
        Model1 model = new Model1();

        // var query;
        public ActionResult slide()
        {
            return View();
        }
            public ActionResult get_Header()
        {
            WebClient client = new WebClient();
            string userIpAddress = client.DownloadString("https://ipinfo.io/ip").Trim();

            // Kiểm tra nếu địa chỉ là IPv6 loopback (::1)

            string apiUrl = $"https://ipinfo.io/{userIpAddress}/json";

            try
            {


                string jsonResponse = client.DownloadString(apiUrl);

                // Xử lý dữ liệu JSON để lấy thông tin vị trí
                JObject locationData = JObject.Parse(jsonResponse);
                string country = locationData["country"].ToString();
                string city = locationData["city"].ToString();
                string latitude = locationData["loc"].ToString().Split(',')[0];
                string longitude = locationData["loc"].ToString().Split(',')[1];

                TRUYCAP tRUYCAP = new TRUYCAP();
                tRUYCAP.THOIGIAN = DateTime.Now;
                tRUYCAP.SOLUOT = 1;
                tRUYCAP.DIACHITRUYCAP = country + "-" + city + "-" + userIpAddress;
                model.TRUYCAPs.Add(tRUYCAP);
                model.SaveChanges();
                // Sử dụng thông tin vị trí ở đây
            }
            catch
            {
                // Xử lý lỗi nếu có
            }


            var loaihang = model.LoaiHangs.ToList();
            return PartialView("Header", loaihang);
        }
        // GET: Shop
       
        public void display_ca()
        {
            if(Session["id_account"]==null)
            {
                Session["sum_item_car"] = 0;
            }
            else
            {
                int s = 0;
                int account = int.Parse(Session["id_account"].ToString());
                var sum = model.GIOHANGS.Where(c => c.IDTAIKHOAN == account).ToList();
                foreach (var item in sum)
                {
                    s = s + item.SOLUONG;
                }

                Session["sum_item_car"] = s;


            }
          
        }
        public ActionResult Index()
        {
            display_ca();
            return View();
        }


        public ActionResult Index1()
        {
            return PartialView("Main");
        }

        public ActionResult render_product()
        {

            var  hanghoa = model.SANPHAMs.OrderByDescending(s => s.NGAYDANG).Skip(0).Take(8).ToList();

            return PartialView("New_product", hanghoa);
        }


        public ActionResult render_product1(int page)
        {

            var hanghoa = model.SANPHAMs.OrderBy(s => s.IDSANPHAM).Skip((page - 1) * 8).Take(8).ToList();
            return PartialView("New_product", hanghoa);
        }

        public ActionResult render(int Id)
        {
            var hanghoa= model.SANPHAMs.Where(s=>s.IDLOAI==Id).ToList();  
            return PartialView("New_product", hanghoa);
        }

        public ActionResult render_feature()
        {
            decimal sum = 0;
            var result = model.SANPHAMs.ToList();
            foreach (var item in result)
            {
                sum = sum + 1;
            }
            TempData["page_sanphamchinh"] = Math.Ceiling(sum / 8);
            var hanghoa = model.SANPHAMs.OrderBy(s => s.IDSANPHAM).Skip(0).Take(8).ToList();
        
            return PartialView("Feature_Main", hanghoa);
        }
        public ActionResult render_feature1(int page)
        {
            var hanghoa = model.SANPHAMs.OrderBy(s => s.IDSANPHAM).Skip((page - 1) * 8).Take(8).ToList();
       
            return PartialView("Feature_Main", hanghoa);
        }
        [HttpGet]
        public ActionResult uploadanh()
        {
            return View();
        }

       
        public ActionResult search(string Item)
        {
            /*var hanghoa = (from h in model.HangHoas
                          where h.TenHang.Contains(Item)
                          select h).ToList() ;*/
            var hanghoa = model.SANPHAMs.Where(h => h.TENHANG.Contains(Item)).ToList();
            return PartialView("New_product", hanghoa);
            //c2
            //  var listhang = model.HangHoas.Where(h => h.TenHang.Contains(item)).ToList();
        }


        public ActionResult cart(int item)
        {
            if(Session["id_account"]==null)
            {
                return RedirectToAction("index", "login");
            }
            else
            {
                var id_account =int.Parse(Session["id_account"].ToString());
                var order = model.GIOHANGS.FirstOrDefault(s=>s.IDTAIKHOAN== id_account && s.IDSANPHAM==item);
                if (order == null)
                {
                    var pro = model.SANPHAMs.FirstOrDefault(s => s.IDSANPHAM == item);
                    GIOHANG dat = new GIOHANG();
                    dat.IDTAIKHOAN = int.Parse(Session["id_account"].ToString());
                    dat.IDSANPHAM = pro.IDSANPHAM;
                    dat.SOLUONG = 1;
                    model.GIOHANGS.Add(dat);
                    model.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    /*order.quantity = order.quantity + 1;
                    model.SaveChanges();*/
                    Response.Write("<script>alert('san pham ')</script>");
                   
                }    
            }
            return RedirectToAction("index", "shop");
        }


        public ActionResult show_card()
        {

            if (Session["id_account"] == null)
            {
                // var query = new dathang();
                return RedirectToAction("index", "shop");
            }
            else
            {
                int account = int.Parse(Session["id_account"].ToString());
                var query = (from o in model.GIOHANGS
                             join c in model.SANPHAMs on o.IDSANPHAM equals c.IDSANPHAM
                             where o.IDTAIKHOAN == account
                             select new two
                             {
                                 id_hang = o.IDHANG,
                                 id_sp = o.IDSANPHAM,
                                 gia = (c.KHUYENMAI!=null && c.KHUYENMAI!=0)?c.KHUYENMAI:c.GIA,
                                 name = c.TENHANG,
                                 anh = c.ANH,
                                 soluong = o.SOLUONG
                             }).ToList();


                return View(query);
            }
  
        }

        public ActionResult update_soluong(GIOHANG dathang)
        {
            var slkho = model.SANPHAMs.Find(dathang.IDSANPHAM);
         
              var objCourse = model.GIOHANGS.Single(course => course.IDHANG == dathang.IDHANG);

            //Field which will be update  
            if (dathang.SOLUONG > slkho.SOLUONG) 
                {
                  Response.Write("<script>alert('so luong dat phai nho hon hoac bang so luong trong kho')</script>");
             
               }
              else
            {
                objCourse.SOLUONG = dathang.SOLUONG;

                // executes the appropriate commands to implement the changes to the database

                model.SaveChanges();
                display_ca();
              
            }
            int account = int.Parse(Session["id_account"].ToString());
            var query = (from o in model.GIOHANGS
                     join c in model.SANPHAMs on o.IDSANPHAM equals c.IDSANPHAM
                     where o.IDTAIKHOAN == account
                     select new two
                     {
                         id_hang = o.IDHANG,
                         id_sp = o.IDSANPHAM,
                         gia = c.GIA,
                         name = c.TENHANG,
                         anh = c.ANH,
                         soluong = o.SOLUONG
                     }).ToList(); 

            return View("show_card", query);


        }


        public ActionResult delete_item_cart(GIOHANG dathang)  
        {
            
            var item=model.GIOHANGS.Find(dathang.IDHANG);
            //
            model.GIOHANGS.Remove(item);
            // executes the appropriate commands to implement the changes to the database
            model.SaveChanges();
            display_ca();
            return RedirectToAction("show_card");
        }



        [HttpPost]
        public ActionResult uploadanh(HttpPostedFileBase h)
        {
            if (h.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(h.FileName);
                string _path = Path.Combine(Server.MapPath("~/Content/images"), _FileName);
                h.SaveAs(_path);
            }
            return View();
        }


        public ActionResult thanhtoan()
        {
            int id = int.Parse(Session["id_account"].ToString());
            var thongtin=model.TAIKHOANs.Find(id);
            Session["email"] = thongtin.EMAIL;
            Session["diachi"] = thongtin.DIACHI;
            Session["sodienthoai"] = thongtin.SODIENTHOAI;
            if (Session["email"] == null && Session["diachi"] == null && Session["sodienthoai"] == null)
            {
                return RedirectToAction("khach_hang_thong_tin","shop");
            }
                else
            {
                float sum_tienthanhtoan = 0;
                var noidung = "";
                //   int account = int.Parse(Session["id_account"].ToString());
                int account = int.Parse(Session["id_account"].ToString());
                var a = model.GIOHANGS.Where(s => s.IDTAIKHOAN == account).ToList();
                // tinh tien can thanh toan
                var query = (from o in model.GIOHANGS
                             join c in model.SANPHAMs on o.IDSANPHAM equals c.IDSANPHAM
                             where o.IDTAIKHOAN == account
                             select new two
                             {
                                 id_hang = o.IDHANG,
                                 id_sp = o.IDSANPHAM,
                                 gia = c.GIA,
                                 name = c.TENHANG,
                                 anh = c.ANH,
                                 soluong = o.SOLUONG
                             }).ToList();

                foreach (var o in query)
                {
                    sum_tienthanhtoan = sum_tienthanhtoan + (o.soluong * int.Parse(o.gia.ToString()));
                }
                // end tinh tien thanh toan
                foreach (var item in a)
                {
                    //noi dung gui ve mail khachhang
                    var tenhang = model.SANPHAMs.FirstOrDefault(s => s.IDSANPHAM == item.IDSANPHAM);
                    noidung = noidung + (tenhang.TENHANG).ToString() +"</br>"+ " số lượng " + (item.SOLUONG).ToString() + "<br>";
                    //end noi dung gui ve mail khachhang

                    //them du lieu vao lich su mua hang
                    LICHSUMUA lichsu = new LICHSUMUA();
                    lichsu.IDSANPHAM = item.IDSANPHAM;
                    lichsu.GIA = tenhang.GIA;
                    lichsu.TONGTIEN = (tenhang.GIA*item.SOLUONG);
                    lichsu.SOLUONG = item.SOLUONG;
                    lichsu.TRANGTHAI = "đang sử lí";
                    lichsu.IDTAIKHOAN = item.IDTAIKHOAN;
                    lichsu.TENSANPHAM = tenhang.TENHANG;
                    lichsu.THOIGIANMUA = DateTime.Now;
                    lichsu.SODIENTHOAI = Session["sodienthoai"].ToString();
                    lichsu.EMAIL = Session["email"].ToString();
                    lichsu.DIACHI = Session["diachi"].ToString();
                    model.LICHSUMUAs.Add(lichsu);
                    //end them du lieu vao lich su mua hang
                    //xoa san pham khoi gio hang sau khi thanh toan
                    var b = model.GIOHANGS.Find(item.IDHANG);
                    model.GIOHANGS.Remove(b);
                    //end xoa san pham khoi gio hang sau khi thanh toan

                    //tru so luong bang hang hoa sau khi dat hang
                    var sanpham = model.SANPHAMs.Find(item.IDSANPHAM);
                    sanpham.SOLUONG = sanpham.SOLUONG - item.SOLUONG;
                }
                //
                string sendMail = "";
                if (noidung != "")
                {
                    float sum_tien=sum_tienthanhtoan;
                    string formattedNumber = sum_tien.ToString("N0");
                    noidung = noidung + "<br>" + "tổng tiền thanh toán: " + formattedNumber + "VND";
                    try
                    {
                        string email_nguoinhan = Session["email"].ToString();
                        string fromEmail = email_nguoinhan;/*"nguyenvanthangtn2k@gmail.com"*/;
                        MailMessage mailMessage = new MailMessage(fromEmail, email_nguoinhan, "thông báo đặt hàng thành công", noidung);
                        mailMessage.IsBodyHtml = true;
                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential("nguyenvanthang2k000123@gmail.com", "hzvnthbdjdxushew");
                        smtpClient.Send(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        sendMail = ex.Message.ToString();
                        Console.WriteLine(ex.ToString());
                    }
                }
                //
                model.SaveChanges();             
                Response.Write("<script language=javascript>alert('Thanh toán thành công');</script>");
                display_ca();
                return RedirectToAction("donhang","shop");

            }
        }



        public ActionResult khach_hang_thong_tin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult khach_hang_thong_tin(thongtin info)
        {
            Session["email"] = info.email;
            Session["diachi"] = info.diachi;
            Session["sodienthoai"] = info.sodienthoai;
            return RedirectToAction("show_card","shop");
        }


        public ActionResult Detail_product(int id)
        {
            var luotxem = model.SANPHAMs.SingleOrDefault(s => s.IDSANPHAM == id);
            luotxem.LUOTXEM = luotxem.LUOTXEM + 1;
            model.SaveChanges();
            preview pr = new preview();
            var a =  model.SANPHAMs.Where(s => s.IDSANPHAM == id).First();
            ViewBag.user = model.TAIKHOANs.ToList();
            var b = model.BINHLUANs.Where(s => s.IDSANPHAM == id).ToList();
            pr.sanpham = a;
            pr.binhluans = b;
            return View(pr);
        }

        public ActionResult sanphamlienquan(int id)
        {
            var list = model.SANPHAMs.Where(s=>s.IDLOAI==id).OrderByDescending(s=>s.IDSANPHAM).Skip(0).Take(4).ToList();
            return View(list);
        }

        [HttpPost]
        public ActionResult post_comment(BINHLUAN post)
        {
            if (Session["id_account"] == null)
            {
                // var query = new dathang();
                return RedirectToAction("index", "Login");
            }
            else
            {
                 int account = int.Parse(Session["id_account"].ToString());
                //tim xem khach hang da tung mua hang chua , neu mua roi duoc binh luan
                 var account_id = model.LICHSUMUAs.FirstOrDefault(s => s.IDTAIKHOAN == account && s.IDSANPHAM==post.IDSANPHAM);

                if (account_id != null)
                {
                    BINHLUAN p = new BINHLUAN();
                    p.IDSANPHAM = post.IDSANPHAM;
                    p.NOIDUNGBINHLUAN = post.NOIDUNGBINHLUAN;
                    p.IDTAIKHOAN = account;
                    p.NGAYBINHLUAN = DateTime.Now;
                    p.DANHGIA = post.DANHGIA;

                    model.BINHLUANs.Add(p);
                    model.SaveChanges();
                    return Redirect("/shop/Detail_product?id="+post.IDSANPHAM);
                }
                else
                {
                    TempData["err_comment"] = "lưu ý: chỉ có khách hàng đã mua sản phẩm mới có quyền đánh giá sản phẩm";
                    return Redirect("/shop/Detail_product?id=" + post.IDSANPHAM);
                }
            }

        }

        
        public ActionResult DonHang()
        {
            if (Session["id_account"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                int account = int.Parse(Session["id_account"].ToString());
                var list=model.LICHSUMUAs.OrderByDescending(s=>s.THOIGIANMUA).Where(s => s.IDTAIKHOAN == account).Skip(0).Take(8).ToList();
                decimal sum = list.Count();
                TempData["page_donhang"] = Math.Ceiling(sum/8);
                return View(list);
            }
           
        }
        public ActionResult Donhang_page(int page)
        {
            if (Session["id_account"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                int account = int.Parse(Session["id_account"].ToString());
                var list = model.LICHSUMUAs.OrderByDescending(s => s.THOIGIANMUA).Where(s => s.IDTAIKHOAN == account).Skip((page - 1) * 8).Take(8).ToList();
                decimal sum = list.Count();
                TempData["page_donhang"] = Math.Ceiling(sum / 8);
                return View(list);
            }

        }

        [HttpPost]
        public ActionResult SuLiDon(LICHSUMUA ls, int donhang)
        {
            var sanpham = model.LICHSUMUAs.Find(ls.IDHANG);
            if (donhang == 2)
            {
                sanpham.TRANGTHAI = "đang chuẩn bị hàng";
            }
            else if (donhang == 3)
            {
                sanpham.TRANGTHAI = "đang giao";
            }
            else
            {
                sanpham.TRANGTHAI = "đã hoàn thành";
            }
            model.SaveChanges();

            return RedirectToAction("DonHang", "Shop");

        }

        public ActionResult ChiTietDonHang(int id)
        {
            var list = model.LICHSUMUAs.Where(s => s.IDHANG == id).FirstOrDefault();
            return View(list);
        }

        public ActionResult slides()
        {
            var list = model.SLIDEs.ToList();
           return PartialView("slides",list);
        }
        // lien he
        public ActionResult LienHe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LienHe([Bind(Include = "ID,TEN,EMAIL,TINNHAN,NGAYGUI")] LIENHE lienhe)
        {
            lienhe.NGAYGUI = DateTime.Now;
            model.LIENHEs.Add(lienhe);
            model.SaveChanges();
            Response.Write("<script>alert('Gửi thành công')</script>");
            return View();
        }

        public ActionResult TinTuc()
        {
            var list = model.TINTUCs.OrderByDescending(s => s.NGAYDANG).Skip(0).Take(10).ToList();   
            var listtintuc = model.TINTUCs.ToList();
            decimal sum = listtintuc.Count();
            TempData["page_tintuc"] = Math.Ceiling(sum / 10);
            return View(list);
        }
        public ActionResult tintuc_page(int page)
        {
            decimal sum = 0;
            var result = model.TINTUCs.ToList();
            sum = result.Count();
            TempData["page_tintuc"] = Math.Ceiling(sum / 10);
            var list = model.TINTUCs.OrderBy(s => s.NGAYDANG).Skip((page - 1) * 10).Take(10).ToList();
            return View("TinTuc", list);
        }
        public ActionResult Thongtincanhan()
        {
            if (Session["id_account"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                int account = int.Parse(Session["id_account"].ToString());
                var list = model.TAIKHOANs.FirstOrDefault(s => s.IDTAIKHOAN == account);
                return View(list);
            }
        }
        [HttpPost]
        public ActionResult Thongtincanhan(TAIKHOAN taikhoan)
        {
            var Taikhoan = model.TAIKHOANs.Find(taikhoan.IDTAIKHOAN);
            Taikhoan.EMAIL = taikhoan.EMAIL;
            Taikhoan.DIACHI = taikhoan.DIACHI;
            Taikhoan.SODIENTHOAI = taikhoan.SODIENTHOAI;
            taikhoan.MATKHAU = taikhoan.MATKHAU;
            model.SaveChanges();
            Response.Write("<script>alert('Gửi thành công')</script>");
            return View(Taikhoan);
        }

        public JsonResult xacnhanmatkhau(TAIKHOAN tAIKHOAN)
        {
            var list = model.TAIKHOANs.FirstOrDefault(s=>s.IDTAIKHOAN==tAIKHOAN.IDTAIKHOAN && s.MATKHAU==tAIKHOAN.MATKHAU);
            if(list!=null)
            {
                return Json(new{status = true}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PlusLike(int id)
        {
            var news = model.TINTUCs.Find(id);
            news.LUOTTHICH = news.LUOTTHICH + 1;
            model.SaveChanges();
            news = model.TINTUCs.Find(id);
            return Json(new {soluong = news.LUOTTHICH}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddBinhluanTin(BINHLUAN_TIN tinTuc)
        {
            if (Session["id_account"] == null)
            {
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
          
                BINHLUAN_TIN bl = new BINHLUAN_TIN();
                bl.NOIDUNGBINHLUAN = tinTuc.NOIDUNGBINHLUAN;
                bl.NGAYBINHLUAN = DateTime.Now;
                bl.ID_TIN = tinTuc.ID_TIN;
                bl.TENTAIKHOAN = Session["name_account"].ToString();
                model.BINHLUAN_TINs.Add(bl);
                model.SaveChanges();
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            
        }

        public ActionResult DisplayComment(int commentId)
        {
            var content_binhluan = model.BINHLUAN_TINs.OrderByDescending(s=>s.NGAYBINHLUAN).Skip(0).Take(3).Where(s => s.ID_TIN == commentId).ToList();
            return View(content_binhluan);
        }
        public ActionResult size_product(int id)
        {
            var size = model.KICHTHUOCs.Where(s => s.IDSANPHAM == id).ToList();
            return View(size);
        }


    }
    ///
 








}
