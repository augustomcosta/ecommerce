using ecommerce_api.Domain.Entities.Base;

namespace ecommerce_api.Domain.Models.OrderAggregate;

public class ProductItemOrdered : ModelBase
{
    public ProductItemOrdered()
    {
    }
    
    public ProductItemOrdered(int productItemId, string productName, string imageUrl)
    {
        ProductItemId = productItemId;
        ProductName = productName;
        ImageUrl = imageUrl;
    }

    public int ProductItemId { get; set; }
    public string ProductName { get; set; }
    public string ImageUrl { get; set; }
}