using EnterTask.Data.DataEntities;
using EnterTask.Data.Repository;

namespace EnterTask.Logic.Repositories.PaggingRepositories
{
    public interface IPaggingRepository<TEntity>
        where TEntity : class, IDataEntity, new()
    {
        Task<RepositoryResult<IEnumerable<TEntity>>> GetPage(int page, int pageSize);

        Task<RepositoryResult<(int pages, int elements)>> GetTotalCount(int pageSize);
    }
}
