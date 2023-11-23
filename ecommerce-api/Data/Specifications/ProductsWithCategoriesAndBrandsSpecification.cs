using ecommerce_api.Domain.Entities;
using ecommerce_api.Domain.Specifications;

namespace ecommerce_api.Data.Specifications;

public class ProductsWithCategoriesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithCategoriesAndBrandsSpecification()
    {
        AddInclude(p => p.Category);
        AddInclude(p => p.Brand);
    }

    public ProductsWithCategoriesAndBrandsSpecification(int id) 
        : base(x => x.Id == id)
    {
        AddInclude(p => p.Category);
        AddInclude(p => p.Brand);
    }
}