using Contracts.Tags.Requests;
using Contracts.Tags.Responses;

namespace Application.Common.Interfaces.Services;

public interface ITagService
{
    public Task<GetWorkerTagResponseDto> AddTag(int recruiterId, AddTagWorkerRequestDto requestDto);
    public Task DeleteWorkerTag(int recruiterId, DeleteTagWorkerRequestDto requestDto);
    public Task<List<GetWorkerTagResponseDto>> GetWorkerTags(int recruiterId, int workerId);
}