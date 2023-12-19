using ecommerce_api.Domain.Entities.Base;

namespace ecommerce_api.Domain.Models.OrderAggregate;

public class DeliveryMethod : ModelBase
{
    public string ShortName { get; set; }
    public string DeliveryTime { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}