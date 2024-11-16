using Contracts.Units.Responses;
using Domain.Postgres;

namespace Application.Mappers;

public static class UnitMapper
{
    public static GetUnitResponseDto MapDbToDto(this DbUnit dbUnit)
    {
        return new GetUnitResponseDto
        {
            Name = dbUnit.Name,
            Id = dbUnit.Id
        };
    }
}