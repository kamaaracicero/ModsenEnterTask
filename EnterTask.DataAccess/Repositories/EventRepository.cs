using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;
using EnterTask.DataAccess.Search;
using System.Linq.Expressions;

namespace EnterTask.DataAccess.Repositories
{
    internal class EventRepository : BaseRepository<Event>, IRepository<Event>
    {
        public EventRepository(MainDbContext mainDbContext)
            : base(mainDbContext)
        { }

        public new async Task<bool> ContainsAsync(params object[] keyValues)
            => await base.ContainsAsync(keyValues);

        public async Task<Event?> GetByIdAsync(params object[] keyValues)
        {
            if (keyValues.Length == 0 || keyValues[0] is not int key) {
                return null;
            }
            return await GetByParameterAsync(e => e.Id == key);
        }

        public async Task<IEnumerable<Event>> GetByIdsAsync(params object[] keyValues)
        {
            List<Event> result = new List<Event>();

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

        public async Task<IEnumerable<Event>> GetPage(PageInfo pageInfo)
            => await base.GetPageAsync(pageInfo);

        public async Task<bool> RemoveAsync(Event entity)
        {
            var res = await base.RemoveAsync(entity.Id);
            await SaveChangesAsync();
            return res;
        }

        public async Task<bool> RemoveByIdAsync(params object[] keyValues)
        {
            var res = await base.RemoveAsync(keyValues);
            await SaveChangesAsync();
            return res;
        }

        public async Task<bool> UpdateAsync(Event entity)
        {
            var res = await base.UpdateAsync(entity, entity.Id);
            await SaveChangesAsync();
            return res;
        }

        public async Task<bool> UpdateByIdAsync(Event update, params object[] keyValues)
        {
            var res = await base.UpdateAsync(update, keyValues);
            await SaveChangesAsync();
            return res;
        }

        protected override IQueryable<Event> ApplyFilter(IQueryable<Event> query, IFilterSettings<Event>? settings)
        {
            if (settings is not EventFilterSettings filter) {
                return query;
            }

            if (filter.EventStartMin.HasValue) {
                query = query.Where(e => e.Start >= filter.EventStartMin.Value);
            }
            if (filter.EventStartMax.HasValue) {
                query = query.Where(e => e.Start <= filter.EventStartMax.Value);
            }
            if (!string.IsNullOrEmpty(filter.Place)) {
                query = query.Where(e => e.Place.StartsWith(filter.Place));
            }
            if (!string.IsNullOrEmpty(filter.Category)) {
                query = query.Where(e => e.Category.StartsWith(filter.Category));
            }

            return query;
        }

        public new async Task AddAsync(Event entity)
        {
            await base.AddAsync(entity);
            await base.SaveChangesAsync();
        }

        public new async Task<Event?> FindByIdAsync(params object[] keyValues)
            => await base.FindByIdAsync(keyValues);

        public new async Task<IEnumerable<Event>> GetAllAsync(IFilterSettings<Event>? filterSettings = null,
            PageInfo? pageInfo = null)
            => await base.GetAllAsync(filterSettings, pageInfo);

        public new async Task<Event?> GetByParameterAsync(Expression<Func<Event, bool>> predicate)
            => await base.GetByParameterAsync(predicate);

        public async Task<IEnumerable<Event>> PerformSearchAsync<TParam>(
            IEFSearch<Event, TParam> search,
            TParam param)
            => await search.SearchAsync(_dbSet, param);

        public new async Task<IEnumerable<Event>> GetAllWhereAsync(Expression<Func<Event, bool>> predicate)
            => await base.GetAllWhereAsync(predicate);
    }
}
