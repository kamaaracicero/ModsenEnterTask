using EnterTask.Data.DataEntities;
using EnterTask.Data.Exceptions;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;
using EnterTask.Logic.Search;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.Logic.Repositories
{
    internal class EventChangeRepository : RepositoryBase<EventChange>, IRepository<EventChange>
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

        public new async Task<IEnumerable<EventChange>> GetAllAsync(IFilterSettings<EventChange>? filterSettings = null)
            => await base.GetAllAsync(filterSettings);
    }
}
