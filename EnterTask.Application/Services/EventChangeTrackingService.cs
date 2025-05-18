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
                var changes = ObjectComparer<Event>.Compare(@event, update);
                foreach (var change in changes)
                {
                    var entity = _converter.Convert(change);
                    entity.EventId = eventId;
                    entity.Date = DateTime.UtcNow;

                    await _eventChangeRepository.AddAsync(entity);
                }

                return ServiceResult.Ok($"All changes added. Total count {changes.Count()}");
            }
            else {
                throw new LinkNotFoundException(nameof(Event), nameof(EventChange), eventId);
            }
        }
    }
}
