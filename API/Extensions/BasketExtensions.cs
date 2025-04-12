using System;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class BasketExtensions
{
  public static BasketDto ToDto(this Basket basket)
  {
    return new BasketDto
    {
      BasketId = basket.BasketId,
      ClientSecret = basket.ClientSecret,
      PaymentIntentId = basket.PaymentIntentId,
      Items = basket.Items.Select(item => new BasketItemDto
      {
        ProductId = item.Product.Id,
        Name = item.Product.Name,
        Price = item.Product.Price,
        Brand = item.Product.Brand,
        Type = item.Product.Type,
        Quantity = item.Quantity,
        PictureUrl = item.Product.PictureUrl
      }).ToList()
    };
  }

  public static async Task<Basket> GetBasketWithItems(this IQueryable<Basket> query, string? basketId)
  {
    return await query
      .Include(b => b.Items)
      .ThenInclude(b => b.Product)
      .FirstOrDefaultAsync(b => b.BasketId == basketId) ?? throw new Exception("Basket not found");
  }
}
