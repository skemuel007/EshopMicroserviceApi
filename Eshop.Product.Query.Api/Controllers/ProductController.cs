using Eshop.Product.DataProvider.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Product.Query.Api.Controllers;

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
}