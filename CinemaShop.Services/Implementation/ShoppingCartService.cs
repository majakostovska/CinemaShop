using CinemaShop.Domain.DomainModels;
using CinemaShop.Domain.DTO;
using CinemaShop.Repository.Interface;
using CinemaShop.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaShop.Services.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ProductInOrder> _productInOrderRepository;
        private readonly IUserRepository _userRepository;
        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<ProductInOrder> productInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _productInOrderRepository = productInOrderRepository;
        }

        public bool DeleteProductFromShoppingCart(string userId,Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.ProductInShoppingCarts.Where(z => z.ProductId.Equals(id)).FirstOrDefault();

                userShoppingCart.ProductInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }

            return false;
        }

        public ShoppingCartDTO getShoppingCartInfo(string userId)
        {

            var loggedInUser = this._userRepository.Get(userId);

            var userShoppingCart = loggedInUser.UserCart;
            var AllProducts = userShoppingCart.ProductInShoppingCarts.ToList();
            var allProductPrice = AllProducts.Select(z=> new 
            {
                ProductPrice = z.Product.Price,
                Quantity = z.Quantity
            }).ToList();

            double totalPrice = 0;
            foreach (var item in allProductPrice)
            {
                totalPrice += item.ProductPrice * item.Quantity;
            }

            ShoppingCartDTO shoppingCartDTOItem = new ShoppingCartDTO
            {
                Products = AllProducts,
                TotalPrice = totalPrice
            };
            //var allProducts = userShoppingCart.ProductInShoppingCarts.Select(z => z.Product).ToList();
            return shoppingCartDTOItem;
        }

        public bool Order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userShoppingCart = loggedInUser.UserCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    User=loggedInUser
                    
                };

                this._orderRepository.Insert(order);
                List<ProductInOrder> productInOrders = new List<ProductInOrder>();
                var result = userShoppingCart.ProductInShoppingCarts.Select(z => new ProductInOrder
                {
                    Id = Guid.NewGuid(),
                    ProductId = z.ProductId,
                    OrderId =z.ShoppingCartId,
                    Quantity = z.Quantity,
                    UserOrder = order
                }).ToList();

                productInOrders.AddRange(result);
                foreach(var item in productInOrders)
                {
                    this._productInOrderRepository.Insert(item);
                }
                loggedInUser.UserCart.ProductInShoppingCarts.Clear();
                this._userRepository.Update(loggedInUser);
                return true;
            }
            return false;
        }

        
    }
}
