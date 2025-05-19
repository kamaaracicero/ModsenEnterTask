namespace EnterTask.Data.DataEntities
{
    public class EventImage : IDataEntity
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public int Number { get; set; }

        public byte[] Data { get; set; }

        public Event Event { get; set; } = null!;

        private EventImage(int id, int eventId, int number, byte[] data)
        {
            Id = id;
            EventId = eventId;
            Number = number;
            Data = data;
        }

        public EventImage(int eventId, int number, byte[] data)
            : this(0, eventId, number, data)
        { }

        public EventImage()
            : this(0, 0, [])
        { }

        public void Update(object? obj)
        {
            if (obj is not EventImage temp) {
                return;
            }

            this.Number = temp.Number;
            this.Data = temp.Data;
        }

        public override int GetHashCode() => Id
            ^ EventId
            ^ (Data != null ? Data.GetHashCode() : 0);

        public override bool Equals(object? obj)
        {
            if (obj is null || !(obj is EventImage)) {
                return false;
            }

            return this.GetHashCode() == obj.GetHashCode();
        }

        public override string ToString() => $"EventImage: {EventId} -> {Number}";
    }
}
