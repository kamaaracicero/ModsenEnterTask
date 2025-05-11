namespace EnterTask.WebAPI.DTOs
{
    public class EventPageDTO
    {
        public IEnumerable<EventDTO>? Events { get; set; } = null!;

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int? TotalItems { get; set; } = null!;

        public int? TotalPages { get; set; } = null!;
    }
}
