using Infrastructure.Dapper.Interfaces.Settings;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Dapper;

public class DapperSettings : IDapperSettings
{
    public string ConnectionString { get; set; }
 
    public DapperSettings(IConfiguration configuration)
    {
        ConnectionString = configuration["ConnectionStrings:Database"];
    }
}