using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Application.Exceptions.Workers;
using Application.Mappers;
using Application.Services.Common.RepositoriesExtensions;
using Contracts.Workers.Requests;
using Contracts.Workers.Responses;
using Domain.Postgres;

namespace Application.Services;

public class WorkerService : IWorkerService
{
    private IWorkerRepository _workerRepository;
    private ICompanyRepository _companyRepository;
    private IUnitRepository _unitRepository;
    
    public WorkerService(IWorkerRepository workerRepository, ICompanyRepository companyRepository, IUnitRepository unitRepository)
    {
        _workerRepository = workerRepository;
        _companyRepository = companyRepository;
        _unitRepository = unitRepository;
    }
    
    public async Task<GetWorkerResponseDto> AddWorker(AddWorkerRequestDto addWorkerRequestDto, int recruiterId)
    {
        var unit = await _unitRepository.GetUnitByIdOrThrowAsync(addWorkerRequestDto.UnitId);
        var companyUser = await _companyRepository.GetCompaniesUsersByIdsAsync(unit.CompanyId, recruiterId);
        if (companyUser is null)
        {
            throw new CantAddWorkerToCompanyException();
        }

        if (await _workerRepository.GetWorkerByEmailAsync(addWorkerRequestDto.Email) is not null)
        {
            throw new WorkerEmailIsExistException();
        }

        var res = await _workerRepository.AddWorkerAsync(new DbWorker
        {
            Name = addWorkerRequestDto.Name,
            Surname = addWorkerRequestDto.Surname,
            Patronymic = addWorkerRequestDto.Patronymic,
            IsLeader = addWorkerRequestDto.IsLeader,
            CompanyId = unit.CompanyId,
            Email = addWorkerRequestDto.Email,
            IsCandidate = addWorkerRequestDto.IsCandidate,
            UnitId = addWorkerRequestDto.UnitId,
            Birthday = addWorkerRequestDto.Birthday
        });
        return res.MapDbToDto();
    }

    public async Task<GetWorkerResponseDto> GetWorkerById(int workerId, int recruiterId)
    {
        var res = await _workerRepository.GetWorkerByIdOrThrowAsync(workerId);
        await _companyRepository.CheckUserRecruiterOfCompanyAsync(res.CompanyId, recruiterId);

        return res.MapDbToDto();
    }

    public async Task DeleteWorkerById(int workerId, int recruiterId)
    {
        var res = await _workerRepository.GetWorkerByIdOrThrowAsync(workerId);
        await _companyRepository.CheckUserRecruiterOfCompanyAsync(res.CompanyId, recruiterId);
        await _workerRepository.DeleteWorkerByIdAsync(workerId);
    }

    public async Task<GetWorkerResponseDto> UpdateWorker(UpdateWorkerRequestDto updateWorkerRequestDto, int recruiterId)
    {
        var res = await _workerRepository.GetWorkerByIdOrThrowAsync(updateWorkerRequestDto.Id);
        await _companyRepository.CheckUserRecruiterOfCompanyAsync(res.CompanyId, recruiterId);
        
        if (!string.IsNullOrEmpty(updateWorkerRequestDto.Email) && await _workerRepository.GetWorkerByEmailAsync(updateWorkerRequestDto.Email) is not null)
        {
            throw new WorkerEmailIsExistException();
        }
        
        var newWorker = await _workerRepository.UpdateWorkerAsync(new DbWorker
        {
            Id=updateWorkerRequestDto.Id,
            Name = updateWorkerRequestDto.Name ?? res.Name,
            Surname = updateWorkerRequestDto.Surname ?? res.Surname,
            Patronymic = updateWorkerRequestDto.Patronymic ?? res.Patronymic,
            IsLeader = updateWorkerRequestDto.IsLeader ?? res.IsLeader,
            Email = updateWorkerRequestDto.Email ?? res.Email,
            IsCandidate = updateWorkerRequestDto.IsCandidate ?? res.IsCandidate,
            UnitId = updateWorkerRequestDto.UnitId ?? res.UnitId,
            Birthday = updateWorkerRequestDto.Birthday ?? res.Birthday
        });

        return newWorker.MapDbToDto();
    }

    public async Task<List<GetWorkerResponseDto>> GetCompanyWorkers(int companyId, int recruiterId)
    {
        await _companyRepository.GetCompanyOrThrowAsync(companyId);
        await _companyRepository.CheckUserRecruiterOfCompanyAsync(companyId, recruiterId);
        
        var res = await _workerRepository.GetAllWorkersByCompanyIdAsync(companyId);
        return res.Select(i => i.MapDbToDto()).ToList();
    }
}