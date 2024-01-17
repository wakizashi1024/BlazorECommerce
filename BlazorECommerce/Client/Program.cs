global using System.Net.Http.Json;
global using BlazorECommerce.Shared;
global using Microsoft.AspNetCore.Components.Authorization;
global using BlazorECommerce.Client.Services.CategoryService;
global using BlazorECommerce.Client.Services.ProductService;
global using BlazorECommerce.Client.Services.CartService;
global using BlazorECommerce.Client.Services.AuthService;
global using BlazorECommerce.Client.Services.OrderService;
global using BlazorECommerce.Client.Services.UserInfoService;

using BlazorECommerce.Client;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IShipService, ShipService>();

await builder.Build().RunAsync();
