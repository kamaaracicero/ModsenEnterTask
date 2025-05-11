namespace EnterTask.WebAPI.DTOs
{
    public class EventFilterSettingDTO
    {
        public DateTime? EventStartMin { get; set; }

        public DateTime? EventStartMax { get; set; }

        public string Place { get; set; } = null!;

        public string Category { get; set; } = null!;
    }
}
