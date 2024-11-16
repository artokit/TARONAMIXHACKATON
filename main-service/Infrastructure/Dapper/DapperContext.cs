using System.Data;
using Dapper;
using Infrastructure.Dapper.Interfaces;
using Infrastructure.Dapper.Interfaces.Settings;
using Npgsql;

namespace Infrastructure.Dapper;

public class DapperContext : IDapperContext
{
    private IDapperSettings _dapperSettings;

    public DapperContext(IDapperSettings dapperSettings)
    {
        _dapperSettings = dapperSettings;
    }
    
    public async Task<T?> FirstOrDefault<T>(IQueryObject queryObject)
    { 
        return await Execute(query => query.QueryFirstOrDefaultAsync<T>(queryObject.Sql, queryObject.Params));
    }

    public async Task<List<T>> ListOrDefault<T>(IQueryObject queryObject)
    {
        return (await Execute(query => query.QueryAsync<T>(queryObject.Sql, queryObject.Params))).ToList();
    }

    public async Task Command(IQueryObject queryObject)
    {
        await Execute(query => query.QueryAsync(queryObject.Sql, queryObject.Params));
    }

    public async Task<T> CommandWithResponse<T>(IQueryObject queryObject)
    {
        return await Execute(query => query.QueryFirstAsync<T>(queryObject.Sql, queryObject.Params));
    }

    private async Task<T> Execute<T>(Func<IDbConnection, Task<T>> query)
    {
        await using var connection = new NpgsqlConnection(_dapperSettings.ConnectionString);
        var res = await query(connection);
        await connection.CloseAsync();
        return res;
    }
}