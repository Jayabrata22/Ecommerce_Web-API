using AutoMapper;
using Ecommerce.Business.Serviceinterface;
using Ecommerce.DataAccess.Interface;
using Ecommerce.Models.DTO;
using Ecommerce.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.ServiceRepository
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
        }

        public async Task SubmitReviewAsync(ReviewDto reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            review.ReviewDate = DateTime.UtcNow;
            await _reviewRepo.SubmitReviewAsync(review);
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync(int productId)
        {
            var reviews = await _reviewRepo.GetReviewsByProductIdAsync(productId);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }
    }

}
