namespace EnterTask.Data.DataEntities
{
    public class Event : IDataEntity
    {
        private Event(int id, string name, string description, DateTime start, string place, string category, int maxPeopleCount)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Start = start;
            this.Place = place;
            this.Category = category;
            this.MaxPeopleCount = maxPeopleCount;
        }

        public Event(string name, string description, DateTime start, string place, string category, int maxPeopleCount)
            : this(0, name, description, start, place, category, maxPeopleCount)
        { }

        public Event()
            : this(string.Empty, string.Empty, new DateTime(2000, 1, 1), string.Empty, string.Empty, 0)
        { }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public string Place { get; set; }

        public string Category { get; set; }

        public int MaxPeopleCount { get; set; }

        public ICollection<Registration> Registrations { get; set; } = [];

        public ICollection<EventChange> Changes { get; set; } = [];

        public ICollection<EventImage> Images { get; set; } = [];

        public void Update(object? obj)
        {
            if (obj is null or not Event) {
                return;
            }

            var temp = obj as Event;
            if (temp != null) {
                this.Name = temp.Name;
                this.Description = temp.Description;
                this.Start = temp.Start;
                this.Place = temp.Place;
                this.Category = temp.Category;
                this.MaxPeopleCount = temp.MaxPeopleCount;
            }
        }

        public override int GetHashCode() => Id
            ^ (Name != null ? Name.GetHashCode() : 0)
            ^ (Description != null ? Description.GetHashCode() : 0)
            ^ Start.GetHashCode()
            ^ (Place != null ? Place.GetHashCode() : 0)
            ^ (Category != null ? Category.GetHashCode() : 0)
            ^ MaxPeopleCount;

        public override bool Equals(object? obj)
        {
            if (obj is null or not Event) {
                return false;
            }

            return this.GetHashCode() == obj.GetHashCode();
        }

        public override string ToString() => $"Event: {Name}";
    }
}