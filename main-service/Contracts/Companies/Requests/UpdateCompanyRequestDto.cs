namespace Contracts.Companies.Requests;

public record UpdateCompanyRequestDto(
    string? Name,
    string? Description,
    Guid? ImageId
);