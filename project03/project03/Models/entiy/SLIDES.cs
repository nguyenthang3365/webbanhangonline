using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project03.Models.entiy
{
    [Table("SLIDES")]
    public class SLIDES
    {
        [Key]
        public int ID { set; get; }

        [Display(Name = "Ảnh")]
        
        public string ANH { set; get; }
        [Display(Name = "TIÊU ĐỀ")]
        public string TIEUDE { set; get; }
    }
}