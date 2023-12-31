using Eshop.Infrastructure.Commands.Product;
using Eshop.Infrastructure.Events.Product;

namespace Eshop.Product.DataProvider.Services;

public interface IProductService
{
    Task<ProductCreated> GetProduct(string productId);
    Task<ProductCreated> AddProduct(CreateProduct product);
}