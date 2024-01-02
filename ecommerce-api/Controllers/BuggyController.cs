using ecommerce_api.Controllers.Base;
using ecommerce_api.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers;

public class BuggyController : BaseController
{
    private readonly AppDbContext _context;
    
    public BuggyController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("testauth")]
    [Authorize]
    public ActionResult<string> GetSecretText()
    {
        return "secret stuff";
    }

    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
        var product = _context.Products.Find(42);

        if (product is null)
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpGet("servererror")]
    public ActionResult GetServerErrorRequest()
    {
        var product = _context.Products.Find(42);

        var productToReturn = product.ToString();

        return Ok();
    }

    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest();
    }
    
    [HttpGet("badrequest/{id}")]
    public ActionResult GetNotFoundRequest(int id)
    {
        return Ok();
    }
}