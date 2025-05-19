using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;
using EnterTask.DataAccess.Search;
using System.Linq.Expressions;

namespace EnterTask.DataAccess.Repositories
{
    internal class EventImageRepository : BaseRepository<EventImage>, IRepository<EventImage>
    {
        public EventImageRepository(MainDbContext mainDbContext)
            : base(mainDbContext)
        { }

        public async Task<EventImage?> GetByIdAsync(params object[] keyValues)
        {
            if (keyValues.Length == 0 || keyValues[0] is not int key) {
                return null;
            }
            return await GetByParameterAsync(e => e.Id == key);
        }

        public async Task<IEnumerable<EventImage>> GetByIdsAsync(params object[] keyValues)
        {
            List<EventImage> result = new List<EventImage>();

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

        public async Task<IEnumerable<EventImage>> GetPage(PageInfo pageInfo)
            => await base.GetPageAsync(pageInfo);

        public async Task<bool> RemoveAsync(EventImage entity)
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

        public async Task<bool> UpdateAsync(EventImage entity)
        {
            var res = await base.UpdateAsync(entity, entity.Id);
            await base.SaveChangesAsync();
            return res;
        }

        public async Task<bool> UpdateByIdAsync(EventImage update, params object[] keyValues)
        {
            var res = await base.UpdateAsync(update, keyValues);
            await base.SaveChangesAsync();
            return res;
        }

        public new async Task AddAsync(EventImage entity)
        {
            await base.AddAsync(entity);
            await base.SaveChangesAsync();
        }

        public new async Task<bool> ContainsAsync(params object[] keyValues)
            => await base.ContainsAsync(keyValues);

        public new async Task<EventImage?> FindByIdAsync(params object[] keyValues)
            => await base.FindByIdAsync(keyValues);

        public new async Task<IEnumerable<EventImage>> GetAllAsync(IFilterSettings<EventImage>? filterSettings = null,
            PageInfo? pageInfo = null)
            => await base.GetAllAsync(filterSettings, pageInfo);

        public new async Task<EventImage?> GetByParameterAsync(Expression<Func<EventImage, bool>> predicate)
            => await base.GetByParameterAsync(predicate);

        public async Task<IEnumerable<EventImage>> PerformSearchAsync<TParam>(
            IEFSearch<EventImage, TParam> search,
            TParam param)
            => await search.SearchAsync(_dbSet, param);

        public new async Task<IEnumerable<EventImage>> GetAllWhereAsync(Expression<Func<EventImage, bool>> predicate)
            => await base.GetAllWhereAsync(predicate);
    }
}
