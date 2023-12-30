namespace Eshop.Infrastructure.Commands.Product;

public class CreateProduct
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public Guid CategoryId { get; set; }
}