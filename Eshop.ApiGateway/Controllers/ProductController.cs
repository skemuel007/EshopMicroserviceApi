using Eshop.Infrastructure.Commands.Product;
using Eshop.Infrastructure.EventBus;
using Eshop.Infrastructure.Events.Product;
using Eshop.Infrastructure.Query.Product;
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
    private readonly IRequestClient<GetProductById> _requestClient;
    public ProductController(IBusControl bus,
        IOptionsMonitor<RabbitMqOption> rabbitMqOptions,
        IRequestClient<GetProductById> requestClient)
    {
        _bus = bus;
        _rabbitMqOption = rabbitMqOptions.CurrentValue;
        _requestClient = requestClient;
    }
    
    [HttpGet("{productId}")]
    public async Task<IActionResult> Get(string productId)
    {
        // await Task.CompletedTask;
        var productRequest = new GetProductById() { ProductId = productId };
        var response = await _requestClient.GetResponse<ProductCreated>(productRequest);
        // return Accepted("Product Created");
        return Accepted(response.Message);
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