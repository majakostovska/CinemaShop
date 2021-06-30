using CinemaShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaShop.Domain.DomainModels
{
    public class Order : BaseEntity
    {

        public string UserId { get; set; }
        public CinemaShopUser User { get; set; }
        public IEnumerable<ProductInOrder> ProductInOrders { get; set; }

    }
}
