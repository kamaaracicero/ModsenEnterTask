using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.Data.Services;

namespace EnterTask.Application.Services
{
    public interface IRegistrationService
    {
        Task<ServiceResult<IEnumerable<Registration>>> GetAll(IFilterSettings<Registration>? filterSettings = null,
            PageInfo? pageInfo = null);

        Task<ServiceResult<IEnumerable<Participant>>> GetParticipantsByEventIdAsync(int eventId);

        Task<ServiceResult> RegisterParticipantToEventAsync(int participantId, int eventId);

        Task<ServiceResult> RemoveRegistrationAsync(int participantId, int eventId);

        Task<ServiceResult<IEnumerable<Event>>> GetParticipantEvents(int participantId);
    }
}
