using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Services.Implementations;
using WebAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly);
});

// Database
var myConnectionString = builder.Configuration.GetConnectionString("MyConnectString");
builder.Services.AddDbContext<CoffeeShopDbContext>(option => option.UseSqlServer(myConnectionString));

// Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

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
