using Application.Common.Interfaces.Persistence;
using Domain.Postgres;
using Infrastructure.Common.Persistence.Scripts.Workers;
using Infrastructure.Dapper;
using Infrastructure.Dapper.Interfaces;

namespace Infrastructure.Common.Persistence.Repositories;

public class WorkerRepository : IWorkerRepository
{
    private IDapperContext _dapperContext;
    
    public WorkerRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    
    public async Task<DbWorker> AddWorkerAsync(DbWorker dbWorker)
    {
        var queryObject = new QueryObject(PostgresWorkerElement.AddWorker, new
        {
            name=dbWorker.Name, 
            surname=dbWorker.Surname,
            patronymic=dbWorker.Patronymic,
            email=dbWorker.Email,
            is_leader=dbWorker.IsLeader, 
            is_candidate=dbWorker.IsCandidate,
            company_id=dbWorker.CompanyId,
            unit_id=dbWorker.UnitId,
            birthday=dbWorker.Birthday
        });
        return await _dapperContext.CommandWithResponse<DbWorker>(queryObject);
    }

    public async Task<DbWorker?> GetWorkerByIdAsync(int id)
    {
        var queryObject = new QueryObject(PostgresWorkerElement.GetWorkerById, new {worker_id=id});
        return await _dapperContext.FirstOrDefault<DbWorker>(queryObject);
    }

    public async Task DeleteWorkerByIdAsync(int id)
    {
        var queryObject = new QueryObject(PostgresWorkerElement.DeleteWorkerById, new
        {
            id,
        });
        await _dapperContext.Command(queryObject);
    }

    public async Task<DbWorker> UpdateWorkerAsync(DbWorker dbWorker)
    {
        var queryObject = new QueryObject(PostgresWorkerElement.UpdateWorker, new
        {
            id = dbWorker.Id,
            name=dbWorker.Name, 
            surname=dbWorker.Surname,
            patronymic=dbWorker.Patronymic,
            email=dbWorker.Email,
            is_leader=dbWorker.IsLeader, 
            is_candidate=dbWorker.IsCandidate,
            unit_id=dbWorker.UnitId,
            birthday=dbWorker.Birthday
        });
        return await _dapperContext.CommandWithResponse<DbWorker>(queryObject);
    
    }

    public async Task<List<DbWorker>> GetAllWorkersByCompanyIdAsync(int companyId)
    {
        var queryObject = new QueryObject(PostgresWorkerElement.GetCompanyWorkers, new
        {
            company_id=companyId
        });
        
        return await _dapperContext.ListOrDefault<DbWorker>(queryObject);
    }

    public async Task<List<DbWorker>> GetAllWorkersByUnitIdAsync(int unitId)
    {
        var queryObject = new QueryObject(PostgresWorkerElement.GetUnitWorkers, new
        {
            unit_id=unitId
        });
        
        return await _dapperContext.ListOrDefault<DbWorker>(queryObject);
    }

    public async Task<DbWorker?> GetWorkerByEmailAsync(string email)
    {
        var queryObject = new QueryObject(PostgresWorkerElement.GetWorkerByEmail, new
        {
            email
        });
        
        return await _dapperContext.FirstOrDefault<DbWorker>(queryObject);
    }
}