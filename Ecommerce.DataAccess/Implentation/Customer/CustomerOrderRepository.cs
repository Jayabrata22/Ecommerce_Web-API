﻿using Ecommerce.DataAccess.Interface.Customer;
using Ecommerce.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Implentation.Customer
{
    public class CustomerOrderRepository : ICustomerOrderInterface
    {
        private readonly ApplicationDbContext _context;

        public CustomerOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrderAsync(string userId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null || !cart.CartItems.Any()) return null;

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                TotalAmount = cart.CartItems.Sum(i => i.Product.Price * i.Quantity),
                OrderItems = cart.CartItems.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    PriceAtPurchase = i.Product.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(string userId) =>
            await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId).ToListAsync();

        public async Task<Order> GetOrderByIdAsync(int orderId) =>
            await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

    }
}
