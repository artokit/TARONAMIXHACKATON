namespace Contracts.Companies.Requests;

public record CreateCompanyRequestDto(
    string Name,
    string? Description
);