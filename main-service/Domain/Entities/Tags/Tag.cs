using Domain.Common.Tags;
using Domain.Entities.Workers;

namespace Domain.Entities.Tags;

public class Tag
{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public string Name { get; set; }
    public TagsType Type { get; set; }
    
    public Worker Worker { get; set; }
}