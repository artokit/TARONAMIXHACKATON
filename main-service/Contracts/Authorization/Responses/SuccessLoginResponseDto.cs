
namespace Contracts.Authorization.Responses;

public record SuccessLoginResponseDto(
    string AccessToken,
    DateTime CreatedAt
);
