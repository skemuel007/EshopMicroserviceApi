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
        try
        {
            var product = _collection.AsQueryable<CreateProduct>().FirstOrDefault(x => x.ProductId == productId);
            if (product == null)
                throw new Exception("Product not found.");

            var fetchedProduct = new ProductCreated()
                { ProductId = product.ProductId, ProductName = product.ProductName, CreatedAt = DateTime.UtcNow };
            return await Task.FromResult(fetchedProduct);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ProductCreated> AddProduct(CreateProduct product)
    {
        await _collection.InsertOneAsync(product);
        return new ProductCreated() { ProductId = product.ProductId, ProductName = product.ProductName, CreatedAt = DateTime.UtcNow };
    }
}