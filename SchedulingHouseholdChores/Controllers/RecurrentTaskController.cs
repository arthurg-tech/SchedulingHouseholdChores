using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchedulingHouseholdChores.Data;
using SchedulingHouseholdChores.Models;

namespace SchedulingHouseholdChores.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var tasks = await _context.RecurrentTasks.ToListAsync();
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(RecurrentTask task)
    {
        _context.RecurrentTasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
    }
}