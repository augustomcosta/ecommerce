using ecommerce_api.Data.Specifications;
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

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

        return await _unityOfWork.Repository<Order>().ListAsync(spec);
    }

    public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

        return await _unityOfWork.Repository<Order>().GetEntityWithSpec(spec);
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
    {
        return await _unityOfWork.Repository<DeliveryMethod>().ListAllAsync();
    }

}