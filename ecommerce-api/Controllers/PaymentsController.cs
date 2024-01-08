using Domain.Models;
using ecommerce_api.Controllers.Base;
using ecommerce_api.Domain.Models.OrderAggregate;
using ecommerce_api.Domain.Services.Interfaces;
using ecommerce_api.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using PaymentIntent = Stripe.PaymentIntent;

namespace ecommerce_api.Controllers;

public class PaymentsController : BaseController
{

    private const string WhSecret = "";
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentsController> _logger;
    
    public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }

    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
    {
        var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

        if (basket == null) return BadRequest(new ApiResponse(400, "Problem with the basket"));

        return basket;
    }

    [HttpPost("webhook")]
    public async Task<ActionResult> StripeWebhook()
    {
        var json = await new StreamReader(Request.Body).ReadToEndAsync();

        var stripeEvent = EventUtility.ConstructEvent(json,
            Request.Headers["Stripe-Signature"], WhSecret);

        PaymentIntent intent;
        Order order;

        switch (stripeEvent.Type)
        {
            case "payment_intent.succeeded":
                intent = (PaymentIntent)stripeEvent.Data.Object;
                _logger.LogInformation("Payment succeeded: ", intent.Id);
                order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                _logger.LogInformation("Order updated to payment received: ", order.Id);
                break;
            case "payment_intent.payment_failed":
                intent = (PaymentIntent)stripeEvent.Data.Object;
                _logger.LogInformation("Payment failed: ", intent.Id);
                order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                _logger.LogInformation("Order update to payment failed: ", order.Id);
                break;
        }

        return new EmptyResult();
    }
}