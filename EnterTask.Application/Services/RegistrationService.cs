using EnterTask.Data.DataEntities;
using EnterTask.Data.Exceptions;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.Data.Services;
using EnterTask.DataAccess.RelatedEntityResolvers;
using EnterTask.DataAccess.Repositories;

namespace EnterTask.Application.Services
{
    internal class RegistrationService : IRegistrationService
    {
        private readonly IRepository<Registration> _registrationRepository;
        private readonly IRelatedEntityResolver<Event, Participant> _eventToPartResolver;
        private readonly IRelatedEntityResolver<Participant, Event> _partToEventResolver;


        public RegistrationService(IRepository<Registration> registrationRepository,
            IRelatedEntityResolver<Event, Participant> eventToPartResolver,
            IRelatedEntityResolver<Participant, Event> partToEventResolver)
        {
            _registrationRepository = registrationRepository;
            _eventToPartResolver = eventToPartResolver;
            _partToEventResolver = partToEventResolver;
        }

        public async Task<ServiceResult<IEnumerable<Registration>>> GetAll(
            IFilterSettings<Registration>? filterSettings = null,
            PageInfo? pageInfo = null)
        {
            var res = await _registrationRepository.GetAllAsync(filterSettings, pageInfo);

            return ServiceResult<IEnumerable<Registration>>.Ok(res,
                $"Registrations received. Total count: {res.Count()}");
        }

        public async Task<ServiceResult<IEnumerable<Event>>> GetParticipantEvents(int participantId)
        {
            var res = await _partToEventResolver.GetRelatedAsync(participantId);

            return ServiceResult<IEnumerable<Event>>.Ok(res,
                $"Participant events received. Total count: {res.Count()}");
        }

        public async Task<ServiceResult<IEnumerable<Participant>>> GetParticipantsByEventIdAsync(int eventId)
        {
            var res = await _eventToPartResolver.GetRelatedAsync(eventId);

            return ServiceResult<IEnumerable<Participant>>.Ok(res,
                $"Participant events received. Total count: {res.Count()}");
        }

        public async Task<ServiceResult> RegisterParticipantToEventAsync(int participantId, int eventId)
        {
            var partIdCheck = await _partToEventResolver.CheckKey(participantId);
            var eventIdCheck = await _eventToPartResolver.CheckKey(eventId);

            if (partIdCheck && eventIdCheck) {
                var entity = new Registration(participantId, eventId, DateOnly.FromDateTime(DateTime.UtcNow));

                await _registrationRepository.AddAsync(entity);

                return ServiceResult.Ok($"Registration successfully added");
            }
            else if (!partIdCheck) {
                throw new LinkNotFoundException(nameof(Participant), nameof(Registration), eventId);
            }
            else {
                throw new LinkNotFoundException(nameof(Event), nameof(Registration), eventId);
            }
        }

        public async Task<ServiceResult> RemoveRegistrationAsync(int participantId, int eventId)
        {
            var res = await _registrationRepository.RemoveByIdAsync(participantId, eventId);

            if (res) {
                return ServiceResult.Ok("Registration successfully deleted");
            }
            else {
                throw new NotFoundWithIdException(nameof(Registration), participantId, eventId);
            }
        }
    }
}
