using Eshop.Infrastructure.Commands.Product;
using Eshop.Infrastructure.Events.Product;
using Eshop.Product.DataProvider.Repositories;

namespace Eshop.Product.DataProvider.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ProductCreated> GetProduct(string productId)
    {
        var product = await _productRepository.GetProduct(productId);
        return product;
    }

    public async Task<ProductCreated> AddProduct(CreateProduct product)
    {
        return await _productRepository.AddProduct(product: product);
    }
}