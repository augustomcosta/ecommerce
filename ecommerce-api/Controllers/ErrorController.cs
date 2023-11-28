using ecommerce_api.Controllers.Base;
using ecommerce_api.Errors;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers;

[Route("errors/{code}")]
public class ErrorController : BaseController
{
    [HttpGet]
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}