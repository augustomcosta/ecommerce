using ecommerce_api.Domain.Entities.Base;

namespace ecommerce_api.Domain.Models.OrderAggregate;

public class Order : ModelBase
{
    public string BuyerEmail { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public Address ShipToAddress { get; set; }
    public DeliveryMethod DeliveryMethod { get; set; }
    public IReadOnlyList<OrderItem> OrderItems { get; set; }
    public decimal Subtotal { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
<<<<<<< HEAD
    public PaymentIntent PaymentIntent { get; set; }
=======
>>>>>>> 2d3df45019c5f2127538fffff19f774047a9ac8b

    public Order(){}
    
    public Order(IReadOnlyList<OrderItem> orderItems,string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod,
         decimal subtotal, PaymentIntent paymentIntent)
    {
        BuyerEmail = buyerEmail;
        ShipToAddress = shipToAddress;
        DeliveryMethod = deliveryMethod;
        OrderItems = orderItems;
        Subtotal = subtotal;
        PaymentIntent = paymentIntent;
    }

    public decimal GetTotal()
    {
        return Subtotal + DeliveryMethod.Price;
    }
    
}