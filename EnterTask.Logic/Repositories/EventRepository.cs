using EnterTask.Data.DataEntities;
using EnterTask.Data.Exceptions;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;
using EnterTask.Logic.Search;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.Logic.Repositories
{
    internal class EventRepository : IRepository<Event>
    {
        private readonly MainDbContext _dbContext;

        public EventRepository(MainDbContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }

        public async Task<RepositoryResult> AddAsync(Event entity)
        {
            try
            {
                await _dbContext.Events.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return new RepositoryResult(true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult(false) {
                    Errors = new List<Exception> { ex }
                };
            }
        }

        public async Task<RepositoryResult<bool>> ContainsAsync(Event entity)
        {
            try
            {
                var elem = await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == entity.Id);
                if (elem != null)
                    return new RepositoryResult<bool>(true, true);
                else
                    return new RepositoryResult<bool>(false, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<bool>(false, false) {
                    Errors = new List<Exception>() { ex }
                };
            }
        }

        public async Task<RepositoryResult<Event?>> GetById(int id)
        {
            try
            {
                var elem = await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);
                if (elem != null)
                    return new RepositoryResult<Event?>(elem, true);
                else
                    return new RepositoryResult<Event?>(null, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<Event?>(null, false) {
                    Errors = new List<Exception>() { ex }
                };
            }
        }

        public async Task<RepositoryResult<IEnumerable<Event>>> GetAllAsync()
        {
            try
            {
                var res = await _dbContext.Events.ToListAsync();
                return new RepositoryResult<IEnumerable<Event>>(res, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<IEnumerable<Event>>([], false) {
                    Errors = new List<Exception>() { ex }
                };
            }
        }

        public async Task<RepositoryResult> RemoveAsync(Event entity)
        {
            try
            {
                var elem = await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == entity.Id);
                if (elem != null) {
                    _dbContext.Events.Remove(elem);
                    await _dbContext.SaveChangesAsync();

                    return new RepositoryResult(true);
                }
                else {
                    return new RepositoryResult(false) {
                        Errors = new List<Exception>() {
                            new RepositoryException(this, $"Element with id {entity.Id} not found!")
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new RepositoryResult(false) {
                    Errors = new List<Exception>() { ex }
                };
            }
        }

        public async Task<RepositoryResult> UpdateAsync(Event entity)
        {
            try
            {
                var elem = await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == entity.Id);
                if (elem != null)
                {
                    elem.Update(entity);
                    await _dbContext.SaveChangesAsync();
                    return new RepositoryResult(true);
                }
                else
                    return new RepositoryResult(false) {
                        Errors = new List<Exception> {
                            new RepositoryException(this, $"Element with id {entity.Id} not found!")
                        }
                    };
            }
            catch (Exception ex)
            {
                return new RepositoryResult<bool>(false, false) {
                    Errors = new List<Exception>() { ex }
                };
            }
        }

        public async Task<RepositoryResult<IEnumerable<Event>>> PerformSearchAsync<TParam>
            (ISearch<Event, TParam> search, TParam param)
        {
            try
            {
                var res = await search.SearchAsync(_dbContext.Events, param);
                return new RepositoryResult<IEnumerable<Event>>(res, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<IEnumerable<Event>>([], false) {
                    Errors = new List<Exception>() { ex }
                };
            }
        }
    }
}
