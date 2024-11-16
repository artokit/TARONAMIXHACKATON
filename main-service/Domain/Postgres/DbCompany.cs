using Domain.Entities.Images;
using Domain.Entities.Users;

namespace Domain.Postgres;

public class DbCompany
{
    public int Id { get; set; }
    public Guid? ImageId { get; set; }
    public int DirectorId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public List<User> Recruiters { get; set; }
    public Image? Image { get; set; }
    public User Director { get; set; }
}