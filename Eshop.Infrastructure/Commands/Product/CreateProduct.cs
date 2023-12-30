using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eshop.Infrastructure.Commands.Product;

public class CreateProduct
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    // public object _Id { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public float ProductPrice { get; set; }
    public Guid CategoryId { get; set; }
}