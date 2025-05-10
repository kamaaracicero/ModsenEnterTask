namespace EnterTask.Logic.Filter.Event
{
    internal class EventFilterSettings : IFilterSettings
    {
        public DateTime? EventStartMin { get; set; }

        public DateTime? EventStartMax { get; set; }

        public string? Place { get; set; }

        public string? Category { get; set; }
    }
}
