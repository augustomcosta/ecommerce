using ecommerce_api.Domain.Entities;
using ecommerce_api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IGenericRepository<Product> _productRepo;

    private readonly IGenericRepository<ProductCategory> _categoryRepo;

    private readonly IGenericRepository<ProductBrand> _brandRepo;
    public ProductController(IGenericRepository<Product> productRepo, 
                            IGenericRepository<ProductCategory> categoryRepo,
                            IGenericRepository<ProductBrand> brandRepo)
                            {
                                _productRepo = productRepo;
                                _categoryRepo = categoryRepo;
                                _brandRepo = brandRepo;
                            }   

    [HttpGet("products")]
    public async Task<ActionResult> GetAllProducts()
    {
        var products = await _productRepo.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        return await _productRepo.GetByIdAsync(id);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _brandRepo.GetAllAsync());
    }
    
    [HttpGet("categories")]
    public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetProductCategories()
    {
        return Ok(await _categoryRepo.GetAllAsync());
    }
}