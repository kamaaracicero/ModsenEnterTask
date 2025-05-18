using EnterTask.Data.DataEntities;
using EnterTask.Data.Exceptions;
using EnterTask.Data.Services;
using EnterTask.DataAccess.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Channels;

namespace EnterTask.Application.Services
{
    internal class EventImageService : IEventImageService
    {
        //private readonly IMemoryCache _memoryCache;
        private readonly IRepository<Event> _eventRepository;
        private readonly IRepository<EventImage> _eventImageRepository;
        //private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

        public EventImageService(//IMemoryCache memoryCache,
            IRepository<Event> eventRepository,
            IRepository<EventImage> eventImageRepository)
        {
            //_memoryCache = memoryCache;
            _eventRepository = eventRepository;
            _eventImageRepository = eventImageRepository;
        }

        public async Task<ServiceResult> AddImageToEventAsync(int eventId, EventImage image)
        {
            var @event = await _eventRepository.GetByIdAsync(eventId);
            if (@event != null) {
                image.EventId = @event.Id;
                await _eventImageRepository.AddAsync(image);

                //_memoryCache.Set(image.Id, image, _cacheDuration);

                return ServiceResult.Ok($"Image added to event. Image id {image.Id}");
            }
            else {
                throw new LinkNotFoundException(nameof(Event), nameof(EventImage), eventId);
            }
        }

        public async Task<ServiceResult<IEnumerable<EventImage>>> GetEventImagesAsync(int eventId)
        {
            // Здесь заменить получение всех элементов, на получение только Id
            var images = await _eventImageRepository.GetAllWhereAsync(e => e.EventId == eventId);
            // List<int> ids = _eventImageRepository.GetIdsWhere(e => e.EventId == eventId);
            // List<EventImage> result = new List<EventImage>();
            // foreach (var image in images) {
            // if (_memoryCache.TryGetValue(image.Id, out EventImage memCache))
            //     images.Add(memCache)
            // }
            return ServiceResult<IEnumerable<EventImage>>.Ok(images, $"Images for event [{eventId}] loaded");
        }

        public async Task<ServiceResult> RemoveEventImageAsync(int id)
        {
            var res = await _eventImageRepository.RemoveByIdAsync(id);
            if (res) {
                return ServiceResult.Ok("Image successfully deleted!");
            }
            else {
                throw new NotFoundWithIdException(nameof(EventImage), id);
            }
        }

        public async Task<ServiceResult> RemoveEventImagesAsync(int eventId)
        {
            var images = await _eventImageRepository.GetAllWhereAsync(e => e.EventId == eventId);
            foreach (var image in images) {
                await _eventImageRepository.RemoveByIdAsync(image.Id);
            }

            return ServiceResult.Ok($"All images deleted. Total count: {images.Count()}");
        }
    }
}
