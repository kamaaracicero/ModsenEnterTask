namespace EnterTask.WebAPI.DTOs
{
    public class PersonDTO
    {
        public int ParticipantId { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
