using Domain.Common.Tags;

namespace Contracts.Tags.Requests;

public class AddTagWorkerRequestDto
{
    public int WorkerId { get; set; }
    public string Name { get; set; }
    public TagsType Type { get; set; }
}