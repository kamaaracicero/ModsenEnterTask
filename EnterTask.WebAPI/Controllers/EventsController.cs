using EnterTask.Data.DataEntities;
using EnterTask.Logic.Repositories;
using EnterTask.WebAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnterTask.WebAPI.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IRepository<Event> _eventRepository;

        public EventsController(IRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet("getall", Name = "GetAllEvents")]
        public async Task<IEnumerable<EventDTO>> GetAll()
        {
            return null;
        }

        [HttpGet("getbyid/{id}", Name = "GetEventById")]
        public async Task<EventDTO> GetById(int id)
        {
            return null;
        }

        [HttpGet("getbyname/{name}", Name = "GetEventByName")]
        public async Task<EventDTO> GetByName(string name)
        {
            return null;
        }

        [HttpPost("create", Name = "CreateEvent")]
        public async Task<IActionResult> CreateNew([FromBody] EventDTO model)
        {
            return Ok();
        }

        [HttpPost("update", Name = "UpdateEvent")]
        public async Task<IActionResult> Update([FromBody] EventDTO model)
        {
            return Ok();
        }

        [HttpDelete("remove/{id}", Name = "RemoveEvent")]
        public async Task<IActionResult> Remove(int id)
        {
            return Ok();
        }

        [HttpPost("filter", Name = "GetFilteredEvents")]
        public async Task<int[]> GetFiltered([FromBody] EventFilterSettingDTO model)
        {
            return [1, 2, 3];
        }

        [HttpPost("addimage", Name = "AddImageToEvent")]
        public async Task<IActionResult> AddImage([FromBody] EventImageDTO model)
        {
            return Ok();
        }
    }
}
