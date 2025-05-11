using EnterTask.Data.DataEntities;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.Logic.Repositories.PaggingRepositories
{
    internal class ParticipantPaggingRepository : IPaggingRepository<Participant>
    {
        private readonly MainDbContext _mainDbContext;

        public ParticipantPaggingRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<RepositoryResult<IEnumerable<Participant>>> GetPage(int page, int pageSize)
        {
            try
            {
                if (page < 1 || pageSize < 1)
                    throw new ArgumentException("Page size and current page must be positive numbers");

                var paged = await _mainDbContext.Participants
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new RepositoryResult<IEnumerable<Participant>>(paged, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<IEnumerable<Participant>>([], false) {
                    Errors = new List<Exception> { ex }
                };
            }
        }

        public async Task<RepositoryResult<(int pages, int elements)>> GetTotalCount(int pageSize)
        {
            try
            {
                var totalItems = await _mainDbContext.Participants.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                return new RepositoryResult<(int pages, int elements)>((totalPages, totalItems), true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<(int pages, int elements)>((-1,-1), false) {
                    Errors = new List<Exception> { ex }
                };
            }
        }
    }
}
