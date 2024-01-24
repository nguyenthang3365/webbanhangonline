namespace project03.Models.entiy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public  class two
    {
        public int id_hang { set; get; }
        public int id_sp { get; set; }
        public  int soluong { get; set; }
        public  double? gia { get; set; }
        public  string name { get; set; }
        public  string anh { get; set; }


    }
}
