using CinemaShop.Domain.DomainModels;
using CinemaShop.Repository.Interface;
using CinemaShop.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaShop.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRespository;
        public OrderService(IOrderRepository orderRespository)
        {
            this._orderRespository = orderRespository;
        }
        public List<Order> getAllOrders()
        {
            return this._orderRespository.getAllOrders();
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return this._orderRespository.GetOrderDetails(model);
        }
    }
}
