using AutoMapper;
using ecommerce_api.Controllers.Base;
using ecommerce_api.Data.Specifications;
using ecommerce_api.Domain.Entities;
using ecommerce_api.Domain.Repositories;
using ecommerce_api.Dtos;
using ecommerce_api.Errors;
using ecommerce_api.Helpers;
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
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
    {
        var spec = new ProductsWithCategoriesAndBrandsSpecification(productParams);

        var countSpec = new ProductWithFiltersForCountSpecification(productParams);
        
        var totalItems = await _productRepo.CountAsync(countSpec);

        var products = await _productRepo.ListAsync(spec);

        var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
        
        return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize,totalItems,data));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
    {
        var spec = new ProductsWithCategoriesAndBrandsSpecification(id);
        
        var product = await _productRepo.GetEntityWithSpec(spec);

        if (product == null) return NotFound(new ApiResponse(404));
        
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