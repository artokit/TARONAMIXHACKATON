namespace Domain.Postgres;

public class DbWorker
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public bool IsLeader { get; set; }
    public bool IsCandidate { get; set; }
    public int CompanyId { get; set; }
    public int UnitId { get; set; }
    public DateTime  Birthday { get; set; }
    
    public DbUnit Unit { get; set; }
    public DbCompany Company { get; set; }
}