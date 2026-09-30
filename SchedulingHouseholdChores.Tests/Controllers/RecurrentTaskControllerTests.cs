using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchedulingHouseholdChores.Controllers;
using SchedulingHouseholdChores.Data;
using SchedulingHouseholdChores.Models;

namespace SchedulingHouseholdChores.Tests;

public class RecurrentTasksControllerTests
{
    // Método auxiliar para gerar um banco de dados temporário em memória para cada teste
    private DbContextOptions<AppDbContext> GetInMemoryOptions()
    {
        return new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task GetTasks_ReturnsOkResult_WithListOfTasks()
    {
        var options = GetInMemoryOptions();
        using var context = new AppDbContext(options);

        context.RecurrentTasks.Add(new RecurrentTask { Id = 10, Title = "Teste 1", FrequencyInDays = 5 });
        context.RecurrentTasks.Add(new RecurrentTask { Id = 11, Title = "Teste 2", FrequencyInDays = 7 });
        await context.SaveChangesAsync();

        var controller = new RecurrentTasksController(context);

        var result = await controller.GetTasks();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var tasks = Assert.IsAssignableFrom<IEnumerable<RecurrentTask>>(okResult.Value);
        Assert.Equal(2, tasks.Count()); // Verifica se retornou exatamente as 2 tarefas criadas
    }

    [Fact]
    public async Task CreateTask_ReturnsCreatedAtActionResult_AndAddsToDatabase()
    {
        var options = GetInMemoryOptions();
        using var context = new AppDbContext(options);
        var controller = new RecurrentTasksController(context);

        var newTask = new RecurrentTask
        {
            Id = 20,
            Title = "Limpar a casa",
            Description = "Teste de post",
            FrequencyInDays = 10,
            LastExecution = DateTime.Now
        };
        var result = await controller.CreateTask(newTask);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedTask = Assert.IsType<RecurrentTask>(createdResult.Value);

        Assert.Equal("GetTasks", createdResult.ActionName);
        Assert.Equal("Limpar a casa", returnedTask.Title);

        var taskInDb = await context.RecurrentTasks.FindAsync(20);
        Assert.NotNull(taskInDb);
        Assert.Equal("Limpar a casa", taskInDb.Title);
    }
}