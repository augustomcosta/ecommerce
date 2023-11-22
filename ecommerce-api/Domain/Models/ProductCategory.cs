using ecommerce_api.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ecommerce_api.Domain.Entities;

public class ProductCategory : ModelBase
{
    public string? Name { get; set; }
}