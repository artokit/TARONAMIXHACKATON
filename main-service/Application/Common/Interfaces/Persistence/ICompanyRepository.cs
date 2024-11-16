using Domain.Postgres;

namespace Application.Common.Interfaces.Persistence;

public interface ICompanyRepository
{
    public Task<DbCompany?> GetCompanyByIdAsync(int companyId);
    public Task<DbCompany> GetUserCompaniesAsync(int userId);
    public Task AddRecruiterToCompanyAsync(int userId, int companyId);
    public Task<DbCompany> AddCompanyAsync(DbCompany dbCompany);
    public Task<DbCompany> UpdateCompanyAsync(DbCompany dbCompany);
    public Task DeleteCompanyByIdAsync(int companyId);
    public Task DeleteRecruiterByIdAsync(int companyId, int recruiterId);
    public Task<DbCompanyUser?> GetCompaniesUsersByIdsAsync(int companyId, int userId);
}