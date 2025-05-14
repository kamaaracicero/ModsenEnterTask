using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;

namespace EnterTask.Logic.Repositories
{
    internal class ParticipantRepository : RepositoryBase<Participant>, IRepository<Participant>
    {
        public ParticipantRepository(MainDbContext mainDbContext)
            : base(mainDbContext)
        { }

        public async Task<Participant?> GetByIdAsync(params object[] keyValues)
        {
            if (keyValues.Length == 0 || keyValues[0] is not int key)
            {
                return null;
            }
            return await GetByParameterAsync(e => e.Id == key);
        }

        public async Task<IEnumerable<Participant>> GetPage(PageInfo pageInfo)
            => await base.GetPageAsync(pageInfo);

        public async Task<bool> RemoveAsync(Participant entity)
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

        public async Task<bool> UpdateAsync(Participant entity)
        {
            var res = await base.UpdateAsync(entity, entity.Id);
            await SaveChangesAsync();
            return res;
        }

        public async Task<bool> UpdateByIdAsync(Participant update, params object[] keyValues)
        {
            var res = await base.UpdateAsync(update, keyValues);
            await SaveChangesAsync();
            return res;
        }

        public new async Task AddAsync(Participant entity)
        {
            await base.AddAsync(entity);
            await base.SaveChangesAsync();
        }

        public new async Task<bool> ContainsAsync(params object[] keyValues)
            => await base.ContainsAsync(keyValues);

        public new async Task<Participant?> FindByIdAsync(params object[] keyValues)
            => await base.FindByIdAsync(keyValues);

        public new async Task<IEnumerable<Participant>> GetAllAsync(IFilterSettings<Participant>? filterSettings = null)
            => await base.GetAllAsync(filterSettings);
    }
}
