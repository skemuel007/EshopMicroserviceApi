using Eshop.Infrastructure.EventBus;
using Eshop.Infrastructure.Mongo;
using Eshop.Product.DataProvider.Repositories;
using Eshop.Product.DataProvider.Services;
using Eshop.Product.Query.Api.Handlers;
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
builder.Services.AddScoped<GetProductByIdHandler>();

builder.Services.AddMongoDb(builder.Configuration);

var rabbitMq = new RabbitMqOption();
builder.Configuration.GetSection("RabbitMq").Bind(rabbitMq);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<GetProductByIdHandler>();
    // https://medium.com/@bantyder/how-to-use-masstransit-8-0-13-rabbitmq-with-aspnetcore-7e199998b92d
    x.UsingRabbitMq((ctx,cfg)=>
    { 
        cfg.Host(new Uri(rabbitMq.ConnectionString),"/" , hostCfg => 
        { 
            hostCfg.Username(rabbitMq.Username);
            hostCfg.Password(rabbitMq.Password); 
        }); 
        cfg.ConfigureEndpoints(ctx); 
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

app.MapControllers();

// https://github.com/CodeMazeBlog/CodeMazeGuides/tree/main/aspnetcore-webapi/MassTransitRabbitMQ
// https://code-maze.com/masstransit-rabbitmq-aspnetcore/
/*var busControl = app.Services.GetService<IBusControl>();
await busControl.StartAsync(new CancellationToken());*/

var dbInitializer = app.Services.GetService<IDatabaseInitializer>();
await dbInitializer.InitializeAsync();

app.Run();