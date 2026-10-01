using Microsoft.IdentityModel.Tokens;
using SchedulingHouseholdChores.Data;
using SchedulingHouseholdChores.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace SchedulingHouseholdChores.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly byte[] _key;
    private readonly ILogger<AuthService> _logger;
    private readonly IDefaultTasksService _defaultTasksService;

    public AuthService(AppDbContext context, byte[] key, ILogger<AuthService> logger, IDefaultTasksService defaultTasksService)
    {
        _context = context;
        _key = key;
        _logger = logger;
        _defaultTasksService = defaultTasksService;
    }

    public async Task<AuthResponse?> RegisterAsync(string email, string password, string name)
    {
        try
        {
            var normalizedEmail = email.ToLower().Trim();

            _logger.LogInformation($"Tentando registrar usuário: {normalizedEmail}");

            var existingUser = _context.Users.FirstOrDefault(u => u.Email.ToLower() == normalizedEmail);
            if (existingUser != null)
            {
                _logger.LogWarning($"Usuário já existe: {normalizedEmail}");
                return null;
            }

            _logger.LogInformation($"Usuário não encontrado, criando novo: {normalizedEmail}");

            var passwordHash = HashPassword(password);

            var user = new User
            {
                Email = normalizedEmail,
                PasswordHash = passwordHash,
                Name = name,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Usuário criado com sucesso: {normalizedEmail} (ID: {user.Id})");

            try
            {
                var defaultTasks = _defaultTasksService.GetDefaultTasks(user.Id);
                _context.RecurrentTasks.AddRange(defaultTasks);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Tarefas padrão criadas para usuário: {user.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao criar tarefas padrão para usuário {user.Id}: {ex.Message}", ex);
            }

            var token = GenerateJwtToken(user);
            return new AuthResponse(user.Id, user.Email, user.Name, token);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao registrar usuário: {ex.Message}", ex);
            return null;
        }
    }

    public async Task<AuthResponse?> LoginAsync(string email, string password)
    {
        var normalizedEmail = email.ToLower().Trim();

        _logger.LogInformation($"Tentando fazer login: {normalizedEmail}");

        var user = _context.Users.FirstOrDefault(u => u.Email.ToLower() == normalizedEmail);
        if (user == null)
        {
            _logger.LogWarning($"Usuário não encontrado: {normalizedEmail}");
            return null;
        }

        if (!VerifyPassword(password, user.PasswordHash))
        {
            _logger.LogWarning($"Senha incorreta para usuário: {normalizedEmail}");
            return null;
        }

        _logger.LogInformation($"Login bem-sucedido: {normalizedEmail}");

        var token = GenerateJwtToken(user);
        return new AuthResponse(user.Id, user.Email, user.Name, token);
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
            {
                new System.Security.Claims.Claim("nameid", user.Id.ToString()),
                new System.Security.Claims.Claim("email", user.Email),
                new System.Security.Claims.Claim("name", user.Name)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    private bool VerifyPassword(string password, string hash)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput == hash;
    }
}
