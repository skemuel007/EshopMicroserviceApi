using Eshop.Infrastructure.Commands.Product;
using Eshop.Product.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Product.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> Get(string productId)
    {
        var product = await _productService.GetProduct(productId);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateProduct product)
    {
        return Ok(await _productService.AddProduct(product: product));
    }
}