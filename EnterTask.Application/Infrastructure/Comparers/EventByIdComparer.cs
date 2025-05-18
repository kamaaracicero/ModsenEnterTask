using EnterTask.Data.DataEntities;

namespace EnterTask.Application.Infrastructure.Comparers
{
    internal class EventByIdComparer : IEntityComparer<Event>
    {
        public int Compare(Event? x, Event? y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }
            if (x is null)
            {
                return 1;
            }
            if (y is null)
            {
                return -1;
            }

            return x.Id.CompareTo(y.Id);
        }
    }
}
