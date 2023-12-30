using Eshop.Infrastructure.Commands.Product;
using Eshop.Infrastructure.Events.Product;
using MongoDB.Driver;

namespace Eshop.Product.Api.Repositories;

public interface IProductRepository
{
    Task<ProductCreated> GetProduct(Guid productId);
    Task<ProductCreated> AddProduct(CreateProduct product);
}

public class ProductRepository : IProductRepository
{
    private readonly IMongoDatabase _mongoDatabase;
    private IMongoCollection<CreateProduct> _collection;
    public ProductRepository(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
        _collection = _mongoDatabase.GetCollection<CreateProduct>("product");
    }
    public async Task<ProductCreated> GetProduct(Guid productId)
    {
        //var product = _collection.AsQueryable().Where(x => x.Id == productId).FirstOrDefault();
        var product = _collection.AsQueryable().FirstOrDefault(x => x.Id == productId);
        if (product == null)
            throw new Exception("Product not found.");
        return await Task.FromResult(new ProductCreated() { Id = product.Id, Name = product.Name, CreatedAt = DateTime.UtcNow });
    }

    public async Task<ProductCreated> AddProduct(CreateProduct product)
    {
        await _collection.InsertOneAsync(product);
        return new ProductCreated() { Id = product.Id, Name = product.Name, CreatedAt = DateTime.UtcNow };
    }
}