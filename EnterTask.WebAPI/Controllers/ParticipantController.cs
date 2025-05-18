using AutoMapper;
using EnterTask.Data.DataEntities;
using EnterTask.WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterTask.WebAPI.Controllers
{
    [Route("api/participants")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ParticipantController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("register", Name = "RegisterParticipant")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getregistered/{eventId}", Name = "GetRegisteredParticipants")]
        public async Task<IActionResult> GetRegistered(int eventId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getbyid/{id}", Name = "GetParticipantById")]
        public async Task<IActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("cancelregistration", Name = "CancelParticipantRegistration")]
        public async Task<IActionResult> CancelRegistration([FromBody] RegistrationDTO model)
        {
            throw new NotImplementedException();
        }

        [HttpPost("notifications/{id}", Name = "GetParticipantNotifications")]
        public async Task<IActionResult> ReceiveNotifications(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("getpage", Name = "GetParticipantPage")]
        public async Task<IActionResult> GetPage([FromBody] ParticipantPageDTO model)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create", Name = "CreateParticipant")]
        public async Task<IActionResult> Create([FromBody] ParticipantDTO model)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("remove/{id}", Name = "RemoveParticipant")]
        public async Task<IActionResult> Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
