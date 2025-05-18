using EnterTask.Data.DataEntities;
using EnterTask.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.Logic.Repositories.Related.Resolvers
{
    [Obsolete]
    internal class EventToEventChangeResolver : IRelatedEntityResolver<Event, EventChange>
    {
        private readonly MainDbContext _mainDbContext;

        public EventToEventChangeResolver(MainDbContext dbContext)
        {
            _mainDbContext = dbContext;
        }

        public async Task AddRelatedAsync(Event entity, EventChange related)
        {
            related.EventId = entity.Id;

            await _mainDbContext.EventsChange.AddAsync(related);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task RemoveRelatedAsync(Event entity, EventChange related)
        {
            var search = await _mainDbContext.EventsChange.FirstOrDefaultAsync(e => e.Id == related.Id);

            if (search == null)
                throw new ArgumentException("Element to delete not found!");

            _mainDbContext.EventsChange.Remove(search);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventChange>> GetRelatedAsync(Event entity)
        {
            return await _mainDbContext.EventsChange
                .Where(e => e.EventId == entity.Id)
                .ToListAsync();
        }
    }
}
