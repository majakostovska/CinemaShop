using CinemaShop.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaShop.Web.Models.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public CinemaShopUser User { get; set; }
        public virtual ICollection<ProductInOrder> Products { get; set; }

    }
}
