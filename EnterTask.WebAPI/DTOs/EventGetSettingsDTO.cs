namespace EnterTask.WebAPI.DTOs
{
    public class EventGetSettingsDTO
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public DateTime? EventStartMin { get; set; }

        public DateTime? EventStartMax { get; set; }

        public string? Place { get; set; }

        public string? Category { get; set; }
    }
}
