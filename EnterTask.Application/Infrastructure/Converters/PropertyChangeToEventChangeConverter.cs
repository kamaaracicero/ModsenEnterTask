using EnterTask.Application.Infrastructure.Comparers;
using EnterTask.Data.DataEntities;

namespace EnterTask.Application.Infrastructure.Converters
{
    internal class PropertyChangeToEventChangeConverter : IConverter<PropertyChange, EventChange>
    {
        public EventChange Convert(PropertyChange value)
            => new EventChange {
                ParamName = value.PropertyName,
                OldValue = value.OldValue == null ? string.Empty : value.OldValue.ToString(),
                NewValue = value.NewValue == null ? string.Empty : value.NewValue.ToString(),
            };
    }
}
