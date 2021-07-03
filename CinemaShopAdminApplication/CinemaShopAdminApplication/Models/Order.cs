using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaShopAdminApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public CinemaShopUser User { get; set; }
        public ICollection<ProductInOrder> ProductInOrders { get; set; }
    }
}
