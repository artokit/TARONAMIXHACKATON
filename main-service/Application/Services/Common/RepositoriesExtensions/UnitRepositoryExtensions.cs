using Application.Common.Interfaces.Persistence;
using Application.Exceptions.Units;
using Domain.Postgres;

namespace Application.Services.Common.RepositoriesExtensions;

public static class UnitRepositoryExtensions
{
    public static async Task<DbUnit> GetUnitByIdOrThrowAsync(this IUnitRepository unitRepository, int unitId)
    {
        var unit = await unitRepository.GetUnitByIdAsync(unitId);
        if (unit is null)
        {
            throw new UnitNotFoundRequestException();
        }
        return unit;
    }
}