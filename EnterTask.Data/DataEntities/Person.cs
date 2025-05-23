﻿namespace EnterTask.Data.DataEntities
{
    public class Person : IDataEntity
    {
        private Person(int id, int participantId, string login, string password, string role)
        {
            this.Id = id;
            this.ParticipantId = participantId;
            this.Login = login;
            this.Password = password;
            this.Role = role;
        }

        public Person(int participantId, string login, string password, string role)
            : this(0, 0, login, password, role)
        { }

        public Person()
            : this(0, string.Empty, string.Empty, string.Empty)
        { }

        public int Id { get; set; }

        public int ParticipantId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public Participant Participant { get; set; } = null!;

        public void Update(object? obj)
        {
            if (obj is not Person temp) {
                return;
            }

            this.Login = temp.Login;
            this.Password = temp.Password;
            this.Role = temp.Role;
        }

        public override int GetHashCode() => Id
            ^ ParticipantId
            ^ (Login != null ? Login.GetHashCode() : 0)
            ^ (Password != null ? Password.GetHashCode() : 0)
            ^ (Role != null ? Role.GetHashCode() : 0);

        public override bool Equals(object? obj)
        {
            if (obj is null or not Person) {
                return false;
            }

            return this.GetHashCode() == obj.GetHashCode();
        }

        public override string ToString() => $"Person: {Login}";
    }
}
