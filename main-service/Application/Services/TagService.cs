using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Application.Exceptions.Tags;
using Application.Mappers;
using Application.Services.Common.RepositoriesExtensions;
using Contracts.Tags.Requests;
using Contracts.Tags.Responses;
using Domain.Postgres;

namespace Application.Services;

public class TagService : ITagService
{
    private ITagRepository _tagRepository;
    private IWorkerRepository _workerRepository;
    private ICompanyRepository _companyRepository;
    
    public TagService(ITagRepository tagRepository, IWorkerRepository workerRepository, ICompanyRepository companyRepository)
    {
        _tagRepository = tagRepository;
        _workerRepository = workerRepository;
        _companyRepository = companyRepository;
    }
    
    public async Task<GetWorkerTagResponseDto> AddTag(int recruiterId, AddTagWorkerRequestDto requestDto)
    {
        var worker = await _workerRepository.GetWorkerByIdOrThrowAsync(requestDto.WorkerId);
        await _companyRepository.CheckUserRecruiterOfCompanyAsync(worker.CompanyId, recruiterId);
        return (await _tagRepository.AddTagAsync(new DbTag
            { WorkerId = worker.Id, Name = requestDto.Name, Type = requestDto.Type })).MapDbToDto();
    }

    public async Task DeleteWorkerTag(int recruiterId, DeleteTagWorkerRequestDto requestDto)
    {
        var worker = await _workerRepository.GetWorkerByIdOrThrowAsync(requestDto.WorkerId);
        await _companyRepository.CheckUserRecruiterOfCompanyAsync(worker.CompanyId, recruiterId);
        var res = await _tagRepository.GetTagByIdAsync(requestDto.WorkerId, requestDto.TagId);
        if (res is null)
        {
            throw new TagNotFoundException();
        }
        
        await _tagRepository.DeleteTagAsync(requestDto.TagId);
    }

    public async Task<List<GetWorkerTagResponseDto>> GetWorkerTags(int recruiterId, int workerId)
    {
        var worker = await _workerRepository.GetWorkerByIdOrThrowAsync(workerId);
        await _companyRepository.CheckUserRecruiterOfCompanyAsync(worker.CompanyId, recruiterId);
        return (await _tagRepository.GetWorkerTagsAsync(workerId)).Select(i => i.MapDbToDto()).ToList();
    }
}