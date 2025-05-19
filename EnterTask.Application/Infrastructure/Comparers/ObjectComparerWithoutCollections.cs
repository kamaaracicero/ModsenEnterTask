using System.Reflection;

namespace EnterTask.Application.Infrastructure.Comparers
{
    public static class ObjectComparerWithoutCollections<TValue>
        where TValue : class
    {
        public static List<PropertyChange> Compare(TValue oldObj, TValue newObj)
        {
            ArgumentNullException.ThrowIfNull(oldObj);
            ArgumentNullException.ThrowIfNull(newObj);

            var changes = new List<PropertyChange>();
            var props = typeof(TValue).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props) {
                if (!prop.CanRead
                    || prop.GetIndexParameters().Length > 0) {
                    continue;
                }

                var oldValue = prop.GetValue(oldObj);
                var newValue = prop.GetValue(newObj);

                if (!Equals(oldValue, newValue)) {
                    changes.Add(new PropertyChange {
                        PropertyName = prop.Name,
                        OldValue = oldValue,
                        NewValue = newValue
                    });
                }
            }

            return changes;
        }
    }
}
