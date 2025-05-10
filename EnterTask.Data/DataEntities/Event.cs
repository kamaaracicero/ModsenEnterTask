namespace EnterTask.Data.DataEntities
{
    public class Event : IDataEntity
    {
        private Event(int id, string name, string description, DateTime start, string place, string category, int maxPeopleCount, string? picture)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Start = start;
            this.Place = place;
            this.Category = category;
            this.MaxPeopleCount = maxPeopleCount;
            this.Picture = picture;
        }

        public Event(string name, string description, DateTime start, string place, string category, int maxPeopleCount)
            : this(0, name, description, start, place, category, maxPeopleCount, null)
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

        public string? Picture { get; set; }

        public ICollection<Registration> Registrations { get; set; } = [];

        public ICollection<EventChange> Changes { get; set; } = [];

        public void Update(object? obj)
        {
            if (obj is null or not Event)
                return;

            var temp = obj as Event;
            if (temp != null)
            {
                this.Name = temp.Name;
                this.Description = temp.Description;
                this.Start = temp.Start;
                this.Place = temp.Place;
                this.Category = temp.Category;
                this.MaxPeopleCount = temp.MaxPeopleCount;
                this.Picture = temp.Picture;
            }
        }

        public override int GetHashCode() => Id
            ^ Name.GetHashCode()
            ^ Description.GetHashCode()
            ^ Start.GetHashCode()
            ^ Place.GetHashCode()
            ^ Category.GetHashCode()
            ^ MaxPeopleCount
            ^ (Picture == null ? 0 : Picture.GetHashCode());

        public override bool Equals(object? obj)
        {
            if (obj is null or not Event)
                return false;

            return this.GetHashCode() == obj.GetHashCode();
        }

        public override string ToString() => $"Event: {Name}";
    }
}