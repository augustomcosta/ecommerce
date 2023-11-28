using ecommerce_api.Domain.Entities;
using ecommerce_api.Domain.Specifications;

namespace ecommerce_api.Data.Specifications;

public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
{
    public ProductWithFiltersForCountSpecification(ProductSpecParams productParams) : base(x =>
    (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains
    (productParams.Search)) &&     
    (!productParams.BrandId.HasValue || x.BrandId == productParams.BrandId) && 
    (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId))
    { }
}