using AutoMapper;
using EnterTask.Application.Services;
using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterTask.WebAPI.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IEventService _eventService;
        private readonly IEventChangeTrackingService _eventChangeTrackingService;
        private readonly IEventImageService _eventImageService;
        private readonly IMapper _mapper;

        public EventsController(ILogger<EventsController> logger,
            IEventService eventService,
            IEventChangeTrackingService eventChangeTrackingService,
            IEventImageService eventImageService,
            IMapper mapper)
        {
            _logger = logger;
            _eventService = eventService;
            _eventChangeTrackingService = eventChangeTrackingService;
            _eventImageService = eventImageService;
            _mapper = mapper;
        }

        [HttpPost("getall", Name = "GetAllEvents")]
        public async Task<IActionResult> GetAll([FromBody] EventGetSettingsDTO settingsDTO)
        {
            var pageInfo = _mapper.Map<PageInfo>(settingsDTO);
            var filterSettings = _mapper.Map<EventFilterSettings>(settingsDTO);

            var res = await _eventService.GetAllAsync(filterSettings, pageInfo);

            _logger.LogInformation(res.Message);
            return Ok(new EventPageDTO
            {
                Events = _mapper.Map<IEnumerable<EventDTO>>(res.Value),
                Page = pageInfo.Page,
                PageSize = pageInfo.PageSize,
                TotalItems = pageInfo.TotalCount,
                TotalPages = pageInfo.TotalPages
            });
        }

        [HttpGet("getbyid/{id}", Name = "GetEventById")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _eventService.GetEventByIdAsync(id);

            _logger.LogInformation(res.Message);
            return Ok(_mapper.Map<EventDTO>(res.Value));
        }

        [HttpGet("getbyname/{name}", Name = "GetEventByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var res = await _eventService.GetEventByNameAsync(name);

            _logger.LogInformation(res.Message);
            return Ok(_mapper.Map<EventDTO>(res.Value));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("create", Name = "CreateEvent")]
        public async Task<IActionResult> CreateNew([FromBody] EventDTO model)
        {
            var entity = _mapper.Map<Event>(model);
            var res = await _eventService.AddAsync(entity);

            _logger.LogInformation(res.Message);
            return Ok(_mapper.Map<EventDTO>(entity));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("update", Name = "UpdateEvent")]
        public async Task<IActionResult> Update([FromBody] EventDTO model)
        {
            var entity = _mapper.Map<Event>(model);

            var res = await _eventChangeTrackingService.TrackChanges(entity.Id, entity);
            _logger.LogInformation(res.Message);

            res = await _eventService.UpdateAsync(entity.Id, entity);
            _logger.LogInformation(res.Message);

            return Ok();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("remove/{id}", Name = "RemoveEvent")]
        public async Task<IActionResult> Remove(int id)
        {
            var res = await _eventService.DeleteAsync(id);

            _logger.LogInformation(res.Message);
            return Ok();
        }

        [HttpPost("addimage", Name = "AddImageToEvent")]
        public async Task<IActionResult> AddImage([FromBody] EventImageDTO model)
        {
            var entity = _mapper.Map<EventImage>(model);
            var res = await _eventImageService.AddImageToEventAsync(entity.EventId, entity);

            _logger.LogInformation(res.Message);
            return Ok();
        }
    }
}
