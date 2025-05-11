namespace EnterTask.WebAPI.DTOs
{
    public class EventDTO
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Start { get; set; } = default;

        public string Place { get; set; } = null!;

        public string Category { get; set; } = null!;

        public int MaxPeopleCount { get; set; } = 0;

        public string? Picture { get; set; } = null;
    }
}
