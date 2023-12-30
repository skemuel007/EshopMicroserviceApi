using Eshop.Infrastructure.EventBus;
using Eshop.Infrastructure.Mongo;
using Eshop.Product.Api.Handlers;
using Eshop.Product.Api.Repositories;
using Eshop.Product.Api.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<CreateProductHandler>();

var rabbitMq = new RabbitMqOption();
builder.Configuration.GetSection("RabbitMq").Bind(rabbitMq);

builder.Services.AddMassTransit(x =>
{
    // https://medium.com/@bantyder/how-to-use-masstransit-8-0-13-rabbitmq-with-aspnetcore-7e199998b92d
    x.UsingRabbitMq((ctx,cfg) =>
    {
        x.AddConsumer<CreateProductHandler>();
        cfg.Host(new Uri(rabbitMq.ConnectionString),"/" , hostCfg => 
        { 
            hostCfg.Username(rabbitMq.Username);
            hostCfg.Password(rabbitMq.Password); 
        }); 
        
        cfg.ReceiveEndpoint("Create_Product", endpoint =>
        {
            endpoint.PrefetchCount = 16;
            endpoint.UseMessageRetry(retryConfig => { retryConfig.Interval(2, 100); });
            endpoint.ConfigureConsumer<CreateProductHandler>(ctx);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

var busControl = app.Services.GetService<IBusControl>();
await busControl.StartAsync();

var dbInitializer = app.Services.GetService<IDatabaseInitializer>();
await dbInitializer.InitializeAsync();

app.MapControllers();

app.Run();