using AutoMapper;
using EnterTask.Data.DataEntities;
using EnterTask.Logic.Repositories;
using EnterTask.Logic.Repositories.PaggingRepositories;
using EnterTask.Logic.Repositories.Related;
using EnterTask.WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterTask.WebAPI.Controllers
{
    [Route("api/participants")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IRepository<Registration> _registrationRepository;
        private readonly IRelatedRepository<Event> _eventRelatedRepository;
        private readonly IRepository<Participant> _participantRepository;
        private readonly IPaggingRepository<Participant> _participantPaggingRepository;
        private readonly IMapper _mapper;

        public ParticipantController(IRepository<Registration> registrationRepository,
            IRelatedRepository<Event> eventRelatedRepository,
            IRepository<Participant> participantRepository,
            IPaggingRepository<Participant> participantPaggingRepository,
            IMapper mapper)
        {
            _registrationRepository = registrationRepository;
            _eventRelatedRepository = eventRelatedRepository;
            _participantRepository = participantRepository;
            _participantPaggingRepository = participantPaggingRepository;
            _mapper = mapper;
        }

        [HttpPost("register", Name = "RegisterParticipant")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
            try
            {
                var entity = _mapper.Map<Registration>(model);
                var res = await _registrationRepository.AddAsync(entity);
                if (res.Successfully) {
                    var temp = _mapper.Map<RegistrationDTO>(entity);
                    return Ok(entity);
                }
                else {
                    return BadRequest(res.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getregistered/{eventId}", Name = "GetRegisteredParticipants")]
        public async Task<IActionResult> GetRegistered(int eventId)
        {
            try
            {
                // По факту нужно было сначала саму сущность найти
                var res = await _eventRelatedRepository.GetRelatedAsync<Participant>(new Event() { Id = eventId });

                if (res.Successfully) {
                    var temp = _mapper.Map<IEnumerable<ParticipantDTO>>(res.Value);

                    return Ok(temp);
                }
                else {
                    return BadRequest(res.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getbyid/{id}", Name = "GetParticipantById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var res = await _participantRepository.GetByIdAsync(id);

                if (res.Successfully && res.Value != null) {
                    var temp = _mapper.Map<ParticipantDTO>(res.Value);

                    return Ok(temp);
                }
                else if (res.Successfully && res.Value == null) {
                    return NotFound(res.Errors);
                }
                else {
                    return BadRequest(res.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("cancelregistration", Name = "CancelParticipantRegistration")]
        public async Task<IActionResult> CancelRegistration([FromBody] RegistrationDTO model)
        {
            try
            {
                // Тут можно найти ивент и участника и удалить через _eventRelated
                var res = await _registrationRepository.RemoveAsync(new Registration() {
                    ParticipantId = model.ParticipantId,
                    EventId = model.EventId
                });

                if (res.Successfully) {
                    return Ok();
                }
                else {
                    return BadRequest(res.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("notifications/{id}", Name = "GetParticipantNotifications")]
        public async Task<IActionResult> ReceiveNotifications(int id)
        {
            try
            {
                var registrations = await _registrationRepository.GetAllAsync();
                if (registrations.Successfully) {
                    List<NotificationDTO> result = new List<NotificationDTO>();
                    foreach (var registration in registrations.Value.Where(r => r.ParticipantId == id)) {
                        var notifications = await _eventRelatedRepository
                            .GetRelatedAsync<EventChange>(new Event() { Id = registration.EventId });

                        if (notifications.Successfully)
                            result.AddRange(_mapper.Map<IEnumerable<NotificationDTO>>(notifications.Value));
                    }

                    return Ok(result);
                }
                else {
                    return BadRequest(registrations.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("getpage", Name = "GetParticipantPage")]
        public async Task<IActionResult> GetPage([FromBody] ParticipantPageDTO model)
        {
            try
            {
                var res = await _participantPaggingRepository.GetPage(model.Page, model.PageSize);
                if (!res.Successfully)
                    return BadRequest(res.Errors);

                var total = await _participantPaggingRepository.GetTotalCount(model.PageSize);
                if (!total.Successfully)
                    return BadRequest(res.Errors);

                model.Participants = _mapper.Map<IEnumerable<ParticipantDTO>>(res.Value);
                model.TotalItems = total.Value.elements;
                model.TotalPages = total.Value.pages;
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create", Name = "CreateParticipant")]
        public async Task<IActionResult> Create([FromBody] ParticipantDTO model)
        {
            try
            {
                var entity = _mapper.Map<Participant>(model);
                var res = await _participantRepository.AddAsync(entity);
                if (res.Successfully) {
                    var temp = _mapper.Map<ParticipantDTO>(entity);

                    return Ok(temp);
                }
                else
                {
                    return BadRequest(res.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("remove/{id}", Name = "RemoveParticipant")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var res = await _participantRepository.GetByIdAsync(id);
                if (res.Successfully && res.Value == null)
                    return NotFound(res.Errors);

                if (res.Successfully && res.Value != null) {
                    var deleteRes = await _participantRepository.RemoveAsync(res.Value);

                    if (deleteRes.Successfully) {
                        return Ok();
                    }
                    else {
                        return BadRequest(deleteRes.Errors);
                    }
                }
                else {
                    return BadRequest(res.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
