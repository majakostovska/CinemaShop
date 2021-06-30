
using CinemaShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaShop.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
   
        public string OwnerId { get; set; }
        public CinemaShopUser Owner { get; set; }
        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
    }
}
