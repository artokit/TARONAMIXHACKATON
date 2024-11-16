using Contracts.Companies.Requests;
using Contracts.Companies.Responses;

namespace Application.Common.Interfaces.Services;

public interface ICompanyService
{
    public Task<GetCompanyResponseDto> GetCompanyById(int companyId);
    public Task<List<GetCompanyResponseDto>> GetCompanies(int userId);
    public Task InviteRecruiter(int directorId, int recruiterId, int companyId);
    public Task<GetCompanyResponseDto> CreateCompany(CreateCompanyRequestDto requestDto, int directorId);
    public Task<GetCompanyResponseDto> UpdateCompany(int companyId, UpdateCompanyRequestDto requestDto, int directorId);
    public Task DeleteCompany(int companyId, int directorId);
    public Task DeleteRecruiter(int recruiterId, int companyId, int directorId);
}