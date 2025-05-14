using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnterTask.Logic.Repositories
{
    internal abstract class RepositoryBase<TEntity>
        where TEntity : class, IDataEntity, new()
    {
        protected readonly MainDbContext _mainDbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected RepositoryBase(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
            _dbSet = mainDbContext.Set<TEntity>();
        }

        protected async Task<TEntity?> FindByIdAsync(params object[] keyValues)
            => await _dbSet.FindAsync(keyValues);

        protected virtual IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query,
            IFilterSettings<TEntity>? settings)
            => query;

        protected async Task<IEnumerable<TEntity>> GetAllAsync(IFilterSettings<TEntity>? settings = null)
        {
            var query = _dbSet.AsNoTracking();
            query = ApplyFilter(query, settings);
            return await query.ToListAsync();
        }

        protected async Task<TEntity?> GetByParameterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate);
        }

        protected async Task<IEnumerable<TEntity>> GetPageAsync(PageInfo pageInfo)
        {
            if (pageInfo.Page < 1 || pageInfo.PageSize < 1) {
                return [];
            }

            var res = await _dbSet
                .AsNoTracking()
                .Skip((pageInfo.Page - 1) * pageInfo.PageSize)
                .Take(pageInfo.PageSize)
                .ToListAsync();

            pageInfo.TotalCount = await _dbSet.CountAsync();
            pageInfo.TotalPages = (int)Math.Ceiling((double)pageInfo.TotalCount / pageInfo.PageSize);

            return res;
        }

        protected async Task AddAsync(TEntity entity)
            => await _dbSet.AddAsync(entity);

        protected async Task<bool> RemoveAsync(params object[] keyValues)
        {
            var res = await _dbSet.FindAsync(keyValues);
            if (res != null) {
                _dbSet.Remove(res);
                return true;
            }
            else {
                return false;
            }
        }

        protected async Task<bool> UpdateAsync(TEntity entity, params object[] keyValues)
        {
            var res = await _dbSet.FindAsync(keyValues);
            if (res != null) {
                res.Update(entity);
                return true;
            }
            else {
                return false;
            }
        }

        protected async Task SaveChangesAsync()
            => await _mainDbContext.SaveChangesAsync();

        protected async Task<bool> ContainsAsync(params object[] keyValues)
        {
            var res = await _dbSet.FindAsync(keyValues);
            if (res != null) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
