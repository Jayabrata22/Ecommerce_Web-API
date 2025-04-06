using Ecommerce.DataAccess;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.CommonService
{
    public class SendMailOnLowInventory
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration configuration;

        public SendMailOnLowInventory(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        public async Task CheckLimitofInventory()
        {
            var currentInventoryForeach = dbContext.Inventories.Where(i => i.Quantity <= 10).Select(i => new
            {
                i.ProductId,
                i.Quantity,
                i.Product.Name,
                status = "Low Inventory"
            }).ToList();

            if (currentInventoryForeach.Any())
            {
                var list = new List<string>();
                foreach (var item in currentInventoryForeach)
                {
                    // Send email logic here
                    var list1 = ($"Product: {item.Name}, Quantity: {item.Quantity}, Status: {item.status}");
                    list.Add(list1);

                }
                await SendEmail(list);
            }
        }

        private async Task SendEmail(List<string> list)
        {
            // Implement your email sending logic here
            // For example, using SMTP or any email service provider
            string email = configuration["Emal:Tomail"];
            string subject = "Low Inventory Alert";
            string body = "The following products have low inventory:\n\n" + string.Join("\n", list);

            await SendEmailAsync(email, subject, body);
        }


        private Task SendEmailAsync(string email, string subject, string body)
        {
            var mail = configuration["Emal:mail"];
            var pw = configuration["Emal:password"];
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mail, pw)
            };
            var message = new MailMessage(mail, email)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false // set to true if you use HTML tags in body
            };
            return client.SendMailAsync(message);

            //return client.SendMailAsync(new MailMessage
            //    (from: mail,
            //    to: email,
            //    subject,
            //    body));

        }
    }
}
