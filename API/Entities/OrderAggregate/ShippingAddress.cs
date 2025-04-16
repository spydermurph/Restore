using System;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.OrderAggregate;

// This class is a simplified version of the Address class, specifically for shipping purposes in the order aggregate context.
// It is used to represent the shipping address of an order in the OrderAggregate namespace.
// The class is marked with the [Owned] attribute, indicating that it is an owned entity type in Entity Framework Core.
// Owned entity types are typically used to represent complex types that are part of another entity, and they do not have their own identity.
[Owned]
public class ShippingAddress
{
  public required string Name { get; set; }
  public required string Line1 { get; set; }
  public string? Line2 { get; set; }
  public required string City { get; set; }
  public required string State { get; set; }
  [JsonPropertyName("postal_code")]
  public required string PostalCode { get; set; }
  public required string Country { get; set; }
}
