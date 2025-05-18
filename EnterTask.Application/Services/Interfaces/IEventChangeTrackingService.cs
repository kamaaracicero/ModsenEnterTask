using EnterTask.Data.DataEntities;
using EnterTask.Data.Services;

namespace EnterTask.Application.Services
{
    public interface IEventChangeTrackingService
    {
        Task<ServiceResult> TrackChanges(int eventId, Event update);

        Task<ServiceResult> DeleteAllChanges(int eventId);

        Task<ServiceResult<IEnumerable<EventChange>>> GetAllChangesAsync(int eventId);
    }
}
