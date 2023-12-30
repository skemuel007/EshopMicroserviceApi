using Eshop.Infrastructure.Commands.Product;
using Eshop.Infrastructure.Events.Product;

namespace Eshop.Product.DataProvider.Repositories;

public interface IProductRepository
{
    Task<ProductCreated> GetProduct(string productId);
    Task<ProductCreated> AddProduct(CreateProduct product);
}