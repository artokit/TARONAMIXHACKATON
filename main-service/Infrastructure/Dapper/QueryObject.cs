using Infrastructure.Dapper.Interfaces;

namespace Infrastructure.Dapper;

public class QueryObject : IQueryObject
{
    public QueryObject(string sql, object? parameters = null)
    {
        if (string.IsNullOrEmpty(sql))
        {
            throw new ArgumentException();
        }
        Sql = sql;
        Params = parameters;
    }
    public string Sql { get; set; }
    public object? Params { get; set; }
}