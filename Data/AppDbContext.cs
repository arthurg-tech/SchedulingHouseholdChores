using Microsoft.EntityFrameworkCore;
using SchedulingHouseholdChores.Models;

namespace SchedulingHouseholdChores.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<RecurrentTask> RecurrentTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecurrentTask>().HasData(
            new RecurrentTask
            {
                Id = 1,
                Title = "Change pet water fountain filter",
                Description = "Replace the activated carbon filter of the automatic water fountain.",
                FrequencyInDays = 30,
                LastExecution = DateTime.Now.AddDays(-15)
            },
            new RecurrentTask
            {
                Id = 2,
                Title = "Buy Catbio litter",
                Description = "Restock biodegradable cat litter.",
                FrequencyInDays = 45,
                LastExecution = DateTime.Now.AddDays(-40)
            }
        );
    }
}