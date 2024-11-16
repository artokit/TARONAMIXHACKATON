using Application.Common.Interfaces.Persistence;
using Domain.Postgres;
using Infrastructure.Common.Persistence.Scripts.Companies;
using Infrastructure.Common.Persistence.Scripts.Units;
using Infrastructure.Dapper;
using Infrastructure.Dapper.Interfaces;

namespace Infrastructure.Common.Persistence.Repositories;

public class UnitRepository : IUnitRepository
{
    private IDapperContext _dapperContext;

    public UnitRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    
    public Task<DbUnit> AddUnitAsync(DbUnit dbUnit)
    {
        var queryObject = new QueryObject(PostgresUnitElement.AddUnit, new {company_id=dbUnit.CompanyId, name=dbUnit.Name});
        return _dapperContext.CommandWithResponse<DbUnit>(queryObject);
    }

    public async Task<DbUnit> UpdateUnitAsync(DbUnit dbUnit)
    {
        var queryObject =
            new QueryObject(
                PostgresUnitElement.UpdateUnit,
                new
                {
                    unit_id=dbUnit.Id,
                    name = dbUnit.Name
                });
        return await _dapperContext.CommandWithResponse<DbUnit>(queryObject);
    }

    public async Task DeleteUnitByIdAsync(int id)
    {
        var queryObject = new QueryObject(PostgresUnitElement.DeleteUnitById, new {id});
        await _dapperContext.Command(queryObject);
    }

    public async Task<DbUnit?> GetUnitByIdAsync(int id)
    {
        var queryObject =
            new QueryObject(
                PostgresUnitElement.GetUnitById,
                new {id});
        return await _dapperContext.FirstOrDefault<DbUnit>(queryObject);
    }
    
    public async Task<List<DbUnit>> GetAllUnitsByCompanyIdAsync(int id)
    {
        var queryObject =
            new QueryObject(
                PostgresUnitElement.GetAllUnitsByCompanyId, new
                {
                    id
                });
        return await _dapperContext.ListOrDefault<DbUnit>(queryObject);
    }
}