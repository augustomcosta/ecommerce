using ecommerce_api.Domain.Entities;
using ecommerce_api.Domain.Specifications;

namespace ecommerce_api.Data.Specifications;

public class ProductsWithCategoriesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithCategoriesAndBrandsSpecification(ProductSpecParams productParams) 
        : base(x => 
              (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains
              (productParams.Search)) && 
              (!productParams.BrandId.HasValue || x.BrandId == productParams.BrandId) && 
              (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId))
    {
        AddInclude(p => p.Category);
        AddInclude(p => p.Brand);
        AddOrderBy(p => p.Name);
        ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            switch (productParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                
                default:
                    AddOrderBy(n => n.Name);
                    break;
            }
        }
    }

    public ProductsWithCategoriesAndBrandsSpecification(int id) 
        : base(x => x.Id == id)
    {
        AddInclude(p => p.Category);
        AddInclude(p => p.Brand);
    }
}