using Application.Common.Interfaces.Persistence;
using FluentMigrator.Runner;
using Infrastructure.Common.Persistence.Repositories;
using Infrastructure.Dapper;
using Infrastructure.Dapper.Interfaces;
using Infrastructure.Dapper.Interfaces.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<ICompanyRepository, CompanyRepository>();
        services.AddSingleton<IWorkerRepository, WorkerRepository>();
        services.AddSingleton<ITagRepository, TagRepository>();
        services.AddSingleton<IUnitRepository, UnitRepository>();
        return services;
    }
 
    public static IServiceCollection AddDapperContext(this IServiceCollection services)
    {
        services.AddSingleton<IDapperSettings, DapperSettings>();
        services.AddSingleton<IDapperContext, DapperContext>();
        return services;
    }
    
    public static IServiceCollection AddMigrations(this IServiceCollection services, string connectionString)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(InfrastructureExtensions).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());
        return services;
    }

    public static IServiceProvider UseMigrations(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        return serviceProvider;
    }
}