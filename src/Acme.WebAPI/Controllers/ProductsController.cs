using Acme.Models.Entity;
using Acme.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Acme.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase // Core MVC Features
{
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        return Ok(await productService.GetAllProductsAsync());
    }

    // GET: api/Products/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await productService.GetProductByIdAsync(id);

        if (product == null)
            return StatusCode(StatusCodes.Status404NotFound, "Product not found");

        return Ok(product);
    }

    // PUT puts a file or resource at a specific URI, and exactly at that URI.
    // If there's already a file or resource at that URI, PUT replaces that file or resource.
    // If there is no file or resource there, PUT creates one. PUT is idempotent,
    // but paradoxically PUT responses are not cacheable.
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutProduct(int id, [FromBody] Product product)
    {
        if (id != product.Id)
            return BadRequest("ID in URL must match ID in the request body.");

        var updatedProduct = await productService.UpdateProductAsync(product);
        if (updatedProduct == null)
            return NotFound();

        // Standard practice for a successful PUT where the resource is updated
        return NoContent();
    }
}