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
        private readonly IRepository<EmailMessage> _mailRepository;
        private readonly IRepository<ProductInOrder> _productInOrderRepository;
        private readonly IUserRepository _userRepository;
        public ShoppingCartService(IRepository<EmailMessage> mailRepository, IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<ProductInOrder> productInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _productInOrderRepository = productInOrderRepository;
            _mailRepository = mailRepository;
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
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                EmailMessage mail = new EmailMessage();
                mail.MailTo = loggedInUser.Email;
                mail.Subject = "Successfully created order";
                mail.Status = false;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<ProductInOrder> productInOrders = new List<ProductInOrder>();

                var result = userShoppingCart.ProductInShoppingCarts.Select(z => new ProductInOrder
                {
                    Id = Guid.NewGuid(),
                    ProductId = z.Product.Id,
                    OrderedProduct = z.Product,
                    OrderId = order.Id,
                    UserOrder = order,
                    Quantity=z.Quantity
                }).ToList();

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0;

                sb.AppendLine("Успешна нарачка. Нарачката содржи: ");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];

                    totalPrice += item.Quantity * item.OrderedProduct.Price;

                    sb.AppendLine(i.ToString() + ". " + item.OrderedProduct.ProductName + " со цена: " + item.OrderedProduct.Price + " и количина: " + item.Quantity);
                }

                sb.AppendLine("Вкупно: " + totalPrice.ToString());


                mail.Content = sb.ToString();


                productInOrders.AddRange(result);

                foreach (var item in productInOrders)
                {
                    this._productInOrderRepository.Insert(item);
                }

                loggedInUser.UserCart.ProductInShoppingCarts.Clear();

                this._userRepository.Update(loggedInUser);
                this._mailRepository.Insert(mail);

                return true;
            }
            return false;
        
    }
}
}
