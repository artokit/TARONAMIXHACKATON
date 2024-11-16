using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Application.Exceptions.Companies;
using Application.Exceptions.Users;
using Application.Mappers;
using Application.Services.Common.RepositoriesExtensions;
using Contracts.Companies.Requests;
using Contracts.Companies.Responses;
using Domain.Common.Users;

namespace Application.Services;

public class CompanyService : ICompanyService
{
    private ICompanyRepository _companyRepository;
    private IUserRepository _userRepository;
    
    public CompanyService(ICompanyRepository companyRepository, IUserRepository userRepository)
    {
        _companyRepository = companyRepository;
        _userRepository = userRepository;
    }

    public async Task<GetCompanyResponseDto> GetCompanyById(int companyId)
    {
        var res = await _companyRepository.GetCompanyOrThrowAsync(companyId);
        
        return res.MapDbToDto();
    }

    public async Task<List<GetCompanyResponseDto>> GetCompanies(int userId)
    {
        // Todo: 
        throw new NotImplementedException();
    }

    public async Task InviteRecruiter(int directorId, int recruiterId, int companyId)
    {
        var res = await _companyRepository.GetCompanyOrThrowAsync(companyId);
        res.CheckUserDirectorOfCompany(directorId);

        var user = await _userRepository.GetUserByIdOrThrowAsync(recruiterId);
        
        if (user.Role == Roles.Administrator)
        {
            throw new UserCantBeRecruiterException();
        }
        
        var dbCompanyUser = await _companyRepository.GetCompaniesUsersByIdsAsync(companyId, recruiterId);

        if (dbCompanyUser is not null)
        {
            throw new RecruiterInCompanyException();
        }
        
        await _companyRepository.AddRecruiterToCompanyAsync(recruiterId, companyId);
    }

    public async Task<GetCompanyResponseDto> CreateCompany(CreateCompanyRequestDto requestDto, int directorId)
    {
        var res = await _companyRepository.AddCompanyAsync(requestDto.MapDtoToDb(directorId));
        await _companyRepository.AddRecruiterToCompanyAsync(directorId, res.Id);
        return res.MapDbToDto();
    }

    public async Task<GetCompanyResponseDto> UpdateCompany(int companyId, UpdateCompanyRequestDto requestDto, int directorId)
    {
        var res = await _companyRepository.GetCompanyOrThrowAsync(companyId);
        res.CheckUserDirectorOfCompany(directorId);

        var updatedCompany = await _companyRepository.UpdateCompanyAsync(requestDto.MapDtoToDb(companyId, res));
        return updatedCompany.MapDbToDto();
    }

    public async Task DeleteCompany(int companyId, int directorId)
    {
        var res = await _companyRepository.GetCompanyOrThrowAsync(companyId);
        res.CheckUserDirectorOfCompany(directorId);

        await _companyRepository.DeleteCompanyByIdAsync(companyId);
    }

    public async Task DeleteRecruiter(int recruiterId, int companyId, int directorId)
    {
        var res = await _companyRepository.GetCompanyOrThrowAsync(companyId);
        res.CheckUserDirectorOfCompany(directorId);
        
        var dbCompanyUser = await _companyRepository.GetCompaniesUsersByIdsAsync(companyId, recruiterId);
        
        if (dbCompanyUser is null)
        {
            throw new UserNotFoundException("Данного рекрутера нету в вашей компании");
        }
        
        await _companyRepository.DeleteRecruiterByIdAsync(companyId, recruiterId);
    }
}