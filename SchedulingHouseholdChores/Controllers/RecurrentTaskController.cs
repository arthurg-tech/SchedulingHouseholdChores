using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchedulingHouseholdChores.Data;
using SchedulingHouseholdChores.Models;
using System.Security.Claims;

namespace SchedulingHouseholdChores.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RecurrentTasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public RecurrentTasksController(AppDbContext context)
    {
        _context = context;
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirst("nameid")?.Value;
        return int.Parse(userIdClaim ?? "0");
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
        var userId = GetUserId();

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
            UserId = userId
        };

        _context.RecurrentTasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskRequest request)
    {
        var userId = GetUserId();

        var task = await _context.RecurrentTasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
            return NotFound("Tarefa não encontrada");

        if (!string.IsNullOrWhiteSpace(request.Title))
            task.Title = request.Title;

        if (!string.IsNullOrWhiteSpace(request.Description))
            task.Description = request.Description;

        if (request.FrequencyInDays.HasValue && request.FrequencyInDays > 0)
            task.FrequencyInDays = request.FrequencyInDays.Value;

        _context.RecurrentTasks.Update(task);
        await _context.SaveChangesAsync();

        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var userId = GetUserId();

        var task = await _context.RecurrentTasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task == null)
            return NotFound("Tarefa não encontrada");

        _context.RecurrentTasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

public record CreateTaskRequest(string Title, string? Description, int FrequencyInDays);
public record UpdateTaskRequest(string? Title, string? Description, int? FrequencyInDays);