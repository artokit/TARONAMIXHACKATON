using Domain.Common.Tags;

namespace Domain.Postgres;

public class DbTag
{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public string Name { get; set; }
    public TagsType Type { get; set; }
    
    public DbWorker Worker { get; set; }
}