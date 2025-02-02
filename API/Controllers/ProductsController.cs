using System;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository productRepository) : ControllerBase
{

 [HttpGet]
 public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
 {
  return Ok(await productRepository.GetProductsAsync()); 
 }

 [HttpGet("{id:int}")]
 public async Task<ActionResult<Product>> GetProduct(int id)
 {
  var product = await productRepository.GetProductByIdAsync(id);
  if(product == null) {
    return NotFound();
  };
  return product ;
 }

 [HttpPost]
 public async Task<ActionResult<Product>> CreateProduct(Product product)
 {
  productRepository.AddProduct(product);
  if(await productRepository.SaveChangesAsync())
  {
    return CreatedAtAction("GetProduct", new {id = product.Id}, product);
  }
  return BadRequest("Error creating product");
 }

 [HttpPut("{id:int}")]
  public async Task<ActionResult> UpdateProduct(int id, Product product) 
  {
    if(product.Id != id || !ProductExists(id)) 
    {
      return NotFound("Product does not exist");
    }

    productRepository.UpdateProduct(product);

    if(await productRepository.SaveChangesAsync())
    {
      return NoContent();
    }
    return BadRequest("Problem updating th product");
  }




 [HttpDelete("{id:int}")]
 public async Task<ActionResult> DeleteProduct(int id)
 {
    var product = await productRepository.GetProductByIdAsync(id);
    if(product == null) return NotFound("product not found");

    productRepository.DeleteProduct(product);
    
     if(await productRepository.SaveChangesAsync())
    {
      return NoContent();
    }
    return BadRequest("Error updating th product");
 }

   private bool ProductExists(int id) {
    return productRepository.ProductExists(id);
  }
}
