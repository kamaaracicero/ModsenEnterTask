using EnterTask.Data.DataEntities;
using EnterTask.Data.Services;

namespace EnterTask.Application.Services.Interfaces
{
    public interface IPersonService
    {
        Task<ServiceResult> AddLoginToParticipantAsync(int participantId, Person person);

        Task<ServiceResult> DeleteLoginAsync(int participantId);

        Task<ServiceResult> CheckLoginAsync(string login, string password);

        Task<ServiceResult<Person>> GetByIdAsync(int participantId);
    }
}
