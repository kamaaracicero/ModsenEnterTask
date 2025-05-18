using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;
using EnterTask.DataAccess.Search;
using System.Linq.Expressions;

namespace EnterTask.DataAccess.Repositories
{
    internal class RegistrationRepository : BaseRepository<Registration>, IRepository<Registration>
    {
        public RegistrationRepository(MainDbContext mainDbContext)
            : base(mainDbContext)
        { }

        public async Task<Registration?> GetByIdAsync(params object[] keyValues)
        {
            if (keyValues.Length < 2
                || keyValues[0] is not int firstKey
                || keyValues[1] is not int secondKey)
            {
                return null;
            }
            return await GetByParameterAsync(e => e.ParticipantId == firstKey && e.EventId == secondKey);
        }

        public async Task<IEnumerable<Registration>> GetByIdsAsync(params object[] keyValues)
        {
            List<Registration> result = new List<Registration>();
            foreach (var key in keyValues) {
                if (key is not int[] ids) {
                    continue;
                }

                var search = await GetByParameterAsync(e => e.ParticipantId == ids[0] && e.EventId == ids[1]);
                if (search != null) {
                    result.Add(search);
                }
            }

            return result;
        }

        public async Task<IEnumerable<Registration>> GetPage(PageInfo pageInfo)
            => await base.GetPageAsync(pageInfo);

        public async Task<bool> RemoveAsync(Registration entity)
        {
            var res = await base.RemoveAsync(entity.ParticipantId, entity.EventId);
            await base.SaveChangesAsync();
            return res;
        }

        public async Task<bool> RemoveByIdAsync(params object[] keyValues)
        {
            var res = await base.RemoveAsync(keyValues);
            await base.SaveChangesAsync();
            return res;
        }

        public async Task<bool> UpdateAsync(Registration entity)
        {
            var res = await base.UpdateAsync(entity, entity.ParticipantId, entity.EventId);
            await base.SaveChangesAsync();
            return res;
        }

        public async Task<bool> UpdateByIdAsync(Registration update, params object[] keyValues)
        {
            var res = await base.UpdateAsync(update, keyValues);
            await base.SaveChangesAsync();
            return res;
        }

        public new async Task AddAsync(Registration entity)
        {
            await base.AddAsync(entity);
            await base.SaveChangesAsync();
        }

        public new async Task<bool> ContainsAsync(params object[] keyValues)
            => await base.ContainsAsync(keyValues);

        public new async Task<Registration?> FindByIdAsync(params object[] keyValues)
            => await base.FindByIdAsync(keyValues);

        public new async Task<IEnumerable<Registration>> GetAllAsync(IFilterSettings<Registration>? filterSettings = null,
            PageInfo? pageInfo = null)
            => await base.GetAllAsync(filterSettings, pageInfo);

        public new async Task<Registration?> GetByParameterAsync(Expression<Func<Registration, bool>> predicate)
            => await base.GetByParameterAsync(predicate);

        public async Task<IEnumerable<Registration>> PerformSearchAsync<TParam>(
            IEFSearch<Registration, TParam> search,
            TParam param)
            => await search.SearchAsync(_dbSet, param);

        public new async Task<IEnumerable<Registration>> GetAllWhereAsync(Expression<Func<Registration, bool>> predicate)
            => await base.GetAllWhereAsync(predicate);
    }
}
