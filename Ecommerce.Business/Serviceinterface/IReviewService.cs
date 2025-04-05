using Ecommerce.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Serviceinterface
{
    public interface IReviewService
    {
        Task SubmitReviewAsync(ReviewDto reviewDto);
        Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync(int productId);
    }

}
