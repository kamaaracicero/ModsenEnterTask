using EnterTask.Data.DataEntities;
using EnterTask.Data.Exceptions;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.Data.Services;
using EnterTask.DataAccess.Repositories;

namespace EnterTask.Application.Services
{
    internal class EventService : IEventService
    {
        private readonly IRepository<Event> _eventRepository;

        public EventService(IRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<ServiceResult> AddAsync(Event @event)
        {
            await _eventRepository.AddAsync(@event);

            return ServiceResult.Ok($"{nameof(Event)} successfully added. Entity id: {@event.Id}");
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var res = await _eventRepository.RemoveByIdAsync(id);
            if (res) {
                return ServiceResult.Ok("Entity successfully deleted.");
            }
            else {
                throw new NotFoundWithIdException(nameof(Event), id);
            }
        }

        public async Task<ServiceResult<IEnumerable<Event>>> GetAllAsync(
            IFilterSettings<Event>? filterSettings = null,
            PageInfo? pageInfo = null)
        {
            var res = await _eventRepository.GetAllAsync(filterSettings, pageInfo);

            return ServiceResult<IEnumerable<Event>>.Ok(res, "Objects successfully received.");
        }

        public async Task<ServiceResult<Event>> GetEventByIdAsync(int id)
        {
            var res = await _eventRepository.GetByIdAsync(id);
            if (res != null) {
                return ServiceResult<Event>.Ok(res, "Object successfully received");
            }
            else {
                throw new NotFoundWithIdException(nameof(Event), id);
            }
        }

        public async Task<ServiceResult<Event>> GetEventByNameAsync(string name)
        {
            //                      Альтернатива
            // var search = new EventByNameSearchWithLike();
            // var res = await _eventRepository.PerformSearchAsync(search, name);

            var res = await _eventRepository.GetByParameterAsync(e => e.Name == name);
            if (res != null) {
                return ServiceResult<Event>.Ok(res, "Object successfully received");
            }
            else {
                throw new NotFoundWithParamException(nameof(Event), name);
            }
        }

        public async Task<ServiceResult> UpdateAsync(int id, Event @event)
        {
            var res = await _eventRepository.UpdateByIdAsync(@event, id);
            if (res) {
                return ServiceResult.Ok($"{nameof(Event)} updated!");
            }
            else {
                throw new NotFoundWithIdException(nameof(Event), id);
            }
        }
    }
}
