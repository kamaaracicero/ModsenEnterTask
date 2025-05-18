using EnterTask.Data.DataEntities;
using EnterTask.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.DataAccess.RelatedEntityResolvers
{
    internal class EventToEventImageResolver : BaseResolver, IRelatedEntityResolver<Event, EventImage>
    {
        public EventToEventImageResolver(MainDbContext dbContext)
            : base(dbContext)
        { }

        public async Task<bool> CheckKey(params object[] keyValues)
        {
            var res = await _mainDbContext.Events.FindAsync(keyValues);

            if (res != null) {
                return true;
            }
            else {
                return false;
            }
        }

        public async Task<IEnumerable<EventImage>> GetRelatedAsync(Event entity)
        {
            return await _mainDbContext.EventImages
                .AsNoTracking()
                .Where(e => e.EventId == entity.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<EventImage>> GetRelatedAsync(params object[] keyValues)
        {
            if (keyValues.Length == 0 || keyValues[0] is not int id) {
                throw new ArgumentException("Providen key is not id");
            }

            return await _mainDbContext.EventImages
                .AsNoTracking()
                .Where(e => e.EventId == id)
                .ToListAsync();
        }
    }
}
