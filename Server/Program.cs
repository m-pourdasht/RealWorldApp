
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RealWorldApp.Server.Data;
using System.Text;
using RealWorldApp.Client.Services;
//using RealWorldApp.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Add DbContext with SQL Server connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Authentication and JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddHttpClient(); // Add HttpClient support in the server

// Register your services here
builder.Services.AddScoped<ProductService>(); // Register the ProductService

// Add Controllers and Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

// Authentication must come before Authorization
app.UseAuthentication(); // JWT Authentication
app.UseAuthorization();  // Authorization for controllers and pages

// Map Razor pages and controllers
app.MapRazorPages();
app.MapControllers();

// Fallback to the Blazor client-side app for unknown routes
app.MapFallbackToFile("index.html");

app.Run();