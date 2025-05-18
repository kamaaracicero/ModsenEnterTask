namespace EnterTask.Application.Infrastructure.Converters
{
    public interface IConverter<TIn, TOut>
    {
        TOut Convert(TIn value);
    }
}
