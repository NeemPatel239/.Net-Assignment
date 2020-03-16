using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentMusic.Models
{
    public class MusicProducts
    {
        public MusicProducts()
        {
            CompanyID = 0;
            ProductId = 0;
            ProductName = "";
            ReleasedDate = DateTime.Now;
            Model = null;
        }
        [ForeignKey("Company")]
        public int CompanyID { get; set; }
        [Key]
        public int ProductId { get; set; }
        public String ProductName { get; set; }
        public String Model { get; set; }
        public int Price { get; set; }
        public DateTime ReleasedDate { get; set; }
        public virtual Company Company { get; set; }
    }
}
