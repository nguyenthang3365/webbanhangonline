using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project03.Models.entiy
{
    [Table("KICHTHUOC")]
    public class KICHTHUOC
    {
        [Key]
        public int IDKICHTHUOC { set; get; }
        public int IDSANPHAM { set; get; }
        public int TENKICHTHUOC { set; get; }
    }
}