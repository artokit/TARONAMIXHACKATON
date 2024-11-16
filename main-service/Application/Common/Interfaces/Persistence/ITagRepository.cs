using Domain.Postgres;

namespace Application.Common.Interfaces.Persistence;

public interface ITagRepository
{
    public Task<DbTag> AddTagAsync(DbTag dbTag);
    public Task DeleteTagAsync(int tagId);
    public Task<List<DbTag>> GetWorkerTagsAsync(int workerId);
    public Task<DbTag?> GetTagByIdAsync(int workerId, int tagId);
}