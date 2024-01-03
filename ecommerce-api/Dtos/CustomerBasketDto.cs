using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace ecommerce_api.Dtos;

public class CustomerBasketDto
{
    [Required]
    public string Id { get; set; }
    public List<BasketItemDto> Items { get; set; }
    public int? DeliveryMethodId { get; set; }
    public string? ClientSecret { get; set; }
    public string? PaymentIntentId { get; set; }
}