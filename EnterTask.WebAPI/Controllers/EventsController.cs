using AutoMapper;
using EnterTask.Data.DataEntities;
using EnterTask.WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnterTask.WebAPI.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IMapper _mapper;

        public EventsController(ILogger<EventsController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("getall", Name = "GetAllEvents")]
        public async Task<IActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("getbyid/{id}", Name = "GetEventById")]
        public async Task<IActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getbyname/{name}", Name = "GetEventByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("create", Name = "CreateEvent")]
        public async Task<IActionResult> CreateNew([FromBody] EventDTO model)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("update", Name = "UpdateEvent")]
        public async Task<IActionResult> Update([FromBody] EventDTO model)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("remove/{id}", Name = "RemoveEvent")]
        public async Task<IActionResult> Remove(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("addimage", Name = "AddImageToEvent")]
        public async Task<IActionResult> AddImage([FromBody] EventImageDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
