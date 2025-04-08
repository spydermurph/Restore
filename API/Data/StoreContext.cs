using System;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class StoreContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
  public required DbSet<Product> Products { get; set; }
  public required DbSet<Basket> Baskets { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<IdentityRole>().HasData(
      new IdentityRole
      {
        Id = "0f9ac11f-99d9-4801-a59d-fa4dc690a035",
        Name = "Member",
        NormalizedName = "MEMBER"
      },
      new IdentityRole
      {
        Id = "b4d242c9-091b-41ef-80f3-c2b87eadaac8",
        Name = "Admin",
        NormalizedName = "ADMIN"
      }
    );
  }
}
