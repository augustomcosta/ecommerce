using AutoMapper;
using ecommerce_api.Controllers.Base;
using ecommerce_api.Data.Specifications;
using ecommerce_api.Domain.Entities;
using ecommerce_api.Domain.Repositories;
using ecommerce_api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers;

public class ProductController : BaseController
{
    private readonly IGenericRepository<Product> _productRepo;

    private readonly IGenericRepository<ProductCategory> _categoryRepo;

    private readonly IGenericRepository<ProductBrand> _brandRepo;

    private readonly IMapper _mapper;
    public ProductController(IGenericRepository<Product> productRepo, 
                            IGenericRepository<ProductCategory> categoryRepo,
                            IGenericRepository<ProductBrand> brandRepo,
                            IMapper mapper)
                            {
                                _productRepo = productRepo;
                                _categoryRepo = categoryRepo;
                                _brandRepo = brandRepo;
                                _mapper = mapper;
                            }   

    [HttpGet("products")]
    public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
    {
        var spec = new ProductsWithCategoriesAndBrandsSpecification();
        var products = await _productRepo.ListAsync(spec);
        return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
    {
        var spec = new ProductsWithCategoriesAndBrandsSpecification(id);

        var product = await _productRepo.GetEntityWithSpec(spec);

        return _mapper.Map<Product, ProductToReturnDto>(product);
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