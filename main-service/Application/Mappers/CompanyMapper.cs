using Contracts.Companies.Requests;
using Contracts.Companies.Responses;
using Domain.Postgres;

namespace Application.Mappers;

public static class CompanyMapper
{
    public static GetCompanyResponseDto MapDbToDto(this DbCompany dbCompany)
    {
        return new GetCompanyResponseDto(Name: dbCompany.Name, Description: dbCompany.Description, Id: dbCompany.Id, ImageUrl: "");
    }

    public static DbCompany MapDtoToDb(this CreateCompanyRequestDto requestDto, int directorId)
    {
        return new DbCompany { Description = requestDto.Description, Name = requestDto.Name, DirectorId = directorId };
    }

    public static DbCompany MapDtoToDb(this UpdateCompanyRequestDto requestDto, int companyId, DbCompany dbCompany)
    {
        return new DbCompany
        {
            Id = companyId,
            Name = requestDto.Name ?? dbCompany.Name,
            Description = requestDto.Description ?? dbCompany.Description,
            ImageId = requestDto.ImageId ?? dbCompany.ImageId
        };
    }
}