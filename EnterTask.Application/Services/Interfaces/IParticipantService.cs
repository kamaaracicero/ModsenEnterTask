using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.Data.Services;

namespace EnterTask.Application.Services
{
    public interface IParticipantService
    {
        Task<ServiceResult<Participant>> GetByIdAsync(int id);

        Task<ServiceResult<IEnumerable<Participant>>> GetAllAsync(IFilterSettings<Participant>? filterSettings = null,
            PageInfo ? pageInfo = null);

        Task<ServiceResult> AddAsync(Participant participant);

        Task<ServiceResult> UpdateAsync(int id, Participant participant);

        Task<ServiceResult> DeleteAsync(int id);


    }
}
