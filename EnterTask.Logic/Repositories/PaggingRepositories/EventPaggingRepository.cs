using EnterTask.Data.DataEntities;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.Logic.Repositories.PaggingRepositories
{
    [Obsolete]
    internal class EventPaggingRepository : IPaggingRepository<Event>
    {
        private readonly MainDbContext _mainDbContext;

        public EventPaggingRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<RepositoryResult<IEnumerable<Event>>> GetPage(int page, int pageSize)
        {
            try
            {
                if (page < 1 || pageSize < 1)
                    throw new ArgumentException("Page size and current page must be numbers greater than 0");

                var paged = await _mainDbContext.Events
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new RepositoryResult<IEnumerable<Event>>(paged, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<IEnumerable<Event>>([], false) {
                    Errors = new List<Exception> { ex }
                };
            }
        }

        public async Task<RepositoryResult<(int pages, int elements)>> GetTotalCount(int pageSize)
        {
            try
            {
                var totalItems = await _mainDbContext.Events.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                return new RepositoryResult<(int pages, int elements)>((totalPages, totalItems), true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<(int pages, int elements)>((-1, -1), false) {
                    Errors = new List<Exception> { ex }
                };
            }
        }
    }
}
