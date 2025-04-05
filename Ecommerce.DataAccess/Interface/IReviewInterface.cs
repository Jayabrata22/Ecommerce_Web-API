using Ecommerce.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Interface
{
    public interface IReviewRepository
    {
        Task SubmitReviewAsync(Review review);
        Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId);
    }

}
