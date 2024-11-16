using Application.Common.Interfaces.Persistence;
using Application.Exceptions.Workers;
using Domain.Postgres;

namespace Application.Services.Common.RepositoriesExtensions;

public static class WorkerRepositoryExtensions
{
    public static async Task<DbWorker> GetWorkerByIdOrThrowAsync(this IWorkerRepository workerRepository, int workerId)
    {
        var res = await workerRepository.GetWorkerByIdAsync(workerId);
        if (res is null)
        {
            throw new WorkerNotFoundException();
        }

        return res;
    }
}