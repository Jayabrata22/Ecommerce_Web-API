using Ecommerce.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Payment> Payment { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.SeedRoles();        // Call role seeder
            builder.SeedSuperAdmin();   // Call user seeder

            builder.Entity<Product>()
           .HasOne(p => p.Seller)
           .WithMany()
           .HasForeignKey(p => p.SellerId);

            builder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(i => i.ProductId);
            
            builder.Entity<Product>()
                .HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId);

            // Cart -> CartItems
            builder.Entity<Cart>()
                .HasMany(c => c.CartItem)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId);

            // Order -> OrderItems
            builder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            // CartItem -> Product
            builder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems) // now mapped
                .HasForeignKey(ci => ci.ProductId);

            // OrderItem -> Product
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems) // now mapped
                .HasForeignKey(oi => oi.ProductId);
            // Review -> Product
            builder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany()
                .HasForeignKey(r => r.ProductId);

            builder.Entity<Payment>()
        .HasOne(p => p.Order)
        .WithMany()
        .HasForeignKey(p => p.OrderId);

            builder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);
        }
    }
    public static class ModelBuilderExtensions
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "role-admin-id",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "role-seller-id",
                    Name = "Seller",
                    NormalizedName = "SELLER"
                },
                new IdentityRole
                {
                    Id = "role-customer-id",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                }
            );
        }

        public static void SeedSuperAdmin(this ModelBuilder modelBuilder)
        {
            var adminUser = new User
            {
                Id = "admin-user-id",
                UserName = "admin@clothshop.com",
                NormalizedUserName = "ADMIN@CLOTHSHOP.COM",
                Email = "admin@clothshop.com",
                NormalizedEmail = "ADMIN@CLOTHSHOP.COM",
                EmailConfirmed = true,
                PhoneNumber = "8250333465",
                PhoneNumberConfirmed = true,
                Name = "Super Admin",
                CreatedAt = new DateTime(2025, 4, 5, 10, 0, 0),
                UpdatedAt = new DateTime(2025, 4, 5, 10, 0, 0),
                SecurityStamp = "stamp-admin-1234",
                Address = "123 Admin St, Admin City, Admin State, 12345",
                Password = "Admin@123",
                Role = "Admin",
                ConcurrencyStamp = "concurrency-admin-1234",
                PasswordHash = "AQAAAAEAACcQAAAAEGp0YX0QWhHi19pK9xQ1E9y+E..." // Static
            };

            var sellerUser = new User
            {
                Id = "seller-user-id",
                UserName = "seller@clothshop.com",
                NormalizedUserName = "SELLER@CLOTHSHOP.COM",
                Email = "seller@clothshop.com",
                NormalizedEmail = "SELLER@CLOTHSHOP.COM",
                EmailConfirmed = true,
                PhoneNumber = "8597628237",
                PhoneNumberConfirmed = true,
                Name = "Default Seller",
                Password = "Seller@123",
                CreatedAt = new DateTime(2025, 4, 5, 10, 0, 0),
                UpdatedAt = new DateTime(2025, 4, 5, 10, 0, 0),
                Address = "456 Seller St, Seller City, Seller State, 67890",
                Role = "Seller",
                SecurityStamp = "stamp-seller-5678",
                ConcurrencyStamp = "concurrency-seller-5678",
                PasswordHash = "AQAAAAEAACcQAAAAEGf4hTnUmN3FhM79pkLTTxXey..." // Static
            };

            var customerUser = new User
            {
                Id = "customer-user-id",
                UserName = "customer@clothshop.com",
                NormalizedUserName = "CUSTOMER@CLOTHSHOP.COM",
                Email = "customer@clothshop.com",
                NormalizedEmail = "CUSTOMER@CLOTHSHOP.COM",
                EmailConfirmed = true,
                PhoneNumber = "7777777777",
                PhoneNumberConfirmed = true,
                Name = "Default Customer",
                Password = "Customer@123",
                CreatedAt = new DateTime(2025, 4, 5, 10, 0, 0),
                UpdatedAt = new DateTime(2025, 4, 5, 10, 0, 0),
                Address = "789 Customer St, Customer City, Customer State, 54321",
                Role = "Customer",
                SecurityStamp = "stamp-customer-9012",
                ConcurrencyStamp = "concurrency-customer-9012",
                PasswordHash = "AQAAAAEAACcQAAAAEKkkJLpddw0k+1qJ+t9mAxVfA..." // Static
            };

            modelBuilder.Entity<User>().HasData(adminUser, sellerUser, customerUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "role-admin-id", UserId = "admin-user-id" },
                new IdentityUserRole<string> { RoleId = "role-seller-id", UserId = "admin-user-id" },
                new IdentityUserRole<string> { RoleId = "role-customer-id", UserId = "admin-user-id" },

                new IdentityUserRole<string> { RoleId = "role-seller-id", UserId = "seller-user-id" },
                new IdentityUserRole<string> { RoleId = "role-customer-id", UserId = "customer-user-id" }
            );
        }

    }

}
