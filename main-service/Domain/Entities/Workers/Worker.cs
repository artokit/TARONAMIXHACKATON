using Domain.Entities.Companies;

namespace Domain.Entities.Workers;

public class Worker
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public bool IsLeader { get; set; }
    public bool IsCandidate { get; set; }
    public Company Company { get; set; }
}