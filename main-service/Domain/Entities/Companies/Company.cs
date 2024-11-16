using Domain.Entities.Images;
using Domain.Entities.Users;

namespace Domain.Entities.Companies;

public class Company
{
    public int Id { get; set; }
    public Image? Image { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<User> Recruiters { get; set; }
}