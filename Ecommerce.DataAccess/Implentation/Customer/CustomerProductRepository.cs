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
    public class CustomerProductRepository  :IProductCusInterface
    {
        private readonly ApplicationDbContext _context;

        public CustomerProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync(string category, string size, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Products.Include(p => p.Inventory).Include(p => p.Category).AsQueryable();
            if (!string.IsNullOrEmpty(category)) query = query.Where(p => p.Category.Name == category);
            if (!string.IsNullOrEmpty(size)) query = query.Where(p => p.Size == size);
            if (minPrice.HasValue) query = query.Where(p => p.Price >= minPrice);
            if (maxPrice.HasValue) query = query.Where(p => p.Price <= maxPrice);
            return await query.ToListAsync();
        }
        public async Task<Product> GetByIdAsync(int id) =>
            await _context.Products.Include(p => p.Inventory).FirstOrDefaultAsync(p => p.Id == id);
    }
}
