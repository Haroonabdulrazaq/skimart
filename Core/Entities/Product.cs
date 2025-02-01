using System;

namespace Core.Entities;

public class Product : BaseEntity
{
  // Set as not Null
  public required string Name { get; set; }
  public string Description { get; set; } = "";
  public decimal Price { get; set; }
  public required string PictureUrl { get; set; }
  public required string Type { get; set; }
  public required string Brand { get; set; }
  public int  QuatityInStock { get; set; }
}
