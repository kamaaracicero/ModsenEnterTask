using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.Search;
using System.Linq.Expressions;

namespace EnterTask.DataAccess.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class, IDataEntity, new()
    {
        Task<IEnumerable<TEntity>> GetAllAsync(IFilterSettings<TEntity>? filterSettings = null,
            PageInfo? pageInfo = null);

        Task<IEnumerable<TEntity>> GetPage(PageInfo pageInfo);

        Task<bool> ContainsAsync(params object[] keyValues);

        Task AddAsync(TEntity entity);

        Task<bool> RemoveAsync(TEntity entity);

        Task<bool> RemoveByIdAsync(params object[] keyValues);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> UpdateByIdAsync(TEntity update, params object[] keyValues);

        Task<TEntity?> GetByIdAsync(params object[] keyValues);

        Task<IEnumerable<TEntity>> GetByIdsAsync(params object[] keyValues);

        Task<TEntity?> FindByIdAsync(params object[] keyValues);

        Task<TEntity?> GetByParameterAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAllWhereAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> PerformSearchAsync<TParam>(IEFSearch<TEntity, TParam> search, TParam param);
    }
}
