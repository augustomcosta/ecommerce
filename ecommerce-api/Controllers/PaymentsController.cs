using Domain.Models;
using ecommerce_api.Controllers.Base;
using ecommerce_api.Domain.Services.Interfaces;
using ecommerce_api.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers;

public class PaymentsController : BaseController
{
    private readonly IPaymentService _paymentService;
    
    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
    {
        var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

        if (basket == null) return BadRequest(new ApiResponse(400, "Problem with the basket"));

        return basket;
    }
}