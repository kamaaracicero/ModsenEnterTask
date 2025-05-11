namespace EnterTask.WebAPI.DTOs
{
    public class NotificationDTO
    {
        public int EventId { get; set; }

        public string Message { get; set; } = null!;

        public DateTime Date { get; set; }

        public string ParamName { get; set; } = null!;

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }
    }
}
