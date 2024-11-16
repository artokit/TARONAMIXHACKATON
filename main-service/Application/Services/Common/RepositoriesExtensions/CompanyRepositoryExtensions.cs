using Application.Common.Interfaces.Persistence;
using Application.Exceptions.Companies;
using Domain.Postgres;

namespace Application.Services.Common.RepositoriesExtensions;

public static class CompanyRepositoryExtensions
{
    public static async Task<DbCompany> GetCompanyOrThrowAsync(this ICompanyRepository companyRepository, int companyId)
    {
        var res = await companyRepository.GetCompanyByIdAsync(companyId);
        if (res is null)
        {
            throw new CompanyNotFoundException();
        }

        return res;
    }

    public static void CheckUserDirectorOfCompany(this DbCompany dbCompany, int userId)
    {
        if (dbCompany.DirectorId != userId)
        {
            throw new ActionWithCompanyForbiddenException();
        }
    }

    public static async Task CheckUserRecruiterOfCompanyAsync(this ICompanyRepository companyRepository, int companyId, int recruiterId)
    {
        var user = await companyRepository.GetCompaniesUsersByIdsAsync(companyId, recruiterId);
        if (user is null)
        {
            throw new ActionWithCompanyForbiddenException();
        }
    }
}