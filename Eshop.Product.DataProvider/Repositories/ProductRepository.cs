using Eshop.Infrastructure.Commands.Product;
using Eshop.Infrastructure.Events.Product;
using MongoDB.Driver;

namespace Eshop.Product.DataProvider.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoDatabase _mongoDatabase;
    private IMongoCollection<CreateProduct> _collection;
    public ProductRepository(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
        _collection = _mongoDatabase.GetCollection<CreateProduct>("Product", null);
    }
    public async Task<ProductCreated> GetProduct(string productId)
    {
        //var product = _collection.AsQueryable().Where(x => x.Id == productId).FirstOrDefault();
        var product = _collection.AsQueryable().FirstOrDefault(x => x.ProductId == productId);
        if (product == null)
            throw new Exception("Product not found.");
        return await Task.FromResult(new ProductCreated() { ProductId = product.ProductId, ProductName = product.ProductName, CreatedAt = DateTime.UtcNow });
    }

    public async Task<ProductCreated> AddProduct(CreateProduct product)
    {
        await _collection.InsertOneAsync(product);
        return new ProductCreated() { ProductId = product.ProductId, ProductName = product.ProductName, CreatedAt = DateTime.UtcNow };
    }
}