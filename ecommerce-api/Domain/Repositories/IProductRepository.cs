using ecommerce_api.Domain.Entities;

namespace ecommerce_api.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int? id);
    Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
    Task<IReadOnlyList<ProductCategory>> GetProductCategoriesAsync();
    
}