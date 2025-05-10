using EnterTask.Data.DataEntities;
using EnterTask.Data.Exceptions;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;
using EnterTask.Logic.Search;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.Logic.Repositories
{
    internal class RegistrationRepository : IRepository<Registration>
    {
        private readonly MainDbContext _dbContext;

        public RegistrationRepository(MainDbContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }

        public async Task<RepositoryResult> AddAsync(Registration entity)
        {
            try
            {
                await _dbContext.Registrations.AddAsync(entity);
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

        public async Task<RepositoryResult<bool>> ContainsAsync(Registration entity)
        {
            try
            {
                var elem = await _dbContext.Registrations.FirstOrDefaultAsync(r => r.ParticipantId == entity.ParticipantId
                                                                                && r.EventId == entity.EventId);
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

        public async Task<RepositoryResult<IEnumerable<Registration>>> GetAllAsync()
        {
            try
            {
                var res = await _dbContext.Registrations.ToListAsync();
                return new RepositoryResult<IEnumerable<Registration>>(res, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<IEnumerable<Registration>>([], false) {
                    Errors = new List<Exception>() { ex }
                };
            }
        }

        public async Task<RepositoryResult<Registration?>> GetById(int id)
        {
            return new RepositoryResult<Registration?>(null, false) {
                Errors = new List<Exception>() {
                    new RepositoryException(this, "Default search by id is not available for link element!")
                }
            };
        }

        public async Task<RepositoryResult> RemoveAsync(Registration entity)
        {
            try
            {
                var elem = await _dbContext.Registrations.FirstOrDefaultAsync(r => r.ParticipantId == entity.ParticipantId
                                                                                && r.EventId == entity.EventId);
                if (elem != null) {
                    _dbContext.Registrations.Remove(elem);
                    await _dbContext.SaveChangesAsync();

                    return new RepositoryResult(true);
                }
                else
                {
                    return new RepositoryResult(false) {
                        Errors = new List<Exception>() {
                            new RepositoryException(this, $"Link not found!")
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

        public async Task<RepositoryResult> UpdateAsync(Registration entity)
        {
            try
            {
                var elem = await _dbContext.Registrations.FirstOrDefaultAsync(r => r.ParticipantId == entity.ParticipantId
                                                                                && r.EventId == entity.EventId);
                if (elem != null) {
                    elem.Update(entity);
                    await _dbContext.SaveChangesAsync();
                    return new RepositoryResult(true);
                }
                else
                    return new RepositoryResult(false) {
                        Errors = new List<Exception> {
                            new RepositoryException(this, $"Link not found!")
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

        public async Task<RepositoryResult<IEnumerable<Registration>>> PerformSearchAsync<TParam>
            (ISearch<Registration, TParam> search, TParam param)
        {
            try
            {
                var res = await search.SearchAsync(_dbContext.Registrations, param);
                return new RepositoryResult<IEnumerable<Registration>>(res, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<IEnumerable<Registration>>([], false) {
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
