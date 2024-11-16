using Contracts.Workers.Responses;
using Domain.Postgres;

namespace Application.Mappers;

public static class WorkerMapper
{
    public static GetWorkerResponseDto MapDbToDto(this DbWorker dbWorker)
    {
        return new GetWorkerResponseDto(
            Id: dbWorker.Id,
            Email: dbWorker.Email,
            Name: dbWorker.Name,
            Surname: dbWorker.Surname,
            Patronymic: dbWorker.Patronymic,
            IsLeader: dbWorker.IsLeader,
            IsCandidate: dbWorker.IsCandidate,
            UnitId: dbWorker.UnitId,
            Birthday: dbWorker.Birthday
            );
    }
}