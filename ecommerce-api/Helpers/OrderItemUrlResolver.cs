using AutoMapper;
using ecommerce_api.Domain.Models.OrderAggregate;
using ecommerce_api.Dtos;

namespace ecommerce_api.Helpers;

public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
{
    private readonly IConfiguration _config;
    
    public OrderItemUrlResolver(IConfiguration config)
    {
        _config = config;
    }
    
    public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.ItemOrdered.ImageUrl))
        {
            return _config["ApiUrl"] + source.ItemOrdered.ImageUrl;
        }

        return null;
    }
}