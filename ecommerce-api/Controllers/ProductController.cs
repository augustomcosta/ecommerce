using ecommerce_api.Data.Context;
using ecommerce_api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAllProducts()
    {
        var products = await _context.Products.ToListAsync();
        if (products is null)
        {
            return NotFound();
        }
        return products;
    }

    [HttpGet]
    public async Task<ActionResult<Product>> GetById(int id)
    {
       return await _context.Products.FindAsync(id);
    }
    
}