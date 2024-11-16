using Domain.Entities.Companies;

namespace Domain.Entities.Units;

public class Unit
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    
    public Company Company { get; set; }
}