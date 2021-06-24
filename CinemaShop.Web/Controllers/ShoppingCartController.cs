using CinemaShop.Web.Data;
using CinemaShop.Web.Models.Domain;
using CinemaShop.Web.Models.DTO;
using CinemaShop.Web.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CinemaShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<CinemaShopUser> _userManager;

        public ShoppingCartController(ApplicationDbContext context, UserManager<CinemaShopUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInUser = await _context.Users
                .Where(z => z.Id == userId)
                .Include(z => z.UserCart)
                .Include(z=>z.UserCart.ProductInShoppingCarts)
                .Include("UserCart.ProductInShoppingCarts.Product")
                .FirstOrDefaultAsync();

            var userShoppingCart = loggedInUser.UserCart;
            var productPrice = userShoppingCart.ProductInShoppingCarts.Select(z => new
            {
                ProductPrice = z.Product.Price,
                Quantity = z.Quantity
            }).ToList();

            double totalPrice = 0;
            foreach(var item in productPrice)
            {
                totalPrice += item.ProductPrice * item.Quantity;
            }

            ShoppingCartDTO shoppingCartDTOItem = new ShoppingCartDTO
            {
                ProductInShoppingCarts = userShoppingCart.ProductInShoppingCarts.ToList(),
                TotalPrice = totalPrice
            };
            //var allProducts = userShoppingCart.ProductInShoppingCarts.Select(z => z.Product).ToList();
            return View(shoppingCartDTOItem);
        }
        public async Task<IActionResult> DeleteProductFromShoppingCart(Guid productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInUser = await _context.Users
                .Where(z => z.Id == userId)
                .Include(z => z.UserCart)
                .Include(z => z.UserCart.ProductInShoppingCarts)
                .Include("UserCart.ProductInShoppingCarts.Product")
                .FirstOrDefaultAsync();

            var userShoppingCart = loggedInUser.UserCart;
            var productToDelete = userShoppingCart.ProductInShoppingCarts
                .Where(z => z.ProductId == productId)
                .FirstOrDefault();
            userShoppingCart.ProductInShoppingCarts.Remove(productToDelete);
            _context.Update(userShoppingCart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "ShoppingCart");

        }
        public async Task<IActionResult> OrderNow()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInUser = await _context.Users
                .Where(z => z.Id == userId)
                .Include(z => z.UserCart)
                .Include(z => z.UserCart.ProductInShoppingCarts)
                .Include("UserCart.ProductInShoppingCarts.Product")
                .FirstOrDefaultAsync();

            var userShoppingCart = loggedInUser.UserCart;

            Order orderItem = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                User = loggedInUser
            };

            _context.Add(orderItem);
            await _context.SaveChangesAsync();
            List<ProductInOrder> productInOrders = new List<ProductInOrder>();
            productInOrders = userShoppingCart.ProductInShoppingCarts
                .Select(z => new ProductInOrder
                {
                    OrderId = orderItem.Id,
                    ProductId=z.ProductId,
                    SelectedProduct=z.Product,
                    UserOrder=orderItem
                }).ToList();

            foreach(var item in productInOrders)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
            }

            loggedInUser.UserCart.ProductInShoppingCarts.Clear();
            _context.Update(loggedInUser);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
