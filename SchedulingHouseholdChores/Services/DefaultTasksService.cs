using SchedulingHouseholdChores.Models;

namespace SchedulingHouseholdChores.Services;

/// <summary>
/// Implementação do serviço de tarefas padrão para novos usuários
/// </summary>
public class DefaultTasksService : IDefaultTasksService
{
    public List<RecurrentTask> GetDefaultTasks(int userId)
    {
        var now = DateTime.Now;

        return new List<RecurrentTask>
        {
            new RecurrentTask
            {
                Title = "Trocar filtro da fonte de água",
                Description = "Substituir o filtro de carvão ativado da fonte de água automática dos gatos.",
                FrequencyInDays = 30,
                LastExecution = now.AddDays(-15),
                UserId = userId
            },
            new RecurrentTask
            {
                Title = "Comprar areia do Gato",
                Description = "Repor o estoque de areia biodegradável para gatos.",
                FrequencyInDays = 45,
                LastExecution = now.AddDays(-40),
                UserId = userId
            },
            new RecurrentTask
            {
                Title = "Tirar o lixo",
                Description = "Esvaziar todas as lixeiras da casa e levar os sacos para fora.",
                FrequencyInDays = 2,
                LastExecution = now.AddDays(-1),
                UserId = userId
            },
            new RecurrentTask
            {
                Title = "Aspirar a casa",
                Description = "Aspirar todos os pisos e tapetes, dando atenção especial aos pelos de gato nos móveis.",
                FrequencyInDays = 3,
                LastExecution = now.AddDays(-2),
                UserId = userId
            },
            new RecurrentTask
            {
                Title = "Faxina pesada no banheiro",
                Description = "Esfregar o box e o vaso sanitário, limpar as torneiras e acessórios do banheiro.",
                FrequencyInDays = 7,
                LastExecution = now.AddDays(-6),
                UserId = userId
            },
            new RecurrentTask
            {
                Title = "Trocar roupa de cama",
                Description = "Remover os lençóis e fronhas usados e substituir por um conjunto limpo.",
                FrequencyInDays = 7,
                LastExecution = now.AddDays(-7),
                UserId = userId
            },
            new RecurrentTask
            {
                Title = "Lavar as roupas",
                Description = "Separar, lavar e secar as roupas e toalhas.",
                FrequencyInDays = 4,
                LastExecution = now.AddDays(-2),
                UserId = userId
            }
        };
    }
}
