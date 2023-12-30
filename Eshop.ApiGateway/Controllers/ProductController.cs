using Eshop.Infrastructure.Commands.Product;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await Task.CompletedTask;
        return Accepted("Product Created");
    }
    
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromForm] CreateProduct product)
    {
        await Task.CompletedTask;
        return Accepted("Product Created");
    }
}