namespace EnterTask.Data.DataEntities
{
    public class Participant : IDataEntity
    {
        private Participant(int id, string name, string surname, DateOnly dateOfBirth, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.DateOfBirth = dateOfBirth;
            this.Email = email;
        }

        public Participant(string name, string surname, DateOnly dateOfBirth, string email)
            : this(0, name, surname, dateOfBirth, email)
        { }

        public Participant()
            : this(string.Empty, string.Empty, new DateOnly(3000, 1, 1), string.Empty)
        { }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Email { get; set; }

        public ICollection<Registration> Registrations { get; set; } = [];

        public Person Person { get; set; } = null!;

        public void Update(object? obj)
        {
            if (obj is null or not Participant)
                return;

            var temp = obj as Participant;
            if (temp != null)
            {
                this.Name = temp.Name;
                this.Surname = temp.Surname;
                this.DateOfBirth = temp.DateOfBirth;
                this.Email = temp.Email;
            }
        }

        public override int GetHashCode() => Id
            ^ (Name != null ? Name.GetHashCode() : 0)
            ^ (Surname != null ? Surname.GetHashCode() : 0)
            ^ DateOfBirth.GetHashCode()
            ^ (Email != null ? Email.GetHashCode() : 0);

        public override bool Equals(object? obj)
        {
            if (obj is null or not Participant)
                return false;

            return this.GetHashCode() == obj.GetHashCode();
        }

        public override string ToString() => $"Participant: {Name} {Surname}";
    }
}