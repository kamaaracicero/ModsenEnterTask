using EnterTask.Application.Infrastructure.Security;
using EnterTask.Application.Services.Interfaces;
using EnterTask.Data.DataEntities;
using EnterTask.Data.Exceptions;
using EnterTask.Data.Services;
using EnterTask.DataAccess.Repositories;

namespace EnterTask.Application.Services
{
    internal class PersonService : IPersonService
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<Participant> _participantRepository;
        private readonly IPasswordHasher _passwordHasher;

        public PersonService(IRepository<Person> personRepository,
            IRepository<Participant> participantRepository,
            IPasswordHasher passwordHasher)
        {
            _personRepository = personRepository;
            _participantRepository = participantRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ServiceResult> AddLoginToParticipantAsync(int participantId, Person person)
        {
            var search = await _personRepository.GetByParameterAsync(p => p.Login == person.Login);
            if (search != null) {
                throw new LoginAlreadyExistsException(person.Login);
            }

            var res = await _participantRepository.GetByIdAsync(participantId);

            if (res != null) {
                person.Password = _passwordHasher.HashPassword(person.Password);

                await _personRepository.AddAsync(person);
                return ServiceResult.Ok("Login successfully added");
            }
            else {
                throw new LinkNotFoundException(nameof(Participant), nameof(Person), participantId);
            }
        }

        public async Task<ServiceResult<Person>> EnsureLoginAsync(string login, string password)
        {
            var check = await _personRepository.GetByParameterAsync(p => p.Login == login);
            if (check != null) {
                var res = _passwordHasher.VerifyPassword(password, check.Password);
                if (res) {
                    return ServiceResult<Person>.Ok(check, "All good.");
                }
            }

            throw new LoginAttemptFailedException();
        }

        public async Task<ServiceResult> DeleteLoginAsync(int participantId)
        {
            var entity = await _personRepository.GetByParameterAsync(p => p.ParticipantId == participantId);
            if (entity != null) {
                var res = await _personRepository.RemoveAsync(entity);
                if (res) {
                    return ServiceResult.Ok("Login successfully deleted");
                }
            }

            throw new NotFoundWithIdException(nameof(Participant), participantId);
        }

        public async Task<ServiceResult<Person>> GetByIdAsync(int participantId)
        {
            var entity = await _personRepository.GetByParameterAsync(p => p.ParticipantId == participantId);

            if (entity != null) {
                return ServiceResult<Person>.Ok(entity, "Login received");
            }
            else {
                throw new NotFoundWithIdException(nameof(Person), participantId);
            }
        }
    }
}
