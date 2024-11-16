using Domain.Postgres;

namespace Application.Common.Interfaces.Persistence;

public interface IUnitRepository
{
    public Task<DbUnit> AddUnitAsync(DbUnit dbUnit);
    public Task<DbUnit> UpdateUnitAsync(DbUnit dbUnit);
    public Task DeleteUnitByIdAsync(int id);
    public Task<DbUnit?> GetUnitByIdAsync(int id);
    public Task<List<DbUnit>> GetAllUnitsByCompanyIdAsync(int id);
}