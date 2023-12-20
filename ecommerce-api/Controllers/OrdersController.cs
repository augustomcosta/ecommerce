using System.Security.Claims;
using AutoMapper;
using ecommerce_api.Controllers.Base;
using ecommerce_api.Domain.Models.OrderAggregate;
using ecommerce_api.Domain.Services.Interfaces;
using ecommerce_api.Dtos;
using ecommerce_api.Errors;
using ecommerce_api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers;

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
        var email = HttpContext.User.RetrieveEmailFromPrincipal();

        var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

        var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

        if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

        return Ok(order);
    }
    

}