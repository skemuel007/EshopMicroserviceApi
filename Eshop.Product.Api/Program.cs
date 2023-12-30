using Eshop.Infrastructure.Mongo;
using Eshop.Product.Api.Repositories;
using Eshop.Product.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

var dbInitializer = app.Services.GetService<IDatabaseInitializer>();
await dbInitializer.InitializeAsync();

app.MapControllers();

app.Run();