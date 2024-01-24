using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project03.Models.entiy
{
    [Table("TINTUC")]
    public class TINTUC
    {
        [Key]
        public int ID { set; get; }
        [Display(Name = "Ảnh")]
       
        public string ANH { set; get; }
        [Display(Name = "Nội Dung")]
        public string NOIDUNG { set; get; }
        [Display(Name = "Tiêu Đề")]
        public string TIEUDE { set; get; }
        [Display(Name = "Ngày Đăng")]
        public DateTime NGAYDANG { set; get; }
        [Display(Name = "Lượt Thích")]
        public int LUOTTHICH{ set; get; } 
    }
}