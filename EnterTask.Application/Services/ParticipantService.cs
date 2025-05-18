using EnterTask.Data.DataEntities;
using EnterTask.Data.Exceptions;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.Data.Services;
using EnterTask.DataAccess.Repositories;

namespace EnterTask.Application.Services
{
    internal class ParticipantService : IParticipantService
    {
        private readonly IRepository<Participant> _participantRepository;

        public ParticipantService(IRepository<Participant> participantRepository)
        {
            _participantRepository = participantRepository;
        }

        public async Task<ServiceResult> AddAsync(Participant participant)
        {
            await _participantRepository.AddAsync(participant);

            return ServiceResult.Ok("Participant added");
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var res = await _participantRepository.RemoveByIdAsync(id);

            if (res) {
                return ServiceResult.Ok("Participant successfully deleted!");
            }
            else {
                throw new NotFoundWithIdException(nameof(Participant), id);
            }
        }

        public async Task<ServiceResult<IEnumerable<Participant>>> GetAllAsync(
            IFilterSettings<Participant>? filterSettings = null,
            PageInfo? pageInfo = null)
        {
            var res = await _participantRepository.GetAllAsync(filterSettings, pageInfo);

            return ServiceResult<IEnumerable<Participant>>.Ok(res, $"All participants received. Total count {res.Count()}");
        }

        public async Task<ServiceResult<Participant>> GetByIdAsync(int id)
        {
            var res = await _participantRepository.GetByIdAsync(id);

            if (res != null) {
                return ServiceResult<Participant>.Ok(res, "Participant successfully received!");
            }
            else {
                throw new NotFoundWithIdException(nameof(Participant), id);
            }
        }

        public async Task<ServiceResult> UpdateAsync(int id, Participant participant)
        {
            var res = await _participantRepository.UpdateByIdAsync(participant, id);
            if (res) {
                return ServiceResult.Ok("Participant successfully updated!");
            }
            else {
                throw new NotFoundWithIdException(nameof(Participant), id);
            }
        }
    }
}
