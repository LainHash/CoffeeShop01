using BlazorApp.Auth;
using BlazorApp.Components;
using BlazorApp.Services.Implementations;
using BlazorApp.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure HttpClient
builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"] ?? "https://localhost:7100");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Register API Services
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IAuthApiService, AuthApiService>();
builder.Services.AddScoped<IProductApiService, ProductApiService>();
builder.Services.AddScoped<IOrderApiService, OrderApiService>();
builder.Services.AddScoped<IReservationApiService, ReservationApiService>();
builder.Services.AddScoped<ITableApiService, TableApiService>();
builder.Services.AddScoped<ICategoryApiService, CategoryApiService>();
builder.Services.AddScoped<IDiscountApiService, DiscountApiService>();

// Register Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
        options.Cookie.Name = "CoffeeShopAuth";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
