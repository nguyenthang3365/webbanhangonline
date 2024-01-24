using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project03.Models.entiy
{
    [Table("GIOHANG")]
    public class GIOHANG
    {
        [Key]
        public int IDHANG { get; set; }

        public int IDTAIKHOAN { get; set; }


        public int IDSANPHAM { get; set; }

        public int SOLUONG { get; set; }
    }
}