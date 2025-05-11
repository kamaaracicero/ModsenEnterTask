using EnterTask.Data.DataEntities;
using EnterTask.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.Logic.Repositories.Related.Resolvers
{
    internal class ParticipantToEventResolver : IRelatedEntityResolver<Participant, Event>
    {
        private readonly MainDbContext _mainDbContext;

        public ParticipantToEventResolver(MainDbContext dbContext)
        {
            _mainDbContext = dbContext;
        }

        public async Task AddRelatedAsync(Participant entity, Event related)
        {
            var link = new Registration(entity.Id, related.Id, DateOnly.FromDateTime(DateTime.Now));

            await _mainDbContext.Registrations.AddAsync(link);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> GetRelatedAsync(Participant entity)
        {
            return await _mainDbContext.Registrations
                .Where(r => r.ParticipantId == entity.Id)
                .Select(r => r.Event)
                .ToListAsync();
        }

        public async Task RemoveRelatedAsync(Participant entity, Event related)
        {
            var search = await _mainDbContext.Registrations.FirstOrDefaultAsync(r => r.EventId == related.Id
                                                                                && r.ParticipantId == entity.Id);

            if (search == null)
                throw new ArgumentException("Element to delete not found!");

            _mainDbContext.Registrations.Remove(search);
            await _mainDbContext.SaveChangesAsync();
        }
    }
}
