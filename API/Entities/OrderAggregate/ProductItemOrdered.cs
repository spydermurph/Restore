using System;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.OrderAggregate;

// This class represents an item that has been ordered in the context of an order aggregate.
// It contains properties that describe the product being ordered, such as its ID, name, and picture URL.
// The class is used to encapsulate the details of a product item within an order, allowing for better organization and management of order-related data.
// The properties are marked with the 'required' keyword, indicating that they must be provided when creating an instance of this class.
[Owned]
public class ProductItemOrdered
{
  public int ProductId { get; set; }
  public required string Name { get; set; }
  public required string PictureUrl { get; set; }
}
