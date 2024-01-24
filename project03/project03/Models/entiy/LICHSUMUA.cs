using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project03.Models.entiy
{
    [Table("LICHSUMUA")]
    public class LICHSUMUA
    {
        [Key]
        [Display(Name = "id Hàng")]
        public int IDHANG { get; set; }
        [Display(Name = "id Tài Khoản")]
        public int IDTAIKHOAN { get; set; }

        [Display(Name = "Id Sản Phẩm")]
        public int IDSANPHAM { get; set; }
        [Display(Name = "Số Lượng")]
        public int SOLUONG { get; set; }
        [Display(Name = "Thời Gian Mua")]
        public DateTime THOIGIANMUA { get; set; }
        [Display(Name = "Số Điện Thoại")]
        public string SODIENTHOAI { set; get; }
        [Display(Name = "Email")]
        public string EMAIL { set; get; }
        [Display(Name = "Địa Chỉ")]
        public string DIACHI { set; get; }
        [Display(Name = "Giá")]
        public double? GIA { set; get; }
        [Display(Name = "Tổng Tiền")]
        public double? TONGTIEN { set; get; }
        [Display(Name = "Tên Sản Phẩm")]
        public string TENSANPHAM { set; get; }
        [Display(Name = "Trạng Thái")]
        public string TRANGTHAI { set; get; }
    }
}