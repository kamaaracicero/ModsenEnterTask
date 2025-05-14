using EnterTask.Data.DataEntities;
using EventData = EnterTask.Data.DataEntities.Event;


namespace EnterTask.Logic.Filter.Event
{
    internal class EventFilter : IFilter<EventData>
    {
        public IEnumerable<EventData> Filter(IEnumerable<EventData> entities, IFilterSettings settings)
        {
            if (settings is not EventFilterSettings temp)
                throw new ArgumentException("Error format filter setting", nameof(settings));

            entities = FilterByDate(entities, temp);
            entities = FilterByPlace(entities, temp);
            entities = FilterByCategory(entities, temp);

            return entities;
        }

        private IEnumerable<EventData> FilterByDate(IEnumerable<EventData> entities, EventFilterSettings settings)
        {
            

            return entities;
        }

        private IEnumerable<EventData> FilterByPlace(IEnumerable<EventData> entities, EventFilterSettings settings)
        {
            

            return entities;
        }

        private IEnumerable<EventData> FilterByCategory(IEnumerable<EventData> entities, EventFilterSettings settings)
        {
            

            return entities;
        }
    }
}
