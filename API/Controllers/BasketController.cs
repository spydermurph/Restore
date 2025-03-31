using System;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class BasketController(StoreContext context) : BaseApiController
{
  [HttpGet]
  public async Task<ActionResult<BasketDto>> GetBasket()
  {
    var basket = await RetrieveBasket();

    if (basket == null)
    {
      return NoContent();
    }

    return basket.ToDto();
  }

  [HttpPost]
  public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
  {
    var basket = await RetrieveBasket();

    basket ??= CreateBakset();

    var product = await context.Products.FindAsync(productId);
    if (product == null)
    {
      return BadRequest("Problem adding product to basket");
    }

    basket.AddItem(product, quantity);

    var result = await context.SaveChangesAsync() > 0;

    if (result) return CreatedAtAction(nameof(GetBasket), basket.ToDto());

    return BadRequest("Problem updating basket");
  }



  [HttpDelete]
  public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
  {

    var basket = await RetrieveBasket();

    if (basket == null)
    {
      return BadRequest("Unable to retrieve basket");
    }

    basket.RemoveItem(productId, quantity);

    var result = await context.SaveChangesAsync() > 0;

    if (result)
    {
      return Ok();
    }

    return BadRequest("Problem removing item from basket");
  }

  private async Task<Basket?> RetrieveBasket()
  {
    var basket = await context.Baskets
      .Include(b => b.Items)
      .ThenInclude(b => b.Product)
      .FirstOrDefaultAsync(b => b.BasketId == Request.Cookies["basketId"]);

    return basket;
  }

  private Basket CreateBakset()
  {
    var basketId = Guid.NewGuid().ToString();
    var cookieOptions = new CookieOptions
    {
      IsEssential = true,
      Expires = DateTime.Now.AddDays(30),
      HttpOnly = true,
      SameSite = SameSiteMode.None,
      Secure = true
    };
    Response.Cookies.Append("basketId", basketId, cookieOptions);
    var basket = new Basket { BasketId = basketId };
    context.Baskets.Add(basket);
    return basket;
  }
}
