using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace project03.Models.entiy
{
    [Table("BINHLUAN")]
    public class BINHLUAN
    {
        [Key]
        public int IDBINHLUAN { set; get; }
        public int IDTAIKHOAN { get; set; }
        public int IDSANPHAM { get; set; }
        public string NOIDUNGBINHLUAN { get; set; }
        public DateTime NGAYBINHLUAN { get; set; }
        public int DANHGIA { get; set; }
    }
}