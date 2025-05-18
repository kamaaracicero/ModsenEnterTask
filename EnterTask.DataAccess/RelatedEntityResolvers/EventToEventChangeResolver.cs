using EnterTask.Data.DataEntities;
using EnterTask.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.DataAccess.RelatedEntityResolvers
{
    internal class EventToEventChangeResolver : BaseResolver, IRelatedEntityResolver<Event, EventChange>
    {
        public EventToEventChangeResolver(MainDbContext dbContext)
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

        public async Task<IEnumerable<EventChange>> GetRelatedAsync(Event entity)
        {
            return await _mainDbContext.EventsChange
                .AsNoTracking()
                .Where(e => e.EventId == entity.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<EventChange>> GetRelatedAsync(params object[] keyValues)
        {
            if (keyValues.Length == 0 || keyValues[0] is not int id) {
                throw new ArgumentException("Providen key is not id");
            }

            return await _mainDbContext.EventsChange
                .AsNoTracking()
                .Where(e => e.EventId == id)
                .ToListAsync();
        }
    }
}
