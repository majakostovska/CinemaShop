
using CinemaShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaShop.Services.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDTO getShoppingCartInfo(string userId);
        bool DeleteProductFromShoppingCart(string userId,Guid id);
        bool Order(string userId);
    }
}
