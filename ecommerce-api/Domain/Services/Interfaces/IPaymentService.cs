using Domain.Models;
using ecommerce_api.Domain.Models.OrderAggregate;

namespace ecommerce_api.Domain.Services.Interfaces;

public interface IPaymentService
{
    Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
    Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);
    Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
}