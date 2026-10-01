namespace SchedulingHouseholdChores.Services;

public interface IAuthService
{
    Task<AuthResponse?> RegisterAsync(string email, string password, string name);
    Task<AuthResponse?> LoginAsync(string email, string password);
}

public record AuthResponse(int UserId, string Email, string Name, string Token);
