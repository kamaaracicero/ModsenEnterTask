using AutoMapper;
using EnterTask.Application.Services;
using EnterTask.Data.DataEntities;
using EnterTask.Data.Repository;
using EnterTask.WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterTask.WebAPI.Controllers
{
    [Route("api/participants")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;
        private readonly IRegistrationService _registrationService;
        private readonly IEventChangeTrackingService _eventChangeTrackingService;
        private readonly ILogger<ParticipantController> _logger;
        private readonly IMapper _mapper;

        public ParticipantController(IParticipantService participantService,
            IRegistrationService registrationService,
            IEventChangeTrackingService eventChangeTrackingService,
            ILogger<ParticipantController> logger,
            IMapper mapper)
        {
            _participantService = participantService;
            _registrationService = registrationService;
            _eventChangeTrackingService = eventChangeTrackingService;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize(Policy = "AuthorizedOnly")]
        [HttpPost("register", Name = "RegisterParticipant")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
            var entity = _mapper.Map<Registration>(model);
            var res = await _registrationService.RegisterParticipantToEventAsync(model.ParticipantId, model.EventId);

            _logger.LogInformation(res.Message);
            return Ok();
        }

        [HttpGet("getregistered/{eventId}", Name = "GetRegisteredParticipants")]
        public async Task<IActionResult> GetRegistered(int eventId)
        {
            var res = await _registrationService.GetParticipantsByEventIdAsync(eventId);

            _logger.LogInformation(res.Message);
            return Ok(_mapper.Map<IEnumerable<ParticipantDTO>>(res.Value));
        }

        [HttpGet("getbyid/{id}", Name = "GetParticipantById")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _participantService.GetByIdAsync(id);

            _logger.LogInformation(res.Message);
            return Ok(_mapper.Map<ParticipantDTO>(res.Value));
        }

        [Authorize(Policy = "AuthorizedOnly")]
        [HttpDelete("cancelregistration", Name = "CancelParticipantRegistration")]
        public async Task<IActionResult> CancelRegistration([FromBody] RegistrationDTO model)
        {
            var res = await _registrationService.RemoveRegistrationAsync(model.ParticipantId, model.EventId);

            _logger.LogInformation(res.Message);
            return Ok();
        }

        [HttpPost("notifications/{id}", Name = "GetParticipantNotifications")]
        public async Task<IActionResult> ReceiveNotifications(int id)
        {
            var events = await _registrationService.GetParticipantEvents(id);

            List<EventChange> changes = new List<EventChange>();
            foreach (var @event in events.Value!) {
                var res = await _eventChangeTrackingService.GetAllChangesAsync(@event.Id);

                if (res.Success && res.Value != null) {
                    changes.AddRange(res.Value);
                }
            }

            return Ok(_mapper.Map<IEnumerable<NotificationDTO>>(changes));
        }

        [HttpPost("getall", Name = "GetAllParticipants")]
        public async Task<IActionResult> GetAll([FromBody] ParticipantGetSettingsDTO model)
        {
            var pageInfo = _mapper.Map<PageInfo>(model);

            var res = await _participantService.GetAllAsync(null, pageInfo);
            _logger.LogInformation(res.Message);

            return Ok(_mapper.Map<IEnumerable<ParticipantDTO>>(res.Value));
        }

        [HttpPost("create", Name = "CreateParticipant")]
        public async Task<IActionResult> Create([FromBody] ParticipantDTO model)
        {
            var entity = _mapper.Map<Participant>(model);
            var res = await _participantService.AddAsync(entity);

            _logger.LogInformation(res.Message);
            return Ok(_mapper.Map<ParticipantDTO>(entity));
        }

        [HttpDelete("remove/{id}", Name = "RemoveParticipant")]
        public async Task<IActionResult> Remove(int id)
        {
            var res = await _participantService.DeleteAsync(id);

            _logger.LogInformation(res.Message);
            return Ok();
        }
    }
}
