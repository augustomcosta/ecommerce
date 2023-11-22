using System.ComponentModel.DataAnnotations;
using ecommerce_api.Domain.Entities.Base;

namespace ecommerce_api.Domain.Entities;

public class Product : ModelBase
{
    [Required]
    [StringLength(50)]
    [MinLength(5)]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(200)]
    [MinLength(5)]
    public string? Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    [StringLength(200)]
    [MinLength(5)]
    public string? ImageUrl { get; set; }  
    
    public ProductCategory Category { get; set; }

    public int CategoryId { get; set; }

    public ProductBrand Brand { get; set; }

    public int BrandId { get; set; }
    
    
}