using Ecommerce.Business.ServiceRepository;
using Ecommerce.DataAccess.Implentation.Seller;
using Ecommerce.DataAccess.Interface;
using Ecommerce.DataAccess.Interface.Seller;
using Ecommerce.Models.DTO;
using Ecommerce.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.CommonService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly INotificationService _notificationService;
        private readonly IOrderInterface _orderRepository;

        public PaymentService(
            IPaymentRepository paymentRepository,
            INotificationService notificationService,
            IOrderInterface orderRepository)
        {
            _paymentRepository = paymentRepository;
            _notificationService = notificationService;
            _orderRepository = orderRepository;
        }

        public async Task ProcessPaymentAsync(PaymentDto dto)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = dto.OrderId,
                UserId = dto.UserId,
                PaymentDate = dto.PaymentDate,
                Amount = dto.Amount,
                PaymentMethod = dto.PaymentMethod,
                Status = dto.Status
            };

            await _paymentRepository.AddPaymentAsync(payment);

            // ✅ Get the sellerId related to the Order
            var sellerId = await _orderRepository.GetSellerIdFromOrderAsync(dto.OrderId);
            await _notificationService.SendOrderPaymentNotificationToSeller(sellerId, dto.OrderId.ToString(), dto.Amount);
        }

        public async Task<IEnumerable<Payment>> GetCustomerPayments(string userId)
        {
            return await _paymentRepository.GetPaymentsByUserAsync(userId);
        }

        public async Task<IEnumerable<Payment>> GetSellerPayments(string sellerId)
        {
            return await _paymentRepository.GetPaymentsBySellerAsync(sellerId);
        }
    }

}
