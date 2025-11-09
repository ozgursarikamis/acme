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
}