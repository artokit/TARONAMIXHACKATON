using Contracts.Workers.Requests;
using Contracts.Workers.Responses;

namespace Application.Common.Interfaces.Services;

public interface IWorkerService
{
    public Task<GetWorkerResponseDto> AddWorker(AddWorkerRequestDto addWorkerRequestDto, int recruiterId);
    public Task<GetWorkerResponseDto> GetWorkerById(int workerId, int recruiterId);
    public Task DeleteWorkerById(int workerId, int recruiterId);
    public Task<GetWorkerResponseDto> UpdateWorker(UpdateWorkerRequestDto updateWorkerRequestDto, int recruiterId);
    public Task<List<GetWorkerResponseDto>> GetCompanyWorkers(int companyId, int recruiterId);
}