using Ecommerce.DataAccess.Interfaces;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository
{
    public class ProductRepository : IProductRepository 
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProduct(Product product)
        {
            var finalProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                Stock = product.Stock,
                SellerId = 4
                
            };

            var newProduct =  _context.Products.Add(finalProduct);
           // await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Products ON");
            await _context.SaveChangesAsync();
            //await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Products OFF");
            return newProduct.Entity;
            
        }

        public async Task<bool> DeleteProduct(int ProductId)
        {
            var Datapresent = await _context.Products.FindAsync(ProductId);
            if (Datapresent != null)
            {
                _context.Products.Remove(Datapresent);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public Task<int> GetlastId()
        {
            var entity = _context.Products.OrderByDescending(x => x.Id).FirstOrDefault();
            if (entity != null)
            {
                return Task.FromResult(entity.Id);
            }
            else
            {
                return Task.FromResult(0);
            }
        }

        public async Task<Product> GetProductbyID(int ProductId)
        {
            var Datapresent = await _context.Products.Where(x =>x.Id ==ProductId).FirstOrDefaultAsync();
            if (Datapresent != null)
            {
                return Datapresent;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
