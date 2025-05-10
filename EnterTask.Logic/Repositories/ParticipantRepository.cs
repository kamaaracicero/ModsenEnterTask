using EnterTask.Data.DataEntities;
using EnterTask.Data.Exceptions;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;
using EnterTask.Logic.Search;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.Logic.Repositories
{
    internal class ParticipantRepository : IRepository<Participant>
    {
        private readonly MainDbContext _dbContext;

        public ParticipantRepository(MainDbContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }

        public async Task<RepositoryResult> AddAsync(Participant entity)
        {
            try
            {
                await _dbContext.Participants.AddAsync(entity);
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

        public async Task<RepositoryResult<bool>> ContainsAsync(Participant entity)
        {
            try
            {
                var elem = await _dbContext.Participants.FirstOrDefaultAsync(e => e.Id == entity.Id);
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

        public async Task<RepositoryResult<Participant?>> GetById(int id)
        {
            try
            {
                var elem = await _dbContext.Participants.FirstOrDefaultAsync(e => e.Id == id);
                if (elem != null)
                    return new RepositoryResult<Participant?>(elem, true);
                else
                    return new RepositoryResult<Participant?>(null, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<Participant?>(null, false) {
                    Errors = new List<Exception>() { ex }
                };
            }
        }

        public async Task<RepositoryResult<IEnumerable<Participant>>> GetAllAsync()
        {
            try
            {
                var res = await _dbContext.Participants.ToListAsync();
                return new RepositoryResult<IEnumerable<Participant>>(res, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<IEnumerable<Participant>>([], false) {
                    Errors = new List<Exception>() { ex }
                };
            }
        }

        public async Task<RepositoryResult> RemoveAsync(Participant entity)
        {
            try
            {
                var elem = await _dbContext.Participants.FirstOrDefaultAsync(e => e.Id == entity.Id);
                if (elem != null) {
                    _dbContext.Participants.Remove(elem);
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

        public async Task<RepositoryResult> UpdateAsync(Participant entity)
        {
            try
            {
                var elem = await _dbContext.Participants.FirstOrDefaultAsync(e => e.Id == entity.Id);
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

        public async Task<RepositoryResult<IEnumerable<Participant>>> PerformSearchAsync<TParam>
            (ISearch<Participant, TParam> search, TParam param)
        {
            try
            {
                var res = await search.SearchAsync(_dbContext.Participants, param);
                return new RepositoryResult<IEnumerable<Participant>>(res, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<IEnumerable<Participant>>([], false) {
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
