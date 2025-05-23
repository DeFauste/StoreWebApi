using Microsoft.EntityFrameworkCore;
using Store.API.Services;
using Store.DataAccess.Postgress;
using Store.DataAccess.Postgress.Repositories;
using Store.DataAccess.Postgress.Repositories.impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
builder.Services.AddDbContext<StoreDbContext>(
        options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(StoreDbContext)));
        }
    );

builder.Services.AddScoped<IClientRepository, ClientRepositoryImpl>();
builder.Services.AddScoped<IProductRepository, ProductRepositoryImpl>();
builder.Services.AddScoped<IAddressRepository, AdressRepositoryImpl>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepositoryImpl>();
builder.Services.AddScoped<IImageRepository, ImageRepositoryImpl>();
builder.Services.AddScoped<ClientService, ClientService>();
builder.Services.AddScoped<ProductService, ProductService>();
builder.Services.AddScoped<SupplierService, SupplierService>();
builder.Services.AddScoped<ImageService, ImageService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

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

app.Run();
