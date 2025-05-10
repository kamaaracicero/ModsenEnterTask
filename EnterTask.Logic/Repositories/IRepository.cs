using EnterTask.Data.DataEntities;
using EnterTask.Data.Repository;

namespace EnterTask.Logic.Repositories
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class, IDataEntity, new()
    {
        Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync();

        Task<RepositoryResult<bool>> ContainsAsync(TEntity entity);

        Task<RepositoryResult> AddAsync(TEntity entity);

        Task<RepositoryResult> RemoveAsync(TEntity entity);

        Task<RepositoryResult> UpdateAsync(TEntity entity);

        Task<RepositoryResult<TEntity?>> GetById(int id);
    }
}
