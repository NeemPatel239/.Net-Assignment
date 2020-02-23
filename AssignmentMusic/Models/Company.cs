﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentMusic.Models
{
    public class Company
    {
        public Company()
        {
            CompanyName = "";
            WebSite = "abc.com";
            LastUpdate = DateTime.MinValue;
        }
        [Key]
        public int ID { get; set; }
        public String CompanyName { get; set; }
        public String WebSite { get; set; }

        public ICollection<MusicProducts> ProductList { get; set; }

        public DateTime LastUpdate { get; set; }

    }
}
