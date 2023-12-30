using Eshop.Infrastructure.Commands.Product;
using Eshop.Infrastructure.Events.Product;

namespace Eshop.Product.Api.Services;

public interface IProductService
{
    Task<ProductCreated> GetProduct(Guid productId);
    Task<ProductCreated> AddProduct(CreateProduct product);
}

public class ProductService : IProductService
{
    public Task<ProductCreated> GetProduct(Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task<ProductCreated> AddProduct(CreateProduct product)
    {
        throw new NotImplementedException();
    }
}