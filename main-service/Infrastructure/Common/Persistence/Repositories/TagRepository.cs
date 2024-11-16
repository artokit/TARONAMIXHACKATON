using Application.Common.Interfaces.Persistence;
using Domain.Postgres;
using Infrastructure.Common.Persistence.Scripts.Tags;
using Infrastructure.Dapper;
using Infrastructure.Dapper.Interfaces;

namespace Infrastructure.Common.Persistence.Repositories;

public class TagRepository : ITagRepository
{
    private IDapperContext _dapperContext;
    
    public TagRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    
    public async Task<DbTag> AddTagAsync(DbTag dbTag)
    {
        var queryObject = new QueryObject(PostgresTagElement.AddTag, new
        {
            worker_id=dbTag.WorkerId,
            type=dbTag.Type,
            name=dbTag.Name
        });
        return await _dapperContext.CommandWithResponse<DbTag>(queryObject);
    }

    public async Task DeleteTagAsync(int tagId)
    {
        var queryObject = new QueryObject(PostgresTagElement.DeleteTag, new {tag_id=tagId});
        await _dapperContext.Command(queryObject);
    }

    public async Task<List<DbTag>> GetWorkerTagsAsync(int workerId)
    {
        var queryObject = new QueryObject(PostgresTagElement.GetWorkerTags, new {worker_id=workerId});
        return await _dapperContext.ListOrDefault<DbTag>(queryObject);
    }

    public async Task<DbTag?> GetTagByIdAsync(int workerId, int tagId)
    {
        var queryObject = new QueryObject(PostgresTagElement.GetTagById, new {tag_id=tagId});
        return await _dapperContext.FirstOrDefault<DbTag>(queryObject);
    }
}