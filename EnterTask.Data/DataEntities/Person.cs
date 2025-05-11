namespace EnterTask.Data.DataEntities
{
    public class Person : IDataEntity
    {
        private Person(int id, int participantId, string login, string password, string role)
        {
            this.Id = id;
            this.Login = login;
            this.Password = password;
            this.Role = role;
        }

        public Person(string login, string password, string role)
            : this(0, 0, login, password, role)
        { }

        public Person()
            : this(string.Empty, string.Empty, string.Empty)
        { }

        public int Id { get; set; }

        public int? ParticipantId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public void Update(object? obj)
        {
            if (obj is not Person temp)
                return;

            this.ParticipantId = temp.Id;
            this.Login = temp.Login;
            this.Password = temp.Password;
            this.Role = temp.Role;
        }

        public override int GetHashCode() => Id
            ^ (ParticipantId.HasValue ? ParticipantId.Value : 0)
            ^ (Login != null ? Login.GetHashCode() : 0)
            ^ (Password != null ? Password.GetHashCode() : 0)
            ^ (Role != null ? Role.GetHashCode() : 0);

        public override bool Equals(object? obj)
        {
            if (obj is null or not Person)
                return false;

            return this.GetHashCode() == obj.GetHashCode();
        }

        public override string ToString() => $"Person: {Login}";
    }
}
