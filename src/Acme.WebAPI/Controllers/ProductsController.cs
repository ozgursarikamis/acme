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
}