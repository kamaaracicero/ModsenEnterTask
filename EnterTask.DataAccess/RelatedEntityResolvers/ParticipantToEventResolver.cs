using EnterTask.Data.DataEntities;
using EnterTask.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.DataAccess.RelatedEntityResolvers
{
    internal class ParticipantToEventResolver : BaseResolver, IRelatedEntityResolver<Participant, Event>
    {
        public ParticipantToEventResolver(MainDbContext dbContext)
            : base(dbContext)
        { }

        public async Task<bool> CheckKey(params object[] keyValues)
        {
            var res = await _mainDbContext.Participants.FindAsync(keyValues);

            if (res != null) {
                return true;
            }
            else {
                return false;
            }
        }

        public async Task<IEnumerable<Event>> GetRelatedAsync(Participant entity)
        {
            return await _mainDbContext.Registrations
                .AsNoTracking()
                .Where(r => r.ParticipantId == entity.Id)
                .Select(r => r.Event)
                .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetRelatedAsync(params object[] keyValues)
        {
            if (keyValues.Length == 0 || keyValues[0] is not int id) {
                throw new ArgumentException("Providen key is not id");
            }

            return await _mainDbContext.Registrations
                .AsNoTracking()
                .Where(r => r.ParticipantId == id)
                .Select(r => r.Event)
                .ToListAsync();
        }
    }
}
