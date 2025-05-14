using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;

namespace EnterTask.Logic.Repositories
{
    internal class EventRepository : RepositoryBase<Event>, IRepository<Event>
    {
        public EventRepository(MainDbContext mainDbContext)
            : base(mainDbContext)
        { }

        public new async Task<bool> ContainsAsync(params object[] keyValues)
            => await base.ContainsAsync(keyValues);

        public async Task<Event?> GetByIdAsync(params object[] keyValues)
        {
            if (keyValues.Length == 0 || keyValues[0] is not int key)
            {
                return null;
            }
            return await GetByParameterAsync(e => e.Id == key);
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

            if (filter.EventStartMin.HasValue)
                query = query.Where(e => e.Start >= filter.EventStartMin.Value);

            if (filter.EventStartMax.HasValue)
                query = query.Where(e => e.Start <= filter.EventStartMax.Value);

            if (!string.IsNullOrEmpty(filter.Place))
                query = query.Where(e => e.Place.StartsWith(filter.Place));

            if (!string.IsNullOrEmpty(filter.Category))
                query = query.Where(e => e.Category.StartsWith(filter.Category));

            return query;
        }

        public new async Task AddAsync(Event entity)
        {
            await base.AddAsync(entity);
            await base.SaveChangesAsync();
        }

        public new async Task<Event?> FindByIdAsync(params object[] keyValues)
            => await base.FindByIdAsync(keyValues);

        public new async Task<IEnumerable<Event>> GetAllAsync(IFilterSettings<Event>? filterSettings = null)
            => await base.GetAllAsync(filterSettings);
    }
}
