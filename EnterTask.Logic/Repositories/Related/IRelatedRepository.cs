using EnterTask.Data.DataEntities;
using EnterTask.Data.Repository;

namespace EnterTask.Logic.Repositories.Related
{
    [Obsolete]
    public interface IRelatedRepository<TEntity>
        where TEntity : class, IDataEntity, new()
    {
        Task<RepositoryResult<IEnumerable<TRelated>>> GetRelatedAsync<TRelated>(TEntity data)
            where TRelated : class, IDataEntity, new();

        Task<RepositoryResult> AddRelatedAsync<TRelated>(TEntity data, TRelated related)
            where TRelated : class, IDataEntity, new();

        Task<RepositoryResult> RemoveRelatedAsync<TRelated>(TEntity data, TRelated related)
            where TRelated : class, IDataEntity, new();
    }
}
