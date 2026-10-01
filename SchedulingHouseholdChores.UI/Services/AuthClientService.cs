using System.Net.Http.Json;

namespace SchedulingHouseholdChores.UI.Services;

public record LoginResponse(int UserId, string Email, string Name, string Token);
public record RegisterResponse(int UserId, string Email, string Name, string Token);

public class AuthClientService
{
    private readonly HttpClient _httpClient;
    private const string StorageKey = "auth_token";
    private string? _token;

    public AuthClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResponse?> LoginAsync(string email, string password)
    {
        try
        {
            var request = new { email, password };
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (result != null)
                {
                    await SetTokenAsync(result.Token);
                    return result;
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login erro: {ex.Message}");
            return null;
        }
    }

    public async Task<RegisterResponse?> RegisterAsync(string name, string email, string password)
    {
        try
        {
            var request = new { name, email, password };
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
                if (result != null)
                {
                    await SetTokenAsync(result.Token);
                    return result;
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Register erro: {ex.Message}");
            return null;
        }
    }

    public async Task SetTokenAsync(string token)
    {
        _token = token;
        // TODO: Usar Blazored.LocalStorage para persistir
    }

    public async Task<string?> GetTokenAsync()
    {
        return _token;
    }

    public async Task LogoutAsync()
    {
        _token = null;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await GetTokenAsync();
        return !string.IsNullOrEmpty(token);
    }

    /// <summary>
    /// Adiciona o token autenticado ao HttpClient para requisições protegidas
    /// </summary>
    public async Task ConfigureAuthenticatedClient(HttpClient client)
    {
        var token = await GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}

