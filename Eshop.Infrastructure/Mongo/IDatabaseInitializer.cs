namespace Eshop.Infrastructure.Mongo;

public interface IDatabaseInitializer
{
    Task InitializeAsync();
}