using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace project03.Models.entiy
{ 
    [Table("SANPHAM")]
    public class SANPHAM
    {
        [Key]
        [Display(Name = "ID Sản Phẩm")]
        public int IDSANPHAM { get; set; }

        [Required]
        [Display(Name = "ID Loại")]
        public int IDLOAI { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Tên Hàng")]
        public string TENHANG { get; set; }

        [Display(Name = "Giá")]
        public double? GIA { get; set; }

        [Display(Name = "Ảnh")]
        public string ANH { get; set; }

        [AllowHtml]
        [Display(Name = "Mô Tả")]
        public string MOTA { get; set; }
        [Display(Name = "Số Lượng")]
        public int SOLUONG { get; set; }
        [Display(Name = "Lượt Xem")]
        public int LUOTXEM { get; set; }
        [Display(Name = "Ngày Đăng")]
        public DateTime NGAYDANG { get; set; }

        [Display(Name = "khuyến mại")]
        public double? KHUYENMAI { get; set; }

    }
}