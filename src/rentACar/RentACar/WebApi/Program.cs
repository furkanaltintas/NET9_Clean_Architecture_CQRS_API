using Application;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Persistence;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();


builder.Services.AddDistributedMemoryCache(); // InMemory (Yayýnladýðýmýz ortamýn)
//builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = "localhost:6379");


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//if (app.Environment.IsProduction())
app.ConfigureCustomExceptionMiddleware();


app.MapScalarApiReference();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
// https://localhost:7075/scalar/v1