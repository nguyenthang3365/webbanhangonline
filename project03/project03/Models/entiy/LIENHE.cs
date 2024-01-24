using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project03.Models.entiy
{
    [Table("LIENHE")]
    public class LIENHE
    {
        [Key]
        public int ID { set; get; }
        [Display(Name = "Tên")]
        public string TEN { get; set; }
        [Display(Name = "Email")]
        public string EMAIL{ get; set; }
        [Display(Name = "Tin Nhắn")]
        public string TINNHAN { get; set; }
        [Display(Name = "Ngày Gửi")]
        public DateTime NGAYGUI { get; set; }
        [Display(Name = "Check")]
        public string XEM { get; set; }
    }
}