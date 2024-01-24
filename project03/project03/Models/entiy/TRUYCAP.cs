using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project03.Models.entiy
{
    [Table("TRUYCAP")]
    public class TRUYCAP
    {
        [Key]
        public int ID { get; set; }

        public DateTime THOIGIAN { get; set; }


        public int SOLUOT { get; set; }

        public string DIACHITRUYCAP { get; set; }
    }
}