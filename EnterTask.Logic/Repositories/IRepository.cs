using EnterTask.Data.DataEntities;
using EnterTask.Data.Repository;
using EnterTask.Logic.Search;

namespace EnterTask.Logic.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class, IDataEntity, new()
    {
        Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync();

        Task<RepositoryResult<bool>> ContainsAsync(TEntity entity);

        Task<RepositoryResult> AddAsync(TEntity entity);

        Task<RepositoryResult> RemoveAsync(TEntity entity);

        Task<RepositoryResult> UpdateAsync(TEntity entity);

        // Возможно поправить возвращаемый результат "true, true" "true, false"
        Task<RepositoryResult<TEntity?>> GetByIdAsync(int id);

        Task<RepositoryResult<IEnumerable<TEntity>>> PerformSearchAsync<TParam>
            (ISearch<TEntity, TParam> search, TParam param);
    }
}
