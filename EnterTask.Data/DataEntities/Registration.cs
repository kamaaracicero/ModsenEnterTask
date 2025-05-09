namespace EnterTask.Data.DataEntities
{
    public class Registration : IDataEntity
    {
        public Registration(int participantId, int eventId, DateOnly date)
        {
            this.ParticipantId = participantId;
            this.EventId = eventId;
            this.Date = date;
        }

        public Registration()
            : this(0, 0, new DateOnly(2000, 1, 1))
        { }

        public int ParticipantId { get; set; }

        public int EventId { get; set; }

        public DateOnly Date { get; set; }

        public Participant? Participant { get; set; }

        public Event? Event { get; set; }

        public void Update(object? obj)
        {
            if (obj is null or not Registration)
                return;

            var temp = obj as Registration;
            if (temp != null)
                this.Date = temp.Date;
        }

        public override int GetHashCode() => ParticipantId
            ^ EventId
            ^ Date.GetHashCode();

        public override bool Equals(object? obj)
        {
            if (obj is null or not Registration)
                return false;

            return this.GetHashCode() == obj.GetHashCode();
        }

        public override string ToString() => $"Registration: {ParticipantId}->{EventId}";
    }
}