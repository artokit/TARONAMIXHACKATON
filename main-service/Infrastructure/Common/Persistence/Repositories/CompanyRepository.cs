using Application.Common.Interfaces.Persistence;
using Domain.Postgres;
using Infrastructure.Common.Persistence.Scripts.Companies;
using Infrastructure.Dapper;
using Infrastructure.Dapper.Interfaces;

namespace Infrastructure.Common.Persistence.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private IDapperContext _dapperContext;
    
    public CompanyRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    
    public async Task<DbCompany?> GetCompanyByIdAsync(int companyId)
    {
        var queryObject = new QueryObject(
            PostgresCompanyElement.GetCompanyById,
            new
            {
                id=companyId
            }
        );
        return await _dapperContext.FirstOrDefault<DbCompany>(queryObject);
    }

    public async Task<DbCompany> GetUserCompaniesAsync(int userId)
    {
        // Todo: 
        var queryObject = new QueryObject(PostgresCompanyElement.GetUserCompanies);
        return await _dapperContext.FirstOrDefault<DbCompany>(queryObject);
    }

    public async Task AddRecruiterToCompanyAsync(int userId, int companyId)
    {
        var queryObject = new QueryObject(
            PostgresCompanyElement.AddRecruiterToCompany,
            new
            {
                company_id=companyId,
                user_id=userId
            });
        await  _dapperContext.Command(queryObject);
    }

    public async Task<DbCompany> AddCompanyAsync(DbCompany dbCompany)
    {
        var queryObject = new QueryObject(
            PostgresCompanyElement.AddCompany,
            new
            {
                name=dbCompany.Name,
                description=dbCompany.Description,
                director_id=dbCompany.DirectorId
            });
        return await  _dapperContext.CommandWithResponse<DbCompany>(queryObject);
    }

    public async Task<DbCompany> UpdateCompanyAsync(DbCompany dbCompany)
    {
        var queryObject = new QueryObject(PostgresCompanyElement.UpdateCompany, new
        {
            id=dbCompany.Id,
            name=dbCompany.Name,
            description=dbCompany.Description,
            image_id=dbCompany.ImageId
        });
        return await  _dapperContext.CommandWithResponse<DbCompany>(queryObject);
    }

    public async Task DeleteCompanyByIdAsync(int companyId)
    {
        var queryObject = new QueryObject(PostgresCompanyElement.DeleteCompanyById, new {id=companyId});
        await _dapperContext.Command(queryObject);
    }

    public async Task DeleteRecruiterByIdAsync(int companyId, int recruiterId)
    {
        var queryObject = new QueryObject(PostgresCompanyElement.DeleteRecruiterById, new
        {
            company_id=companyId,
            user_id=recruiterId
        });
        await _dapperContext.Command(queryObject);
    }

    public async Task<DbCompanyUser?> GetCompaniesUsersByIdsAsync(int companyId, int userId)
    {
        var queryObject = new QueryObject(PostgresCompanyElement.GetCompaniesUsersByIds, new
        {
            company_id=companyId,
            user_id=userId
        });
        return await _dapperContext.FirstOrDefault<DbCompanyUser>(queryObject);
    }
}