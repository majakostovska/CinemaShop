using CinemaShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaShop.Services.Interface
{
    public interface IOrderService
    {
        List<Order> getAllOrders();

        Order GetOrderDetails(BaseEntity model);
    }
}
