using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;
using EnterTask.DataAccess.Search;
using System.Linq.Expressions;

namespace EnterTask.DataAccess.Repositories
{
    internal class PersonRepository : BaseRepository<Person>, IRepository<Person>
    {
        public PersonRepository(MainDbContext mainDbContext)
            : base(mainDbContext)
        { }

        public async Task<Person?> GetByIdAsync(params object[] keyValues)
        {
            if (keyValues.Length == 0 || keyValues[0] is not int key)
            {
                return null;
            }
            return await GetByParameterAsync(e => e.Id == key);
        }

        public async Task<IEnumerable<Person>> GetByIdsAsync(params object[] keyValues)
        {
            List<Person> result = new List<Person>();

            foreach (var key in keyValues)
            {
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

        public async Task<IEnumerable<Person>> GetPage(PageInfo pageInfo)
            => await base.GetPageAsync(pageInfo);

        public async Task<bool> RemoveAsync(Person entity)
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

        public async Task<bool> UpdateAsync(Person entity)
        {
            var res = await base.UpdateAsync(entity, entity.Id);
            await SaveChangesAsync();
            return res;
        }

        public async Task<bool> UpdateByIdAsync(Person update, params object[] keyValues)
        {
            var res = await base.UpdateAsync(update, keyValues);
            await SaveChangesAsync();
            return res;
        }

        public new async Task AddAsync(Person entity)
        {
            await base.AddAsync(entity);
            await base.SaveChangesAsync();
        }

        public new async Task<bool> ContainsAsync(params object[] keyValues)
            => await base.ContainsAsync(keyValues);

        public new async Task<Person?> FindByIdAsync(params object[] keyValues)
            => await base.FindByIdAsync(keyValues);

        public new async Task<IEnumerable<Person>> GetAllAsync(IFilterSettings<Person>? filterSettings = null,
            PageInfo? pageInfo = null)
            => await base.GetAllAsync(filterSettings, pageInfo);

        public new async Task<Person?> GetByParameterAsync(Expression<Func<Person, bool>> predicate)
            => await base.GetByParameterAsync(predicate);

        public async Task<IEnumerable<Person>> PerformSearchAsync<TParam>(
            IEFSearch<Person, TParam> search,
            TParam param)
            => await search.SearchAsync(_dbSet, param);

        public new async Task<IEnumerable<Person>> GetAllWhereAsync(Expression<Func<Person, bool>> predicate)
            => await base.GetAllWhereAsync(predicate);
    }
}
