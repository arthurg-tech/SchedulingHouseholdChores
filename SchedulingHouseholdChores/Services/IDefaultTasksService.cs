using SchedulingHouseholdChores.Models;

namespace SchedulingHouseholdChores.Services;

/// <summary>
/// Serviço que fornece tarefas padrão para novos usuários
/// </summary>
public interface IDefaultTasksService
{
    /// <summary>
    /// Retorna a lista de tarefas padrão que devem ser criadas para um novo usuário
    /// </summary>
    List<RecurrentTask> GetDefaultTasks(int userId);
}
