using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project03.Models
{
    public class User
    {
       public long Id { get; set; }
        
        [Required]
        public string name { get; set; }
        public string adress { get; set; }
        public string email { set; get; }
    }
}