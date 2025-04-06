using Ecommerce.DataAccess.Interface;
using Ecommerce.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Implentation
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Payment.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserAsync(string userId)
        {
            return await _context.Payment
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsBySellerAsync(string sellerId)
        {
            return await _context.Payment
         .Include(p => p.Order)
             .ThenInclude(o => o.OrderItems)
                 .ThenInclude(oi => oi.Product)
         .Where(p => p.Order.OrderItems.Any(oi => oi.Product.SellerId == sellerId))
         .ToListAsync();
        }
    }
}
