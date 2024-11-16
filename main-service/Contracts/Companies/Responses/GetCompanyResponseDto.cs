namespace Contracts.Companies.Responses;

public record GetCompanyResponseDto(
    int Id,
    string ImageUrl,
    string Name,
    string? Description
);