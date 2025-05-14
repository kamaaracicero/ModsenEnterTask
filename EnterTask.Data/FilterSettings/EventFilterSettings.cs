using EnterTask.Data.DataEntities;

namespace EnterTask.Data.FilterSettings
{
    public class EventFilterSettings : IFilterSettings<Event>
    {
        public DateTime? EventStartMin { get; set; }

        public DateTime? EventStartMax { get; set; }

        public string? Place { get; set; }

        public string? Category { get; set; }
    }
}
