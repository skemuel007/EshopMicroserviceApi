using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eshop.Infrastructure.Commands.Product;

public class CreateProduct
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    // public object _Id { get; set; }
    public string ProductId { get; set; }
    
    // https://stackoverflow.com/questions/11557912/element-id-does-not-match-any-field-or-property-of-class
    [DataMember]
    [BsonElement("productName")]
    public string ProductName { get; set; }
    [DataMember]
    [BsonElement("productDescription")]
    public string ProductDescription { get; set; }
    [DataMember]
    [BsonElement("productPrice")]
    public float ProductPrice { get; set; }
    [DataMember]
    [BsonElement("categoryId")]
    public Guid CategoryId { get; set; }
}