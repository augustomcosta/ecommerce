using ecommerce_api.Data.Context;
using ecommerce_api.Domain.Entities;
using ecommerce_api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api.Data.RepositoriesImpl;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Product> Create(Product product)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }
    
    public async Task<Product> GetByIdAsync(int? id)
    {
        return await _context.Products.FindAsync(id);
    }

    public Task<Product> Update(Product product, int? id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> Delete(int? id)
    {
        throw new NotImplementedException();
    }
}