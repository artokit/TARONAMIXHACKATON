using Domain.Entities.Companies;

namespace Domain.Postgres;

public class DbUnit
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; }
    
    public Company Company { get; set; }
}