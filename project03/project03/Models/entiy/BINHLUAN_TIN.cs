using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace project03.Models.entiy
{
    [Table("BINHLUAN_TIN")]
    public class BINHLUAN_TIN
    {
        [Key]
        public int ID { set; get; }
        public string TENTAIKHOAN { get; set; }
        public string NOIDUNGBINHLUAN { get; set; }
        public DateTime NGAYBINHLUAN { get; set; }
        public int ID_TIN { get; set; }
    }
}