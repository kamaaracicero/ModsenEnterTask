using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.Data.Services;

namespace EnterTask.Application.Services
{
    public interface IEventService
    {
        Task<ServiceResult<Event>> GetEventByIdAsync(int id);

        Task<ServiceResult<Event>> GetEventByNameAsync(string name);

        Task<ServiceResult<IEnumerable<Event>>> GetAllAsync(IFilterSettings<Event>? filterSettings = null,
            PageInfo? pageInfo = null);

        Task<ServiceResult> AddAsync(Event @event);

        Task<ServiceResult> UpdateAsync(int id, Event @event);

        Task<ServiceResult> DeleteAsync(int id);
    }
}
