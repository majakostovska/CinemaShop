using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaShopAdminApplication.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Име на филмот")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Постер")]
        public string ProductImage { get; set; }
        [Required]
        [Display(Name = "Цена на билет")]
        public int Price { get; set; }
        [Required]
        [Display(Name = "Рејтинг")]
        public int Rating { get; set; }
        [Required]
        [Display(Name = "Датум на емитување")]
        public DateTime data { get; set; }
        
    }
}
