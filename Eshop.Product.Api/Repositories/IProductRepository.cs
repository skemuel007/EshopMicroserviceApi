using Eshop.Infrastructure.Commands.Product;
using Eshop.Infrastructure.Events.Product;

namespace Eshop.Product.Api.Repositories;

public interface IProductRepository
{
    Task<ProductCreated> GetProduct(string productId);
    Task<ProductCreated> AddProduct(CreateProduct product);
}