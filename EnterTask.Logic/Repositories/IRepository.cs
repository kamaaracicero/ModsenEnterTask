using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;

namespace EnterTask.Logic.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class, IDataEntity, new()
    {
        Task<IEnumerable<TEntity>> GetAllAsync(IFilterSettings<TEntity>? filterSettings = null);

        Task<IEnumerable<TEntity>> GetPage(PageInfo pageInfo);

        Task<bool> ContainsAsync(params object[] keyValues);

        Task AddAsync(TEntity entity);

        Task<bool> RemoveAsync(TEntity entity);

        Task<bool> RemoveByIdAsync(params object[] keyValues);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> UpdateByIdAsync(TEntity update, params object[] keyValues);

        Task<TEntity?> GetByIdAsync(params object[] keyValues);

        Task<TEntity?> FindByIdAsync(params object[] keyValues);
    }
}
