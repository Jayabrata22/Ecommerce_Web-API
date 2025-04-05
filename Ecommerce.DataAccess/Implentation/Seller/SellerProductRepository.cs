using Ecommerce.DataAccess.Interface.Seller;
using Ecommerce.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Implentation.Seller
{
    public  class SellerProductRepository :IProductInterface
    {
        private readonly ApplicationDbContext _context;

        public SellerProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Inventory> GetInventoryAsync(int productId) =>
            await _context.Inventories.FirstOrDefaultAsync(i => i.ProductId == productId);

        public async Task<bool> UpdateInventoryAsync(int productId, int quantity)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductId == productId);
            if (inventory == null) return false;
            inventory.Quantity = quantity;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
