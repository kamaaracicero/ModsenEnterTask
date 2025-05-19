using EnterTask.Application.Infrastructure.Comparers;
using EnterTask.Application.Infrastructure.Converters;
using EnterTask.Data.DataEntities;
using EnterTask.Data.Exceptions;
using EnterTask.Data.Services;
using EnterTask.DataAccess.Repositories;

namespace EnterTask.Application.Services
{
    internal class EventChangeTrackingService : IEventChangeTrackingService
    {
        private readonly IRepository<Event> _eventRepository;
        private readonly IRepository<EventChange> _eventChangeRepository;
        private readonly IConverter<PropertyChange, EventChange> _converter;

        public EventChangeTrackingService(IRepository<Event> eventRepository,
            IRepository<EventChange> eventChangeRepository,
            IConverter<PropertyChange, EventChange> converter)
        {
            _eventRepository = eventRepository;
            _eventChangeRepository = eventChangeRepository;
            _converter = converter;
        }

        public async Task<ServiceResult> DeleteAllChanges(int eventId)
        {
            var changes = await _eventChangeRepository.GetAllWhereAsync(e => e.EventId == eventId);
            foreach (var change in changes) {
                await _eventChangeRepository.RemoveByIdAsync(change.Id);
            }

            return ServiceResult.Ok($"All changes deleted. Total count: {changes.Count()}");
        }

        public async Task<ServiceResult<IEnumerable<EventChange>>> GetAllChangesAsync(int eventId)
        {
            var changes = await _eventChangeRepository.GetAllWhereAsync(e => e.EventId == eventId);

            return ServiceResult<IEnumerable<EventChange>>
                .Ok(changes, $"All changes received. Total count: {changes.Count()}");
        }

        public async Task<ServiceResult> TrackChanges(int eventId, Event update)
        {
            var @event = await _eventRepository.GetByIdAsync(eventId);
            if (@event != null) {
                // Записывает также Registrations, Changes, Images
                // var changes = ObjectComparerWithoutCollections<Event>.Compare(@event, update);
                var changes = Compare(@event, update);
                // foreach (var change in changes)
                foreach (var entity in changes) {
                    // var entity = _converter.Convert(change);
                    // entity.EventId = eventId;
                    // entity.Date = DateTime.UtcNow;

                    await _eventChangeRepository.AddAsync(entity);
                }

                return ServiceResult.Ok($"All changes added. Total count {changes.Count()}");
            }
            else {
                throw new LinkNotFoundException(nameof(Event), nameof(EventChange), eventId);
            }
        }

        private IEnumerable<EventChange> Compare(Event old, Event @new)
        {
            List<EventChange> changes = new List<EventChange>();
            if (!Equals(old.Name, @new.Name)) {
                changes.Add(new EventChange(@new.Id, DateTime.UtcNow,
                    nameof(old.Name), old.Name, @new.Name));
            }

            if (!Equals(old.Description, @new.Description)) {
                changes.Add(new EventChange(@new.Id, DateTime.UtcNow,
                    nameof(old.Description), old.Description, @new.Description));
            }

            if (!Equals(old.Start, @new.Start)) {
                changes.Add(new EventChange(@new.Id, DateTime.UtcNow,
                    nameof(old.Start), old.Start.ToString(), @new.Start.ToString()));
            }

            if (!Equals(old.Place, @new.Place)) {
                changes.Add(new EventChange(@new.Id, DateTime.UtcNow,
                    nameof(old.Place), old.Place, @new.Place));
            }

            if (!Equals(old.Category, @new.Category)) {
                changes.Add(new EventChange(@new.Id, DateTime.UtcNow,
                    nameof(old.Category), old.Category, @new.Category));
            }

            if (!Equals(old.MaxPeopleCount, @new.MaxPeopleCount)) {
                changes.Add(new EventChange(@new.Id, DateTime.UtcNow,
                    nameof(old.MaxPeopleCount), old.MaxPeopleCount.ToString(), @new.MaxPeopleCount.ToString()));
            }

            return changes;
        }
    }
}
