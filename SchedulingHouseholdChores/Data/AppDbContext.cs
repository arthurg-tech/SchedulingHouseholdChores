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
            },
            new RecurrentTask
            {
                Id = 3,
                Title = "Take out the trash",
                Description = "Empty all indoor trash bins and take the bags outside.",
                FrequencyInDays = 2,
                LastExecution = DateTime.Now.AddDays(-1)
            },
            new RecurrentTask
            {
                Id = 4,
                Title = "Vacuum the house",
                Description = "Vacuum all floors and rugs, paying special attention to cat hair on the furniture.",
                FrequencyInDays = 3,
                LastExecution = DateTime.Now.AddDays(-2)
            },
            new RecurrentTask
            {
                Id = 5,
                Title = "Deep clean the bathroom",
                Description = "Scrub the shower and toilet, and wipe down the matte black faucets and accessories with a non-abrasive soft cloth.",
                FrequencyInDays = 7,
                LastExecution = DateTime.Now.AddDays(-6)
            },
            new RecurrentTask
            {
                Id = 6,
                Title = "Change bed linens",
                Description = "Remove used sheets and pillowcases and replace with a fresh set.",
                FrequencyInDays = 7,
                LastExecution = DateTime.Now.AddDays(-7)
            },
            new RecurrentTask
            {
                Id = 7,
                Title = "Do laundry",
                Description = "Sort, wash, and dry clothes and towels.",
                FrequencyInDays = 4,
                LastExecution = DateTime.Now.AddDays(-2)
            }
        );
    }
}