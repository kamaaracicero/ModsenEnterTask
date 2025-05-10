using EnterTask.Data.DataEntities;
using EventData = EnterTask.Data.DataEntities.Event;


namespace EnterTask.Logic.Filter.Event
{
    internal class EventFilter : IFilter<EventData>
    {
        public IEnumerable<EventData> Filter(IEnumerable<EventData> entities, IFilterSettings settings)
        {
            if (settings is null or not EventFilterSettings)
                throw new ArgumentException("Error format filter setting", nameof(settings));

            var temp = settings as EventFilterSettings;
            if (temp != null) {
                FilterByDate(entities, temp);
                FilterByPlace(entities, temp);
                FilterByCategory(entities, temp);

                return entities;
            }
            else {
                return entities;
            }
        }

        private void FilterByDate(IEnumerable<EventData> entities, EventFilterSettings settings)
        {
            if (settings.EventStartMin.HasValue)
                entities = entities.Where(e => e.Start >= settings.EventStartMin.Value);

            if (settings.EventStartMax.HasValue)
                entities = entities.Where(e => e.Start <= settings.EventStartMax.Value);
        }

        private void FilterByPlace(IEnumerable<EventData> entities, EventFilterSettings settings)
        {
            if (!string.IsNullOrEmpty(settings.Place))
                entities = entities.Where(e => e.Place.StartsWith(settings.Place, StringComparison.OrdinalIgnoreCase));
        }

        private void FilterByCategory(IEnumerable<EventData> entities, EventFilterSettings settings)
        {
            if (!string.IsNullOrEmpty(settings.Category))
                entities = entities.Where(e => e.Category.StartsWith(settings.Category, StringComparison.OrdinalIgnoreCase));
        }
    }
}
