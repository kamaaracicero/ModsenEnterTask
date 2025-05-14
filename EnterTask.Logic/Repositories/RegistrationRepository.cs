using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;

namespace EnterTask.Logic.Repositories
{
    internal class RegistrationRepository : RepositoryBase<Registration>, IRepository<Registration>
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

        public new async Task<IEnumerable<Registration>> GetAllAsync(IFilterSettings<Registration>? filterSettings = null)
            => await base.GetAllAsync(filterSettings);
    }
}
