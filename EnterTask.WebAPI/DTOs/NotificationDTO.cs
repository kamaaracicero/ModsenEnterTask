namespace EnterTask.WebAPI.DTOs
{
    public class NotificationDTO
    {
        public int ParticipantId { get; set; }

        public int EventId { get; set; }

        public string Message { get; set; } = null!;
    }
}
