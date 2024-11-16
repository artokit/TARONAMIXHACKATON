using Domain.Common.Tags;

namespace Contracts.Tags.Responses;

public class GetWorkerTagResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TagsType Type { get; set; }
}