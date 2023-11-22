using System.ComponentModel.DataAnnotations;
using ecommerce_api.Domain.Entities.Base;

namespace ecommerce_api.Domain.Entities;

public class Product : ModelBase
{
    [Required]
    [StringLength(50)]
    [MinLength(5)]
    public string? Name { get; set; }
    
    
}