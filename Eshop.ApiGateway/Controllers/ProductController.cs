using Eshop.Infrastructure.Commands.Product;
using Eshop.Infrastructure.EventBus;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Eshop.ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IBusControl _bus;
    private RabbitMqOption _rabbitMqOption;
    public ProductController(IBusControl bus,
        IOptionsMonitor<RabbitMqOption> rabbitMqOptions)
    {
        _bus = bus;
        _rabbitMqOption = rabbitMqOptions.CurrentValue;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await Task.CompletedTask;
        return Accepted("Product Created");
    }
    
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CreateProduct product)
    {
        // await Task.CompletedTask;
        var uri = new Uri($"{_rabbitMqOption.ConnectionString}/{_rabbitMqOption.CreateProductRoutingKey}");
        var endPoint = await _bus.GetSendEndpoint(uri);

        await endPoint.Send(product);
        return Accepted("Product Created");
    }
}