using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;
using EnterTask.DataAccess.Search;
using System.Linq.Expressions;

namespace EnterTask.DataAccess.Repositories
{
    internal class EventChangeRepository : BaseRepository<EventChange>, IRepository<EventChange>
    {
        public EventChangeRepository(MainDbContext mainDbContext)
            : base(mainDbContext)
        { }

        public async Task<EventChange?> GetByIdAsync(params object[] keyValues)
        {
            if (keyValues.Length == 0 || keyValues[0] is not int key)
            {
                return null;
            }
            return await GetByParameterAsync(e => e.Id == key);
        }

        public async Task<IEnumerable<EventChange>> GetByIdsAsync(params object[] keyValues)
        {
            List<EventChange> result = new List<EventChange>();

            foreach (var key in keyValues) {
                if (key is not int id) {
                    continue;
                }

                var search = await GetByParameterAsync(p => p.Id == id);
                if (search != null) {
                    result.Add(search);
                }
            }

            return result;
        }

        public async Task<IEnumerable<EventChange>> GetPage(PageInfo pageInfo)
            => await base.GetPageAsync(pageInfo);

        public async Task<bool> RemoveAsync(EventChange entity)
        {
            var res = await base.RemoveAsync(entity.Id);
            await base.SaveChangesAsync();
            return res;
        }

        public async Task<bool> RemoveByIdAsync(params object[] keyValues)
        {
            var res = await base.RemoveAsync(keyValues);
            await base.SaveChangesAsync();
            return res;
        }

        public async Task<bool> UpdateAsync(EventChange entity)
        {
            var res = await base.UpdateAsync(entity, entity.Id);
            await base.SaveChangesAsync();
            return res;
        }

        public async Task<bool> UpdateByIdAsync(EventChange update, params object[] keyValues)
        {
            var res = await base.UpdateAsync(update, keyValues);
            await base.SaveChangesAsync();
            return res;
        }

        public new async Task AddAsync(EventChange entity)
        {
            await base.AddAsync(entity);
            await base.SaveChangesAsync();
        }

        public new async Task<bool> ContainsAsync(params object[] keyValues)
            => await base.ContainsAsync(keyValues);

        public new async Task<EventChange?> FindByIdAsync(params object[] keyValues)
            => await base.FindByIdAsync(keyValues);

        public new async Task<IEnumerable<EventChange>> GetAllAsync(IFilterSettings<EventChange>? filterSettings = null,
            PageInfo? pageInfo = null)
            => await base.GetAllAsync(filterSettings, pageInfo);

        public new async Task<EventChange?> GetByParameterAsync(Expression<Func<EventChange, bool>> predicate)
            => await base.GetByParameterAsync(predicate);

        public async Task<IEnumerable<EventChange>> PerformSearchAsync<TParam>(
            IEFSearch<EventChange, TParam> search,
            TParam param)
            => await search.SearchAsync(_dbSet, param);

        public new async Task<IEnumerable<EventChange>> GetAllWhereAsync(Expression<Func<EventChange, bool>> predicate)
            => await base.GetAllWhereAsync(predicate);
    }
}
