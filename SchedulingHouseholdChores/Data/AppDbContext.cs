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
                Title = "Trocar filtro da fonte de água",
                Description = "Substituir o filtro de carvão ativado da fonte de água automática dos gatos.",
                FrequencyInDays = 30,
                LastExecution = DateTime.Now.AddDays(-15)
            },
            new RecurrentTask
            {
                Id = 2,
                Title = "Comprar areia do Gato",
                Description = "Repor o estoque de areia biodegradável para gatos.",
                FrequencyInDays = 45,
                LastExecution = DateTime.Now.AddDays(-40)
            },
            new RecurrentTask
            {
                Id = 3,
                Title = "Tirar o lixo",
                Description = "Esvaziar todas as lixeiras da casa e levar os sacos para fora.",
                FrequencyInDays = 2,
                LastExecution = DateTime.Now.AddDays(-1)
            },
            new RecurrentTask
            {
                Id = 4,
                Title = "Aspirar a casa",
                Description = "Aspirar todos os pisos e tapetes, dando atenção especial aos pelos de gato nos móveis.",
                FrequencyInDays = 3,
                LastExecution = DateTime.Now.AddDays(-2)
            },
            new RecurrentTask
            {
                Id = 5,
                Title = "Faxina pesada no banheiro",
                Description = "Esfregar o box e o vaso sanitário, limpar as torneiras e acessórios do banheiro.",
                FrequencyInDays = 7,
                LastExecution = DateTime.Now.AddDays(-6)
            },
            new RecurrentTask
            {
                Id = 6,
                Title = "Trocar roupa de cama",
                Description = "Remover os lençóis e fronhas usados e substituir por um conjunto limpo.",
                FrequencyInDays = 7,
                LastExecution = DateTime.Now.AddDays(-7)
            },
            new RecurrentTask
            {
                Id = 7,
                Title = "Lavar as roupas",
                Description = "Separar, lavar e secar as roupas e toalhas.",
                FrequencyInDays = 4,
                LastExecution = DateTime.Now.AddDays(-2)
            }
        );
    }
}