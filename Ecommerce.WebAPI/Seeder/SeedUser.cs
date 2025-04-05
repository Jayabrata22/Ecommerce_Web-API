using Ecommerce.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace WebApi.Seeder
{
    public static class SeedUser
    {
        public static async Task SeedSellerAndCustomerAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // --- Seed Seller ---
            string sellerEmail = "seller@clothshop.com";
            string sellerPhone = "8888888888";
            string sellerPassword = "Seller@123";

            var seller = await userManager.FindByEmailAsync(sellerEmail);
            if (seller == null)
            {
                var newSeller = new User
                {
                    UserName = sellerEmail,
                    Email = sellerEmail,
                    PhoneNumber = sellerPhone,
                    Name = "Default Seller",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(newSeller, sellerPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newSeller, "Seller");
                }
            }

            // --- Seed Customer ---
            string customerEmail = "customer@clothshop.com";
            string customerPhone = "7777777777";
            string customerPassword = "Customer@123";

            var customer = await userManager.FindByEmailAsync(customerEmail);
            if (customer == null)
            {
                var newCustomer = new User
                {
                    UserName = customerEmail,
                    Email = customerEmail,
                    PhoneNumber = customerPhone,
                    Name = "Default Customer",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(newCustomer, customerPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newCustomer, "Customer");
                }
            }
        }
    }
}
