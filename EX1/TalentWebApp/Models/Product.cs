using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalentWebApp.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, 5000)]
        public double Price { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
