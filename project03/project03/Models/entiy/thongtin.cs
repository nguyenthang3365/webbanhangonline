namespace project03.Models.entiy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class thongtin
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string sodienthoai { get; set; }

        [Required]
        public string diachi { get; set; }

    }
}
