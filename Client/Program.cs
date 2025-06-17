using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RealWorldApp.Client;
using RealWorldApp.Client.Services;
using System.Net.Http.Headers;
using Blazored.SessionStorage;
using RealWorldApp.Shared.Models;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore(); // Add authorization services
builder.Services.AddScoped<CustomAuthStateProvider>(); // Register your custom provider
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddSingleton<BlockUIService>();

// Add JwtAuthorizationMessageHandler for attaching the JWT token
builder.Services.AddScoped<JwtAuthorizationMessageHandler>();

// Configure Authorized HttpClient with JWT Bearer Token Authorization and set the BaseAddress
builder.Services.AddHttpClient("AuthorizedClient", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);  // Set the BaseAddress here
})
.AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

// Register ApiService for ProductDto
builder.Services.AddScoped<ApiService<ProductDto>>(sp =>
    new ApiService<ProductDto>(
        sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthorizedClient"),
        "api/product"
    )
);

// Register ApiService for UserDto
builder.Services.AddScoped<ApiService<UserDto>>(sp =>
    new ApiService<UserDto>(
        sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthorizedClient"),
        "api/users" // <-- Use plural to match UsersController
    )
);

await builder.Build().RunAsync();
