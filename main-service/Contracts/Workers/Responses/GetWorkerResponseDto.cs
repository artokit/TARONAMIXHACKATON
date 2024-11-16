namespace Contracts.Workers.Responses;

public record GetWorkerResponseDto(
    int Id,
    string Email,
    string Name,
    string Surname,
    string? Patronymic,
    bool IsLeader,
    bool IsCandidate,
    int UnitId,
    DateTime Birthday
);