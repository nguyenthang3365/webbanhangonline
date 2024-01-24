using project03.Models.entiy;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace project03.Controllers
{
    public class AdminController : DefaultController
    {
        // Lấy chuỗi kết nối từ web.config
        string mySetting = ConfigurationManager.ConnectionStrings["Qlhanghoa"].ConnectionString;
        // GET: Admin
        Model1 model = new Model1();
        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }

        public ActionResult DSsanpham()
        {
          //  var list = model.SANPHAMs.Take(10).ToList();
            //
            decimal sum = 0;
            var result = model.SANPHAMs.ToList();
            foreach (var item in result)
            {
                sum = sum + 1;
            }
            TempData["page_sanpham"] = Math.Ceiling(sum / 5);
            var list = model.SANPHAMs.OrderBy(s => s.IDSANPHAM).Skip(0).Take(5).ToList();
            return View(list);
        }

        public ActionResult DSsanphamwithpage(int page)
        {
            //  var list = model.SANPHAMs.Take(10).ToList();
            //
            decimal sum = 0;
            var result = model.SANPHAMs.ToList();
            foreach (var item in result)
            {
                sum = sum + 1;
            }
            TempData["page_sanpham"] = Math.Ceiling(sum / 5);
            var list = model.SANPHAMs.OrderBy(s => s.IDSANPHAM).Skip((page-1)*5).Take(5).ToList();
            return View("DSsanpham", list);
        }

        public ActionResult Create()
        {
            ViewBag.OtherTableData = new SelectList(model.LoaiHangs, "MaLoai", "TenLoai");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "IDLOAI,TENHANG,GIA,SOLUONG,MOTA,LUOTXEM,ANH,NGAYDANG,KHUYENMAI")] SANPHAM sanpham, HttpPostedFileBase ANH)
        {
            if (ModelState.IsValid)
            {
            
                    if (ANH.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(ANH.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/images"), _FileName);
                        ANH.SaveAs(_path);
                        sanpham.ANH = _FileName;
                    }
                    sanpham.NGAYDANG = DateTime.Now;
                    model.SANPHAMs.Add(sanpham);
                    model.SaveChanges();
                  Response.Write("<script>alert('Thêm sản phẩm thành công')</script>");
                  ViewBag.OtherTableData = new SelectList(model.LoaiHangs, "MaLoai", "TenLoai");             
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
               var sanpham = model.SANPHAMs.Find(id);
                model.SANPHAMs.Remove(sanpham);
                string directoryimg = Server.MapPath("~/Content/images/" + sanpham.ANH);
                if (System.IO.File.Exists(directoryimg))
                {
                    System.IO.File.Delete(directoryimg);
                }
                model.SaveChanges();
                return RedirectToAction("DSsanpham", "Admin");
            }
           
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                var sanpham = model.SANPHAMs.Find(id);
                ViewBag.OtherTableData = new SelectList(model.LoaiHangs, "MaLoai", "TenLoai");
                return View(sanpham);
        }
   

        [HttpPost]
        public ActionResult Edit([Bind(Include = "IDSANPHAM,IDLOAI,TENHANG,GIA,ANH,SOLUONG,MOTA,LUOTXEM,KHUYENMAI")] SANPHAM sanpham , HttpPostedFileBase ANH)
        {
            var img = model.SANPHAMs.Find(sanpham.IDSANPHAM);
                    if (sanpham.ANH!=null)
                    {
                        string _FileName = Path.GetFileName(ANH.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/images"), _FileName);
                        ANH.SaveAs(_path);
                        string directoryimg = Server.MapPath("~/Content/images/" + img.ANH);
                if (System.IO.File.Exists(directoryimg))
                {
                    System.IO.File.Delete(directoryimg);
                }
                img.ANH = _FileName;
                        
                    }
                    img.IDLOAI = sanpham.IDLOAI;
                    img.TENHANG = sanpham.TENHANG;
                    img.GIA = sanpham.GIA;
                    img.SOLUONG = sanpham.SOLUONG;
                    img.MOTA = sanpham.MOTA;
                    img.LUOTXEM = sanpham.LUOTXEM;
                    img.KHUYENMAI = sanpham.KHUYENMAI;
             model.SaveChanges();
          /*  string sql = "update SANPHAM set IDLOAI='" + sanpham.IDLOAI + "',TENHANG=N'" + sanpham.TENHANG + "',GIA='" + sanpham.GIA + "',SOLUONG='" + sanpham.SOLUONG + "',MOTA=N'" + sanpham.MOTA + "',LUOTXEM='" + sanpham.LUOTXEM + "' " +
            "where IDSANPHAM='" + sanpham.IDSANPHAM + "'";
            using (SqlConnection connection = new SqlConnection(mySetting))
            {
                // Mở kết nối
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    // Kiểm tra xem có hàng nào bị ảnh hưởng không
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Sản phẩm đã được thêm thành công!");
                    }
                    else
                    {
                        Console.WriteLine("Không có sản phẩm nào được thêm.");
                    }
                }
            }*/
                Response.Write("<script>alert('sửa thông tin sản phẩm thành công')</script>");
                    ViewBag.OtherTableData = new SelectList(model.LoaiHangs, "MaLoai", "TenLoai");
                    return View(img);
        }

        public ActionResult Timkiemsanpham(string sanpham)
        {
            var result = model.SANPHAMs.Where(s => s.TENHANG.Contains(sanpham)).ToList();
            decimal sum = result.Count();
            TempData["aa"] = Math.Ceiling(sum/ 5);
            return View("DSsanpham",result);
        }

        public ActionResult QuanLiDonHang()
        {
            var list = model.LICHSUMUAs.OrderByDescending(s => s.THOIGIANMUA).Skip(0).Take(5).ToList();
            var listdonhang = model.LICHSUMUAs.ToList();
            decimal sum = listdonhang.Count();
            TempData["page_donhang_admin"] = Math.Ceiling(sum / 5);
            return View(list);
        }

        public ActionResult QuanLiDonHangwithpage(int page)
        {
            decimal sum = 0;
            var result = model.LICHSUMUAs.ToList();
            foreach (var item in result)
            {
                sum = sum + 1;
            }
            TempData["page_donhang_admin"] = Math.Ceiling(sum / 5);
            var list = model.LICHSUMUAs.OrderBy(s => s.THOIGIANMUA).Skip((page - 1) * 5).Take(5).ToList();
            return View("QuanLiDonHang", list);
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

            return RedirectToAction("QuanLiDonHang","Admin");
         
        }
        // start dasboard admin
        public ActionResult ThongKe()
        {
            var clien = model.TAIKHOANs.ToList();
            var list = model.LICHSUMUAs.Where(s => s.TRANGTHAI == "đang sử lí").ToList();
            var truycap = model.TRUYCAPs.Where(s => s.THOIGIAN.Month == DateTime.Now.Month).ToList();
      //      var email = model.LIENHEs.Where(s => s.NGAYGUI.Date == DateTime.Now.Date).ToList();
            var email = model.LIENHEs.Where(item => DbFunctions.TruncateTime(item.NGAYGUI) == DbFunctions.TruncateTime(DateTime.Now)).ToList();
            var tongtien = model.LICHSUMUAs.Where(s => s.TRANGTHAI == "đã hoàn thành").ToList();
            
            TempData["hangchuasuli"] = list.Count();
            TempData["TongTien_hang"] = tongtien.Sum(s => s.TONGTIEN);
            TempData["DonHangDaHoanThanh"] = tongtien.Count();
            TempData["luongtruycap"] = truycap.Count();
            TempData["khachhang"] = clien.Count();
            TempData["Hop"] = email.Count();

            return View();
        }

        public ActionResult ChiTietDonHang(int id)
        {
            var list = model.LICHSUMUAs.Where(s => s.IDHANG == id).FirstOrDefault();
            return View(list);
        }


        public ActionResult BanDo()
        {
            // sô lượng đơn đặt hàng theo từng tháng
            var list = model.LICHSUMUAs.GroupBy(entry => entry.THOIGIANMUA.Month).Select(group => new
                      {
                          ThoiGian = group.Key,
                          SoDonHang = group.Count()
                      }).ToList();



            var chartData = new
            {
                xValues = list.Select(item => item.ThoiGian.ToString()).ToArray(),
                yValues = list.Select(item => item.SoDonHang).ToArray()
            };

            return Json(chartData, JsonRequestBehavior.AllowGet);
          
        }

        public ActionResult BanDo2()
        {
            // sô lượng đơn đặt hàng theo từng tháng
            var list = model.TRUYCAPs.GroupBy(entry => entry.THOIGIAN.Month).Select(group => new
            {
                ThoiGian = group.Key,
                luottruycap = group.Count()
            }).ToList();



            var chartData = new
            {
                xValues = list.Select(item => item.ThoiGian.ToString()).ToArray(),
                yValues = list.Select(item => item.luottruycap).ToArray()
            };

            return Json(chartData, JsonRequestBehavior.AllowGet);

        }

        //

        public ActionResult ThongTinTruyCap()
        {
            var list = model.TRUYCAPs.OrderByDescending(s => s.THOIGIAN).Skip(0).Take(5).ToList();
            return View(list);
        }
        //

        public ActionResult DanhMucHang()
        {
            var list = model.LoaiHangs.ToList();
            return View(list);
        }
        // end dasboad
        // danh muc loai hang
        public ActionResult Editdanhmuc(int id)
        {
            var list = model.LoaiHangs.Find(id);
            return View(list);
        }
        public ActionResult Deletedanhmuc(int id)
        {
            var danhmuc=model.LoaiHangs.Find(id);
            model.LoaiHangs.Remove(danhmuc);
            model.SANPHAMs.RemoveRange(model.SANPHAMs.Where(s=>s.IDSANPHAM==id).ToList());
            model.SaveChanges();
            return RedirectToAction("DanhMucHang", "Admin");
        }
        public ActionResult Createdanhmuc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Createdanhmuc([Bind(Include = "TenLoai")] LoaiHang loaihang)
        {
            if(ModelState.IsValid)
            {
                model.LoaiHangs.Add(loaihang);
                model.SaveChanges();
            }    
            return View();
        }
        //end danh muc hang
        // start danh mục tài khoản admin
        public ActionResult DanhMucTaiKhoan()
        {
            decimal sum = 0;
            var list = model.TAIKHOANs.OrderBy(s=>s.IDTAIKHOAN).Skip(0).Take(5).ToList();
            var result = model.TAIKHOANs.ToList();
            sum = result.Count();
            TempData["page_taikhoan"] = Math.Ceiling(sum / 5);
            return View(list);
        }
        public ActionResult DanhMucTaiKhoanwithpage(int page)
        {
            decimal sum = 0;
            var result = model.TAIKHOANs.ToList();
            sum = result.Count();
            TempData["page_taikhoan"] = Math.Ceiling(sum / 5);
            var list = model.TAIKHOANs.OrderBy(s => s.IDTAIKHOAN).Skip((page - 1) * 5).Take(5).ToList();
            return View("DanhMucTaiKhoan", list);
        }

        //create tai khoan
        public ActionResult CreateTaiKhoan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTaiKhoan([Bind(Include = "TENTAIKHOAN,MATKHAU,SODIENTHOAI,EMAIL,DIACHI,QUYEN")] TAIKHOAN taikhoan)
        {
            if(ModelState.IsValid)
            {
                model.TAIKHOANs.Add(taikhoan);
                model.SaveChanges();            
                Response.Write("<script>alert('Tạo tài khoản thành công')</script>");
            }
            return View();
        }

        public ActionResult EditTaiKhoan(int id)
        {
            var taikhoan = model.TAIKHOANs.FirstOrDefault(s => s.IDTAIKHOAN == id);
            return View(taikhoan);
        }

        [HttpPost]
        public ActionResult EditTaiKhoan([Bind(Include = "IDTAIKHOAN,TENTAIKHOAN,MATKHAU,SODIENTHOAI,EMAIL,DIACHI,QUYEN")] TAIKHOAN taikhoan)
        {
            if (ModelState.IsValid)
            {
                var taikhoan_find = model.TAIKHOANs.Find(taikhoan.IDTAIKHOAN);
                taikhoan_find.TENTAIKHOAN = taikhoan.TENTAIKHOAN;
                taikhoan_find.MATKHAU = taikhoan.MATKHAU;
                taikhoan_find.SODIENTHOAI = taikhoan.SODIENTHOAI;
                taikhoan_find.EMAIL = taikhoan.EMAIL;
                taikhoan_find.DIACHI = taikhoan.DIACHI;
                taikhoan_find.QUYEN = taikhoan.QUYEN;
                model.SaveChanges();
                Response.Write("<script>alert('sửa thông tin tài khoản thành công')</script>");
            }
            var thongtintaikhoan = model.TAIKHOANs.Find(taikhoan.IDTAIKHOAN);
            return View(thongtintaikhoan);
        }
        //
        public ActionResult DeleteTaiKhoan(int id)
        {
            var taikhoan_find = model.TAIKHOANs.Find(id);
            model.TAIKHOANs.Remove(taikhoan_find);
            model.SaveChanges();
            return RedirectToAction("DanhMucTaiKhoan", "Admin");
        }
        /// start slides
        public ActionResult Slides()
        {
            
            decimal sum = 0;
            var list = model.SLIDEs.OrderBy(s => s.ID).Skip(0).Take(5).ToList();
            var result = model.SLIDEs.ToList();
            sum = result.Count();
            TempData["page_slide"] = Math.Ceiling(sum / 5);
            return View(list);
        }

        public ActionResult CreateSlides()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult CreateSlides([Bind(Include = "ANH,TIEUDE")] SLIDES slides, HttpPostedFileBase ANH)
        {
            if (ModelState.IsValid)
            {
                if (slides.ANH != null)
                {
                    string _FileName = Path.GetFileName(ANH.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/Slides"), _FileName);
                    ANH.SaveAs(_path);
                    slides.ANH = _FileName;
                }
                model.SLIDEs.Add(slides);
                model.SaveChanges();

                Response.Write("<script>alert('tạo slide thành công')</script>");
            }
            return View();
        }

        public ActionResult EditSlides(int id)
        {
            var list = model.SLIDEs.Find(id);
            return View(list);
        }

        [HttpPost]
        public ActionResult EditSlides([Bind(Include = "ID,ANH,TIEUDE")] SLIDES slides, HttpPostedFileBase ANH)
        {
            var img = model.SLIDEs.Find(slides.ID);


            if (slides.ANH != null)
            {
                string _FileName = Path.GetFileName(ANH.FileName);
                string _path = Path.Combine(Server.MapPath("~/Content/images/Slides"), _FileName);
                ANH.SaveAs(_path);
                string directoryimg = Server.MapPath("~/Content/images/Slides/"+img.ANH);
                if(System.IO.File.Exists(directoryimg))
                {
                    System.IO.File.Delete(directoryimg);
                }               
                img.ANH = _FileName;

            }
            img.TIEUDE=slides.TIEUDE;
            model.SaveChanges();
            Response.Write("<script>alert('sửa slide thành công')</script>");
            return View(img);
        }

        public ActionResult DeleteSlides(int id)
        {
            var list = model.SLIDEs.Find(id);
            model.SLIDEs.Remove(list);
            model.SaveChanges();
            return RedirectToAction("Slides", "Admin");
        }

        public ActionResult DanhMucSlidesPage(int page)
        {
            decimal sum = 0;
            var result = model.SLIDEs.ToList();
            sum = result.Count();
            TempData["page_slide"] = Math.Ceiling(sum / 5);
            var list = model.SLIDEs.OrderBy(s => s.ID).Skip((page - 1) * 5).Take(5).ToList();
            return View("Slides", list);
        }

        public ActionResult DanhMucTinTuc()
        {
            decimal sum = 0;
            var list = model.TINTUCs.OrderByDescending(s=>s.NGAYDANG).Skip(0).Take(5).ToList();
            var result = model.TINTUCs.ToList();
            sum = result.Count();
            TempData["page_TinTuc_admin"] = Math.Ceiling(sum / 5);
            return View(list);
        }

        public ActionResult DanhMucTinTucWithPage(int page)
        {
            decimal sum = 0;
            var result = model.TINTUCs.ToList();
            sum = result.Count();
            TempData["page_TinTuc_admin"] = Math.Ceiling(sum / 5);
            var list = model.TINTUCs.OrderBy(s => s.ID).Skip((page - 1) * 5).Take(5).ToList();
            return View("DanhMucTinTuc", list);
        }

        public ActionResult HopThuDen()
        {
            decimal sum = 0;
            var list = model.LIENHEs.OrderByDescending(s=>s.NGAYGUI).Skip(0).Take(5).ToList();
            var result = model.LIENHEs.ToList();
            sum = result.Count();
            TempData["page_hopthu_admin"] = Math.Ceiling(sum / 5);
            return View(list);
        }
        //tin tuc
        public ActionResult CreateTinTuc()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTinTuc([Bind(Include = "ANH,NOIDUNG,TIEUDE")] TINTUC tintuc, HttpPostedFileBase ANH)
        {
            if (ModelState.IsValid)
            {
                if (tintuc.ANH != null)
                {
                    string _FileName = Path.GetFileName(ANH.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/images/tintuc"), _FileName);
                    ANH.SaveAs(_path);
                    tintuc.ANH = _FileName;
                }
                tintuc.NGAYDANG = DateTime.Now;
                model.TINTUCs.Add(tintuc);
                model.SaveChanges();
                Response.Write("<script>alert('tạo tin tức thành công')</script>");
            }
            return View();          
        }

        public ActionResult EditTinTuc(int id)
        {
            var list = model.TINTUCs.Find(id);
            return View(list);
        }
        public ActionResult DeleteTinTuc(int id)
        {
            var list = model.TINTUCs.Find(id);
            model.TINTUCs.Remove(list);
            string directoryimg = Server.MapPath("~/Content/images/tintuc/" + list.ANH);
            if (System.IO.File.Exists(directoryimg))
            {
                System.IO.File.Delete(directoryimg);
            }
            model.SaveChanges();
            return RedirectToAction("DanhMucTinTuc", "admin");

        }
        [HttpPost]
        public ActionResult EditTinTuc([Bind(Include = "ID,ANH,NOIDUNG,TIEUDE")] TINTUC tintuc, HttpPostedFileBase ANH)
        {
            var img = model.TINTUCs.Find(tintuc.ID);
            if (tintuc.ANH != null)
            {
                string _FileName = Path.GetFileName(ANH.FileName);
                string _path = Path.Combine(Server.MapPath("~/Content/images/tintuc"), _FileName);
                ANH.SaveAs(_path);
                string directoryimg = Server.MapPath("~/Content/images/tintuc/" + img.ANH);
                if (System.IO.File.Exists(directoryimg))
                {
                    System.IO.File.Delete(directoryimg);
                }
                img.ANH = _FileName;
            }
            img.NOIDUNG = tintuc.NOIDUNG;
            img.TIEUDE = tintuc.TIEUDE;
            model.SaveChanges();
            Response.Write("<script>alert('sửa tin tức thành công')</script>");
            return View(img);
        }

        // danh muc thu
        public ActionResult DanhMucHopThu()
        {          
            decimal sum = 0;
            var list = model.LIENHEs.OrderByDescending(s => s.NGAYGUI).Skip(0).Take(5).ToList();
            var result = model.LIENHEs.ToList();
            sum = result.Count();
            TempData["page_hopthu_admin"] = Math.Ceiling(sum / 5);
            return View(list);
        }
        public ActionResult DanhMucHopThuPage()
        {

            var list = model.LIENHEs.ToList();
            return View(list);
        }

        public ActionResult DeleteThu(int id)
        {
            var list = model.LIENHEs.Find(id);
            model.LIENHEs.Remove(list);
            model.SaveChanges();
            return RedirectToAction("DanhMucHopThu", "Admin");
        }

        public ActionResult ChiTietThu(int id)
        {
            var list = model.LIENHEs.Find(id);
            return View(list);
        }
        //
        public ActionResult checked_thu([Bind(Include = "ID")] LIENHE lienhe)
        {
            var list = model.LIENHEs.Find(lienhe.ID);
            list.XEM = "1";
            model.SaveChanges();
            return View("ChiTietThu",list);
        }
    }
}