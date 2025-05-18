namespace EnterTask.Application.Infrastructure.Comparers
{
    public class PropertyChange
    {
        public string PropertyName { get; set; } = "";

        public object? OldValue { get; set; }

        public object? NewValue { get; set; }

        public override string ToString()
            => $"{PropertyName}: {OldValue} -> {NewValue}";
    }
}
