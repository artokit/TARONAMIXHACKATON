using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Application.Mappers;
using Application.Services.Common.RepositoriesExtensions;
using Contracts.Units.Requests;
using Contracts.Units.Responses;
using Contracts.Workers.Responses;
using Domain.Postgres;

namespace Application.Services;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IWorkerRepository _workerRepository;

    public UnitService(IUnitRepository unitRepository, ICompanyRepository companyRepository, IWorkerRepository workerRepository)
    {
        _unitRepository = unitRepository;
        _companyRepository = companyRepository;
        _workerRepository = workerRepository;
    }

    public async Task<GetUnitResponseDto> CreateUnit(int recruiterId, CreateUnitRequestDto requestDto)
    {
        await _companyRepository.GetCompanyOrThrowAsync(requestDto.CompanyId);
        await _companyRepository.CheckUserRecruiterOfCompanyAsync(requestDto.CompanyId, recruiterId);
        
        var dbUnit = await _unitRepository.AddUnitAsync(new DbUnit { CompanyId = requestDto.CompanyId, Name = requestDto.Name });
        return dbUnit.MapDbToDto();
    }

    public async Task<GetUnitResponseDto> UpdateUnit(int recruiterId, UpdateUnitRequestDto requestDto)
    {
        var unit = await _unitRepository.GetUnitByIdOrThrowAsync(requestDto.UnitId);
        await _companyRepository.CheckUserRecruiterOfCompanyAsync(unit.CompanyId, recruiterId);

        unit.Name = requestDto.Name ?? unit.Name;
        var updatedUnit = await _unitRepository.UpdateUnitAsync(unit);
        
        return updatedUnit.MapDbToDto();
    }

    public async Task DeleteUnit(int recruiterId, int unitId)
    {
        var unit = await _unitRepository.GetUnitByIdOrThrowAsync(unitId);
        await _companyRepository.CheckUserRecruiterOfCompanyAsync(unit.CompanyId, recruiterId);

        await _unitRepository.DeleteUnitByIdAsync(unitId);
    }

    public async Task<GetUnitResponseDto> GetUnitById(int recruiterId, int unitId)
    {
        var unit = await _unitRepository.GetUnitByIdOrThrowAsync(unitId);
        await _companyRepository.CheckUserRecruiterOfCompanyAsync(unit.CompanyId, recruiterId);
        
        return unit.MapDbToDto();
    }

    public async Task<List<GetUnitResponseDto>> GetCompanyUnits(int recruiterId, int companyId)
    {
        await _companyRepository.GetCompanyOrThrowAsync(companyId);
        await _companyRepository.CheckUserRecruiterOfCompanyAsync(companyId, recruiterId);

        var units = await _unitRepository.GetAllUnitsByCompanyIdAsync(companyId);
        return units.Select(i => i.MapDbToDto()).ToList();
    }

    public async Task<List<GetWorkerResponseDto>> GetUnitWorkers(int recruiterId, int unitId)
    {
        var unit = await _unitRepository.GetUnitByIdOrThrowAsync(unitId);
        await _companyRepository.CheckUserRecruiterOfCompanyAsync(unit.CompanyId, recruiterId);

        return (await _workerRepository.GetAllWorkersByUnitIdAsync(unitId)).Select(i => i.MapDbToDto()).ToList();
    }
}