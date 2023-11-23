using ecommerce_api.Data.Context;
using ecommerce_api.Domain.Entities;
using ecommerce_api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api.Data.RepositoriesImpl;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .ToListAsync();
        return products;
    }

    public async Task<Product> GetByIdAsync(int? id)
    {
        return await _context
            .Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        return await _context.Brands.ToListAsync();
    }

    public async Task<IReadOnlyList<ProductCategory>> GetProductCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }
}