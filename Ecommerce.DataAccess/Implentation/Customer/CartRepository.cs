using Ecommerce.DataAccess.Interface.Customer;
using Ecommerce.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Implentation.Customer
{
    public class CartRepository : ICartInterface
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));
            var result = await _context.Cart.Include(c => c.CartItem).ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            return result;
        }
         //await _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product)
         //    .FirstOrDefaultAsync(c => c.UserId == userId);

        public async Task<Cart> CreateCartAsync(string userId)
        {
            var cart = new Cart { UserId = userId, CartItem = new List<CartItem>() };
            _context.Cart.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task AddItemAsync(string userId, int productId, int quantity)
        {
            var cart = await GetCartByUserIdAsync(userId) ?? await CreateCartAsync(userId);
            var existingItem = cart.CartItem.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingItem != null) existingItem.Quantity += quantity;
            else cart.CartItem.Add(new CartItem { ProductId = productId, Quantity = quantity });
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(int cartItemId)
        {
            var item = await _context.CartItem.FindAsync(cartItemId);
            if (item != null)
            {
                _context.CartItem.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
