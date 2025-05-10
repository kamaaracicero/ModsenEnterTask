using EnterTask.Data.DataEntities;
using EnterTask.Data.Exceptions;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;
using EnterTask.Logic.Search;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.Logic.Repositories
{
    internal class EventChangeRepository : IRepository<EventChange>
    {
        private readonly MainDbContext _dbContext;

        public EventChangeRepository(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RepositoryResult<IEnumerable<EventChange>>> GetAllAsync()
        {
            try
            {
                var res = await _dbContext.EventsChange.ToListAsync();
                return new RepositoryResult<IEnumerable<EventChange>>(res, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<IEnumerable<EventChange>>([], false) {
                    Errors = new List<Exception>() { ex }
                };
            }
        }

        public async Task<RepositoryResult<bool>> ContainsAsync(EventChange entity)
        {
            try
            {
                var elem = await _dbContext.EventsChange.FirstOrDefaultAsync(e => e.Id == entity.Id);
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

        public async Task<RepositoryResult<EventChange?>> GetById(int id)
        {
            try
            {
                var elem = await _dbContext.EventsChange.FirstOrDefaultAsync(e => e.Id == id);
                if (elem != null)
                    return new RepositoryResult<EventChange?>(elem, true);
                else
                    return new RepositoryResult<EventChange?>(null, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<EventChange?>(null, false) {
                    Errors = new List<Exception>() { ex }
                };
            }
        }

        public async Task<RepositoryResult> AddAsync(EventChange entity)
        {
            try
            {
                await _dbContext.EventsChange.AddAsync(entity);
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

        public async Task<RepositoryResult> RemoveAsync(EventChange entity)
        {
            try
            {
                var elem = await _dbContext.EventsChange.FirstOrDefaultAsync(e => e.Id == entity.Id);
                if (elem != null) {
                    _dbContext.EventsChange.Remove(elem);
                    await _dbContext.SaveChangesAsync();

                    return new RepositoryResult(true);
                }
                else
                {
                    return new RepositoryResult(false) {
                        Errors = new List<Exception>() {
                            new RepositoryException(this, $"Element with id {entity.Id} not found!")
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new RepositoryResult(false)
                {
                    Errors = new List<Exception>() { ex }
                };
            }
        }

        public async Task<RepositoryResult> UpdateAsync(EventChange entity)
        {
            try
            {
                var elem = await _dbContext.EventsChange.FirstOrDefaultAsync(e => e.Id == entity.Id);
                if (elem != null) {
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

        public async Task<RepositoryResult<IEnumerable<EventChange>>> PerformSearchAsync<TParam>
            (ISearch<EventChange, TParam> search, TParam param)
        {
            try
            {
                var res = await search.SearchAsync(_dbContext.EventsChange, param);
                return new RepositoryResult<IEnumerable<EventChange>>(res, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<IEnumerable<EventChange>>([], false) {
                    Errors = new List<Exception>() { ex }
                };
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _dbContext.Dispose();

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
