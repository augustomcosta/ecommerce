using ecommerce_api.Data.Context;
using ecommerce_api.Domain.Entities;
using ecommerce_api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("products")]
    public async Task<ActionResult> GetAllProducts()
    {
        var products = await _repository.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _repository.GetProductBrandsAsync());
    }
    
    [HttpGet("categories")]
    public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetProductCategories()
    {
        return Ok(await _repository.GetProductCategoriesAsync());
    }
}