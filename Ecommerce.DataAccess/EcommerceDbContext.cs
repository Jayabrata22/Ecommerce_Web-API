using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ecommerce.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet for each entity (tables in the database)
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        // Configuring entity relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User-Seller/Product Relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Products)
                .WithOne(p => p.Seller)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            // User-Buyer/Order Relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.Buyer)
                .HasForeignKey(o => o.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order-OrderItem Relationship
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            // Product-OrderItem Relationship
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            // Order-Payment Relationship (One-to-One)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne()
                .HasForeignKey<Payment>(p => p.OrderId);

            // Order-Shipping Relationship (One-to-One)
            modelBuilder.Entity<Shipping>()
                .HasOne(s => s.Order)
                .WithOne()
                .HasForeignKey<Shipping>(s => s.OrderId);

            // Product-Review Relationship
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany()
                .HasForeignKey(r => r.ProductId);

            // User-Review Relationship (Buyer giving review)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Buyer)
                .WithMany()
                .HasForeignKey(r => r.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cart-User Relationship
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Buyer)
                .WithOne()
                .HasForeignKey<Cart>(c => c.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // CartItem-Cart Relationship
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            // CartItem-Product Relationship
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId);
        }
    }
}
