using CinemaShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaShop.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> getAllOrders();
        Order GetOrderDetails(BaseEntity model);
    }
}
