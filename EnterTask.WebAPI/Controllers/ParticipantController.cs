using EnterTask.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EnterTask.WebAPI.Controllers
{
    [Route("api/participants")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        [HttpPost("register", Name = "RegisterParticipant")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
            return Ok();
        }

        [HttpGet("getregistered/{eventId}", Name = "GetRegisteredParticipants")]
        public async Task<IEnumerable<ParticipantDTO>> GetRegistered(int eventId)
        {
            return [];
        }

        [HttpGet("getbyid/{id}", Name = "GetParticipantById")]
        public async Task<ParticipantDTO> GetById(int id)
        {
            return null;
        }

        [HttpPost("cancelregistration", Name = "CancelParticipantRegistration")]
        public async Task<IActionResult> CancelRegistration([FromBody] RegistrationDTO model)
        {
            return Ok();
        }

        [HttpPost("receivenotifications", Name = "GetParticipantNotifications")]
        public async Task<IEnumerable<NotificationDTO>> ReceiveNotifications([FromBody] RegistrationDTO model)
        {
            return [];
        }

        [HttpPost("getpage", Name = "GetParticipantPage")]
        public async Task<ParticipantPageDTO> GetPage([FromBody] ParticipantPageDTO model)
        {
            return null;
        }
    }
}
