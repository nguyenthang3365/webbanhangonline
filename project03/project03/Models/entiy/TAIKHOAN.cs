using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project03.Models.entiy
{
    [Table("TAIKHOAN")]
    public class TAIKHOAN
    {
        [Key]
        [Display(Name = "ID Tài Khoản")]
        public int IDTAIKHOAN { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [Display(Name = "Tên Tài Khoản")]
        public string TENTAIKHOAN { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [MinLength(8)]
        [Display(Name = "Mật Khẩu")]
        public string MATKHAU { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [Display(Name = "Số Điện Thoại")]
        public string SODIENTHOAI { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EMAIL { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [Display(Name = "Địa Chỉ")]
        public string DIACHI { get; set; }

        [Display(Name = "Quyền")]
        public string QUYEN { get; set; }
    }
}