using Microsoft.AspNetCore.Mvc;
using SchedulingHouseholdChores.Services;

namespace SchedulingHouseholdChores.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || 
            string.IsNullOrWhiteSpace(request.Password) ||
            string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Email, senha e nome são obrigatórios");
        }

        var result = await _authService.RegisterAsync(request.Email, request.Password, request.Name);

        if (result == null)
            return BadRequest("Email já cadastrado");

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || 
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Email e senha são obrigatórios");
        }

        var result = await _authService.LoginAsync(request.Email, request.Password);

        if (result == null)
            return Unauthorized("Email ou senha inválidos");

        return Ok(result);
    }
}

public record RegisterRequest(string Email, string Password, string Name);
public record LoginRequest(string Email, string Password);
