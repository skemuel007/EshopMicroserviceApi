using Eshop.Infrastructure.Commands.Product;
using Eshop.Infrastructure.Events.Product;
using Eshop.Product.Api.Repositories;

namespace Eshop.Product.Api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ProductCreated> GetProduct(string productId)
    {
        return await _productRepository.GetProduct(productId);
    }

    public async Task<ProductCreated> AddProduct(CreateProduct product)
    {
        return await _productRepository.AddProduct(product: product);
    }
}