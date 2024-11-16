using Contracts.Units.Requests;
using Contracts.Units.Responses;
using Contracts.Workers.Responses;

namespace Application.Common.Interfaces.Services;

public interface IUnitService
{
    public Task<GetUnitResponseDto> CreateUnit(int recruiterId, CreateUnitRequestDto requestDto);
    public Task<GetUnitResponseDto> UpdateUnit(int recruiterId, UpdateUnitRequestDto requestDto);
    public Task DeleteUnit(int recruiterId, int unitId);
    public Task<GetUnitResponseDto> GetUnitById(int recruiterId, int unitId);
    public Task<List<GetUnitResponseDto>> GetCompanyUnits(int recruiterId, int companyId);
    public Task<List<GetWorkerResponseDto>> GetUnitWorkers(int recruiterId, int unitId);
}