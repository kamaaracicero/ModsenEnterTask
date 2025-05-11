namespace EnterTask.WebAPI.DTOs
{
    public class EventFilterSettingDTO
    {
        public DateTime? EventStartMin { get; set; } = null!;

        public DateTime? EventStartMax { get; set; } = null!;

        public string? Place { get; set; } = null!;

        public string? Category { get; set; } = null!;
    }
}
