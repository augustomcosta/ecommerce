using Domain.Models;
using ecommerce_api.Controllers.Base;
using ecommerce_api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers;

public class BasketController : BaseController
{
    private readonly IBasketRepository _basketRepository;

    public BasketController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasketId(string id)
    {
        var basket = await _basketRepository.GetBasketAsync(id);

        return Ok(basket ?? new CustomerBasket(id));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
    {
        var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);

        return Ok(updatedBasket);
    }

    [HttpDelete]
    public async Task DeleteBasket(string id)
    {
        await _basketRepository.DeleteBasketAsync(id);
    }
}