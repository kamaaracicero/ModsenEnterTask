using AutoMapper;
using EnterTask.Data.DataEntities;
using EnterTask.Logic.Filter;
using EnterTask.Logic.Filter.Event;
using EnterTask.Logic.Repositories;
using EnterTask.Logic.Repositories.PaggingRepositories;
using EnterTask.Logic.Repositories.Tracking;
using EnterTask.Logic.Search;
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
        private readonly IPaggingRepository<Event> _paggingEventRepository;
        private readonly ITrackingRepository<Event, EventChange> _trackingEventRepository;
        private readonly IFilter<Event> _filter;
        private readonly IMapper _mapper;

        public EventsController(IRepository<Event> eventRepository,
            IPaggingRepository<Event> paggingEventRepository,
            ITrackingRepository<Event, EventChange> trackingEventRepository,
            IFilter<Event> eventFilter,
            IMapper mapper)
        {
            _eventRepository = eventRepository;
            _paggingEventRepository = paggingEventRepository;
            _trackingEventRepository = trackingEventRepository;
            _filter = eventFilter;
            _mapper = mapper;
        }

        [HttpGet("getall", Name = "GetAllEvents")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _eventRepository.GetAllAsync();
                if (res.Successfully) {
                    var temp = _mapper.Map<IEnumerable<EventDTO>>(res.Value);

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

        [HttpGet("getbyid/{id}", Name = "GetEventById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var res = await _eventRepository.GetByIdAsync(id);
                if (res.Successfully && res.Value != null) {
                    var temp = _mapper.Map<EventDTO>(res.Value);

                    return Ok(temp);
                }
                else if (res.Errors.Any()) {
                    return BadRequest(res.Errors);
                }
                else {
                    return NotFound("Element not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getbyname/{name}", Name = "GetEventByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var search = new EventByNameSearchWithLike();
                var res = await _eventRepository.PerformSearchAsync(search, name);

                if (res.Successfully && res.Value != null) {
                    var temp = _mapper.Map<IEnumerable<EventDTO>>(res.Value);

                    return Ok(temp);
                }
                else if (res.Errors.Any()) {
                    return BadRequest(res.Errors);
                }
                else {
                    return NotFound("Element not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create", Name = "CreateEvent")]
        public async Task<IActionResult> CreateNew([FromBody] EventDTO model)
        {
            try
            {
                var entity = _mapper.Map<Event>(model);
                var res = await _eventRepository.AddAsync(entity);
                if (res.Successfully) {
                    var temp = _mapper.Map<EventDTO>(entity);

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

        [HttpPost("update", Name = "UpdateEvent")]
        public async Task<IActionResult> Update([FromBody] EventDTO model)
        {
            try
            {
                var entity = _mapper.Map<Event>(model);
                var res = await _trackingEventRepository.TrackChanges(entity);
                if (!res.Successfully)
                    return BadRequest(res.Errors);

                res = await _eventRepository.UpdateAsync(entity);
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

        [HttpDelete("remove/{id}", Name = "RemoveEvent")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var res = await _eventRepository.GetByIdAsync(id);
                if (res.Successfully && res.Value == null)
                    return NotFound(res.Errors);

                if (res.Successfully && res.Value != null) {
                    var deleteRes = await _eventRepository.RemoveAsync(res.Value);

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

        [HttpPost("filter", Name = "GetFilteredEvents")]
        public async Task<IActionResult> GetFiltered([FromBody] EventFilterSettingDTO model)
        {
            try
            {
                var res = await _eventRepository.GetAllAsync();
                if (res.Successfully) {
                    var settings = _mapper.Map<EventFilterSettings>(model);
                    return Ok(_filter.Filter(res.Value, settings));
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

        [HttpPost("addimage", Name = "AddImageToEvent")]
        public async Task<IActionResult> AddImage([FromBody] EventImageDTO model)
        {
            try
            {
                var eventRes = await _eventRepository.GetByIdAsync(model.Id);
                if (eventRes.Successfully && eventRes.Value != null) {
                    eventRes.Value.Picture = model.Image;

                    var res = await _eventRepository.UpdateAsync(eventRes.Value);
                    if (res.Successfully) {
                        return Ok();
                    }
                    else {
                        return BadRequest(res.Errors);
                    }
                }
                else {
                    return BadRequest(eventRes.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("getpage", Name = "GetEventPage")]
        public async Task<IActionResult> GetPage([FromBody] EventPageDTO model)
        {
            try
            {
                var res = await _paggingEventRepository.GetPage(model.Page, model.PageSize);
                if (!res.Successfully)
                    return BadRequest(res.Errors);

                var total = await _paggingEventRepository.GetTotalCount(model.PageSize);
                if (!total.Successfully)
                    return BadRequest(res.Errors);

                model.Events = _mapper.Map<IEnumerable<EventDTO>>(res.Value);
                model.TotalItems = total.Value.elements;
                model.TotalPages = total.Value.pages;
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
