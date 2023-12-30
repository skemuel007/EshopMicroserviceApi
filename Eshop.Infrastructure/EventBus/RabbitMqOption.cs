namespace Eshop.Infrastructure.EventBus;

public class RabbitMqOption
{
    public string ConnectionString { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string CreateProductRoutingKey { get; set; }
}