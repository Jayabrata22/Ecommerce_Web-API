using AutoMapper;
using Ecommerce.Business.Serviceinterface;
using Ecommerce.DataAccess.Implentation.Seller;
using Ecommerce.DataAccess.Interface.Customer;
using Ecommerce.DataAccess.Interface.Seller;
using Ecommerce.Models.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.ServiceRepository
{
    public class OrderServiceRepository : IOrderServiceInterface
    {
        private readonly ICustomerOrderInterface _customerOrderInterface;
        private readonly IOrderInterface _orderInterface;
        private readonly IMapper _mapper;

        public OrderServiceRepository(ICustomerOrderInterface customerOrderInterface , IOrderInterface orderInterface,IMapper mapper)
        {
            _customerOrderInterface = customerOrderInterface;
            _orderInterface = orderInterface;
            _mapper = mapper;
        }
        public async Task<OrderDto> CreateOrderAsync(string userId)
        => _mapper.Map<OrderDto>(await _customerOrderInterface.CreateOrderAsync(userId));

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(string userId)
            => _mapper.Map<IEnumerable<OrderDto>>(await _customerOrderInterface.GetOrdersByUserAsync(userId));

        public async Task<OrderDto> GetOrderByIdAsync(int orderId)
            => _mapper.Map<OrderDto>(await _customerOrderInterface.GetOrderByIdAsync(orderId));

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
            => _mapper.Map<IEnumerable<OrderDto>>(await _orderInterface.GetAllOrdersAsync());

        public async Task UpdateOrderStatusAsync(int orderId, string status)
            => await _orderInterface.UpdateOrderStatusAsync(orderId, status);
    }
}

