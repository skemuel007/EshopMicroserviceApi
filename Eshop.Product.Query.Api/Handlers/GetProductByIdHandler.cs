using Eshop.Infrastructure.Commands.Product;
using Eshop.Infrastructure.Events.Product;
using Eshop.Infrastructure.Query.Product;
using Eshop.Product.DataProvider.Services;
using MassTransit;

namespace Eshop.Product.Query.Api.Handlers;

public class GetProductByIdHandler : IConsumer<GetProductById>
{
    private readonly IProductService _productService;
    public GetProductByIdHandler(IProductService productService)
    {
        _productService = productService;
    }
    public async Task Consume(ConsumeContext<GetProductById> context)
    {
        var product = await _productService.GetProduct(context.Message.ProductId);
        await context.RespondAsync<ProductCreated>(product);
    }
}