using Contracts.Tags.Responses;
using Domain.Postgres;

namespace Application.Mappers;

public static class TagMapper
{
    public static GetWorkerTagResponseDto MapDbToDto(this DbTag dbTag)
    {
        return new GetWorkerTagResponseDto
        {
            Id=dbTag.Id,
            Name = dbTag.Name,
            Type = dbTag.Type
        };
    }
}