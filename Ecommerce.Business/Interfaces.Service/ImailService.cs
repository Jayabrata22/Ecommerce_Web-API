using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Interfaces.Service
{
    public interface ImailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
