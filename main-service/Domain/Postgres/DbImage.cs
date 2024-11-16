using Domain.Entities.Images;
using Domain.Entities.Users;

namespace Domain.Postgres;

public class DbImage
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<User> Recruiters { get; set; }
    
    public Image? Image { get; set; }
}