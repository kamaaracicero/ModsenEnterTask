namespace EnterTask.Data.DataEntities
{
    public class EventChange : IDataEntity
    {
        private EventChange(int id, int eventId, string message)
        {
            this.Id = id;
            this.EventId = eventId;
            this.Message = message;
        }

        public EventChange(int eventId, string message)
            : this(0, eventId, message)
        { }

        public EventChange()
            : this(0, string.Empty)
        { }

        public int Id { get; set; }

        public int EventId { get; set; }

        public string Message { get; set; }

        public Event? Event { get; set; }

        public void Update(object? obj)
        {
            if (obj is null or not EventChange)
                return;

            var temp = obj as EventChange;
            if (temp != null)
                this.Message = temp.Message;
        }

        public override int GetHashCode() => Id
            ^ EventId
            ^ Message.GetHashCode();

        public override bool Equals(object? obj)
        {
            if (obj is null or not EventChange)
                return false;

            return this.GetHashCode() == obj.GetHashCode();
        }

        public override string ToString() => $"EventChange: {Message}";
    }
}