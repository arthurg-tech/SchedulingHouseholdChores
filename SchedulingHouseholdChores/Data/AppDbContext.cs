using Microsoft.EntityFrameworkCore;
using SchedulingHouseholdChores.Models;

namespace SchedulingHouseholdChores.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<RecurrentTask> RecurrentTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<RecurrentTask>()
            .HasOne(rt => rt.User)
            .WithMany(u => u.RecurrentTasks)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Dados de exemplo desabilitados usar a API para criar tarefas
        // Será necessário criar migrations para popular dados iniciais
    }
}
