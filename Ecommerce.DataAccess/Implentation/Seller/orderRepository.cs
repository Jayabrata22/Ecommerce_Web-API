﻿using Ecommerce.DataAccess.Interface.Seller;
using Ecommerce.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Implentation.Seller
{
    public class orderRepository : IOrderInterface
    {
        private readonly ApplicationDbContext _context;

        public orderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync() =>
         await _context.Order.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToListAsync();

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _context.Order.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GetSellerIdFromOrderAsync(int orderId)
        {
            var orderItem = await _context.OrderItem
                .Include(oi => oi.Product)
                .FirstOrDefaultAsync(oi => oi.OrderId == orderId);

            return orderItem?.Product?.SellerId;
        }
    }
}
