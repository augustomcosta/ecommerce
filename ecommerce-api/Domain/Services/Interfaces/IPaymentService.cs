using Domain.Models;

namespace ecommerce_api.Domain.Services.Interfaces;

public interface IPaymentService
{
    Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
}