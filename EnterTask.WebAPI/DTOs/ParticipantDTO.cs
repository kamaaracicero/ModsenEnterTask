namespace EnterTask.WebAPI.DTOs
{
    public class ParticipantDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public DateOnly? DateOfBirth { get; set; }

        public string Email { get; set; } = null!;
    }
}
