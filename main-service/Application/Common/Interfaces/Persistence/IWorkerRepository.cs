using Domain.Postgres;

namespace Application.Common.Interfaces.Persistence;

public interface IWorkerRepository
{
    public Task<DbWorker> AddWorkerAsync(DbWorker dbWorker);
    public Task<DbWorker?> GetWorkerByIdAsync(int id);
    public Task DeleteWorkerByIdAsync(int id);
    public Task<DbWorker> UpdateWorkerAsync(DbWorker dbWorker);
    public Task<List<DbWorker>> GetAllWorkersByCompanyIdAsync(int companyId);
    public Task<List<DbWorker>> GetAllWorkersByUnitIdAsync(int unitId);
    public Task<DbWorker?> GetWorkerByEmailAsync(string email);
}