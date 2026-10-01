using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchedulingHouseholdChores.Data;
using SchedulingHouseholdChores.Models;
using System.Security.Claims;

namespace SchedulingHouseholdChores.Controllers;

[Authorize] // Garante que apenas utilizadores com Token válido acedem
[ApiController]
[Route("api/recurrenttasks")]
public class RecurrentTasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public RecurrentTasksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var userId = GetUserId();
        var tasks = await _context.RecurrentTasks
            .Where(t => t.UserId == userId)
            .ToListAsync();

        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title) || request.FrequencyInDays <= 0)
        {
            return BadRequest("Título e frequência são obrigatórios e devem ser válidos");
        }

        var task = new RecurrentTask
        {
            Title = request.Title,
            Description = request.Description ?? string.Empty,
            FrequencyInDays = request.FrequencyInDays,
            LastExecution = DateTime.Now,
            UserId = GetUserId()
        };

        _context.RecurrentTasks.Add(task);
        await _context.SaveChangesAsync();

        return Ok(task);
    }

    private int GetUserId()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? User.FindFirst("nameid")?.Value;

        if (int.TryParse(userIdString, out int userId))
        {
            return userId;
        }

        throw new UnauthorizedAccessException("O ID do utilizador não foi encontrado no token.");
    }
}

public record CreateTaskRequest(string Title, string Description, int FrequencyInDays);