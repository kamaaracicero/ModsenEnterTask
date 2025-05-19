using System;

namespace EnterTask.Data.DataEntities
{
    public class EventChange : IDataEntity
    {
        private EventChange(int id, int eventId, DateTime date, string paramName, string? oldValue, string? newValue)
        {
            this.Id = id;
            this.EventId = eventId;
            this.Date = date;
            this.ParamName = paramName;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public EventChange(int eventId, DateTime date, string paramName, string? oldValue, string? newValue)
            : this(0, eventId, date, paramName, oldValue, newValue)
        { }

        public EventChange()
            : this(0, new DateTime(2000, 1, 1), string.Empty, null, null)
        { }

        public int Id { get; set; }

        public int EventId { get; set; }

        public DateTime Date { get; set; }

        public string ParamName { get; set; }

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        public Event Event { get; set; } = null!;

        public void Update(object? obj)
        {
            if (obj is null or not EventChange) {
                return;
            }

            var temp = obj as EventChange;
            if (temp != null) {
                this.Date = temp.Date;
                this.ParamName = temp.ParamName;
                this.OldValue = temp.OldValue;
                this.NewValue = temp.NewValue;
            }
        }

        public override int GetHashCode() => Id
            ^ EventId
            ^ Date.GetHashCode()
            ^ (ParamName != null ? ParamName.GetHashCode() : 0)
            ^ (OldValue != null ? OldValue.GetHashCode() : 0)
            ^ (NewValue != null ? NewValue.GetHashCode() : 0);

        public override bool Equals(object? obj)
        {
            if (obj is null or not EventChange) {
                return false;
            }

            return this.GetHashCode() == obj.GetHashCode();
        }

        public override string ToString() => $"EventChange: {ParamName}";
    }
}