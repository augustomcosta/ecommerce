using ecommerce_api.Data.UnityOfWork.Interfaces;
using ecommerce_api.Domain.Entities;
using ecommerce_api.Domain.Models.OrderAggregate;
using ecommerce_api.Domain.Repositories.Interfaces;
using ecommerce_api.Domain.Services.Interfaces;

namespace ecommerce_api.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IBasketRepository _basketRepo;
    
    public OrderService(IUnityOfWork unityOfWork, IBasketRepository basketRepo)
    {
        _unityOfWork = unityOfWork;
        _basketRepo = basketRepo;
    }
    
    public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
    {
        var basket = await _basketRepo.GetBasketAsync(basketId);

        var items = new List<OrderItem>();

        foreach (var item in basket.Items)
        {
            var productItem = await _unityOfWork.Repository<Product>().GetByIdAsync(item.Id);
            var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.ImageUrl);
            var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
            items.Add(orderItem);
        }

        var deliveryMethod = await _unityOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

        var subTotal = items.Sum(item => item.Price * item.Quantity);

        var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subTotal);
        
        _unityOfWork.Repository<Order>().Add(order);

        var result = await _unityOfWork.Complete();

        if (result <= 0) return null;

        await _basketRepo.DeleteBasketAsync(basketId);
        
        return order;
    }

    public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
    {
        throw new NotImplementedException();
    }
}