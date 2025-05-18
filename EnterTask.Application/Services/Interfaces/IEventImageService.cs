using EnterTask.Data.DataEntities;
using EnterTask.Data.Services;

namespace EnterTask.Application.Services
{
    public interface IEventImageService
    {
        Task<ServiceResult> AddImageToEventAsync(int eventId, EventImage image);

        Task<ServiceResult<IEnumerable<EventImage>>> GetEventImagesAsync(int eventId);

        Task<ServiceResult> RemoveEventImagesAsync(int eventId);

        Task<ServiceResult> RemoveEventImageAsync(int id);
    }
}
