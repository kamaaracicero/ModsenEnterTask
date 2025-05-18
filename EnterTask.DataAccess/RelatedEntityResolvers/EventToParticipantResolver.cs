using EnterTask.Data.DataEntities;
using EnterTask.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.DataAccess.RelatedEntityResolvers
{
    internal class EventToParticipantResolver : BaseResolver, IRelatedEntityResolver<Event, Participant>
    {
        public EventToParticipantResolver(MainDbContext dbContext)
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

        public async Task<IEnumerable<Participant>> GetRelatedAsync(params object[] keyValues)
        {
            if (keyValues.Length == 0 || keyValues[0] is not int id){
                throw new ArgumentException("Providen key is not id");
            }

            return await _mainDbContext.Registrations
                .AsNoTracking()
                .Where(r => r.EventId == id)
                .Select(r => r.Participant)
                .ToListAsync();
        }

        public async Task<IEnumerable<Participant>> GetRelatedAsync(Event entity)
        {
            return await _mainDbContext.Registrations
                .AsNoTracking()
                .Where(r => r.EventId == entity.Id)
                .Select(r => r.Participant)
                .ToListAsync();
        }
    }
}
