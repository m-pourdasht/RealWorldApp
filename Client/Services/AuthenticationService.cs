using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RealWorldApp.Shared.Models;

namespace RealWorldApp.Client.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Register(Registeration model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", model);
            return response.IsSuccessStatusCode ? "Registration successful" : "Error: " + await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Login(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", model);
            return response.IsSuccessStatusCode ? "Login successful" : "Invalid credentials";
        }
    }
}
