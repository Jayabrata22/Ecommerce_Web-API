using AutoMapper;
using Ecommerce.Business.Serviceinterface;
using Ecommerce.DataAccess.Implentation.Customer;
using Ecommerce.DataAccess.Interface.Customer;
using Ecommerce.Models.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.ServiceRepository
{
    public class CartServiceRepository : ICartServiceInterface
    {
        private readonly ICartInterface _cartInterface;
        private readonly IMapper _mapper;

        public CartServiceRepository(ICartInterface cartInterface,IMapper mapper)
        {
            _cartInterface = cartInterface;
            _mapper = mapper;
        }
        public async Task<CartDto> GetCartByUserIdAsync(string userId)
        => _mapper.Map<CartDto>(await _cartInterface.GetCartByUserIdAsync(userId));

        public async Task AddItemAsync(string userId, int productId, int quantity)
            => await _cartInterface.AddItemAsync(userId, productId, quantity);

        public async Task RemoveItemAsync(int cartItemId)
            => await _cartInterface.RemoveItemAsync(cartItemId);

        public async Task<CartDto> CreateCartAsync(string userId)
            => _mapper.Map<CartDto>(await _cartInterface.CreateCartAsync(userId));
    }
}
