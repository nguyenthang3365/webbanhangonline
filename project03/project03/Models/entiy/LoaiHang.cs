namespace project03.Models.entiy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiHang")]
    public partial class LoaiHang
    {
        [Key]
        [Display(Name = "Mã Loại")]
        public int MaLoai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [StringLength(200, ErrorMessage = "{0} không được vượt quá {1} ký tự.")]
        [Display(Name = "Tên Loại")]
        public string TenLoai { get; set; }

       
       
    }
}
