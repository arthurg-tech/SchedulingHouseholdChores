namespace SchedulingHouseholdChores.Models;

public class RecurrentTask
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int FrequencyInDays { get; set; }
    public DateTime LastExecution { get; set; }

    public DateTime NextExecution => LastExecution.AddDays(FrequencyInDays);
}