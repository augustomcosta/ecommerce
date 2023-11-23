using System.ComponentModel.DataAnnotations;
using ecommerce_api.Domain.Entities;

namespace ecommerce_api.Dtos;

public class ProductToReturnDto
{
    public int Id { get; set; }
    
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

    public string? Category { get; set; }

    public string? Brand { get; set; }
   
}