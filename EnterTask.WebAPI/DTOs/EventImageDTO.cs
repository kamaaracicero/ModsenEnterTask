namespace EnterTask.WebAPI.DTOs
{
    public class EventImageDTO
    {
        public int EventId { get; set; } = 0;

        public int Number { get; set; } = 0;

        public byte[] Data { get; set; } = null!;
    }
}
