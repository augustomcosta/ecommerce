using Core.Specifications;
using Domain.Models;
using ecommerce_api.Data.UnityOfWork.Interfaces;
using ecommerce_api.Domain.Models.OrderAggregate;
using ecommerce_api.Domain.Repositories.Interfaces;
using ecommerce_api.Domain.Services.Interfaces;
using Stripe;
using PaymentIntent = Stripe.PaymentIntent;

namespace ecommerce_api.Domain.Services;

public class PaymentService : IPaymentService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IConfiguration _config;
    
    public PaymentService(IBasketRepository basketRepository, IUnityOfWork unityOfWork,
        IConfiguration config)
    {
        _basketRepository = basketRepository;
        _unityOfWork = unityOfWork;
        _config = config;
    }
    public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
    {
        StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

        var basket = await _basketRepository.GetBasketAsync(basketId);

        if (basket == null) return null;
        
        var shippingPrice = 0m;
        
        if (basket.DeliveryMethodId.HasValue)
        {
            var deliveryMethod =
                await _unityOfWork.Repository<DeliveryMethod>().GetByIdAsync((int)basket.DeliveryMethodId);
            shippingPrice = deliveryMethod.Price;
        }

        foreach (var item in basket.Items)
        {
            var productItem = await _unityOfWork.Repository<Entities.Product>().GetByIdAsync(item.Id);
            if (item.Price != productItem.Price)
            {
                item.Price = productItem.Price;
            }
        }

        var service = new PaymentIntentService();

        PaymentIntent intent;

        if (string.IsNullOrEmpty(basket.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };
            intent = await service.CreateAsync(options);
            basket.PaymentIntentId = intent.Id;
            basket.ClientSecret = intent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long) basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long) shippingPrice * 100
            };
            await service.UpdateAsync(basket.PaymentIntentId, options);
        }

        await _basketRepository.UpdateBasketAsync(basket);

        return basket;
    }

    public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
    {
        var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
        var order = await _unityOfWork.Repository<Order>().GetEntityWithSpec(spec);

        if (order == null) return null;
        
        order.Status = OrderStatus.PaymentReceived;
        await _unityOfWork.Complete();
        
        return order;
    }

    public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
    {
        var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
        var order = await _unityOfWork.Repository<Order>().GetEntityWithSpec(spec);

        if (order == null) return null;
        
        order.Status = OrderStatus.PaymentFailed;
        await _unityOfWork.Complete();
        
        return order;
    }
}