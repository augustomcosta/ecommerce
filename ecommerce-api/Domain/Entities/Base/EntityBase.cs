using System.ComponentModel.DataAnnotations;

namespace ecommerce_api.Domain.Entities.Base;

public class EntityBase
{
    [Key]
    public int Id { get; set; }
}