global using Microsoft.EntityFrameworkCore;
global using BlazorECommerce.Shared;
global using BlazorECommerce.Server.Data;
global using BlazorECommerce.Server.Services.ProductService;
global using BlazorECommerce.Server.Services.CategoryService;
global using BlazorECommerce.Server.Services.CartService;
global using BlazorECommerce.Server.Services.AuthService;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var jiebaNetConfigPath = builder.Configuration.GetValue<string>("JiebaConfigFileDir");
if (jiebaNetConfigPath is not null)
{
    JiebaNet.Segmenter.ConfigManager.ConfigFileBaseDir = Path.IsPathRooted(jiebaNetConfigPath)
        ? jiebaNetConfigPath
        : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jiebaNetConfigPath);
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwaggerUI();
    app.UseSwagger();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
