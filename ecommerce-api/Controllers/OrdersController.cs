using System;
using System.Security.Claims;
using AutoMapper;
using ecommerce_api.Controllers.Base;
using ecommerce_api.Domain.Models.OrderAggregate;
using ecommerce_api.Domain.Services.Interfaces;
using ecommerce_api.Dtos;
using ecommerce_api.Errors;
using ecommerce_api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers;

[Authorize]
public class OrdersController : BaseController
{

    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
    {
        try {
        var email = HttpContext.User.RetrieveEmailFromPrincipal();

        var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

        var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address, orderDto.PaymentIntentId);

        if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

        return Ok(order);
        }

        catch(Exception ex) {
            Console.WriteLine(ex.InnerException.Message);
            throw;
        }
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
    {
        var email = HttpContext.User.RetrieveEmailFromPrincipal();

        var orders = await _orderService.GetOrdersForUserAsync(email);

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderById(int id)
    {
        var email = HttpContext.User.RetrieveEmailFromPrincipal();
        
        var order = await _orderService.GetOrderByIdAsync(id, email);

        if (order == null) return NotFound(new ApiResponse(404));

        return order;
    }

    [HttpGet("deliveryMethods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
    {
        return Ok(await _orderService.GetDeliveryMethodAsync());
    }
}