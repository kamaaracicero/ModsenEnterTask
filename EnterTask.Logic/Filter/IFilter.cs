namespace EnterTask.Logic.Filter
{
    public interface IFilter<TEntity>
    {
        IEnumerable<TEntity> Filter(IEnumerable<TEntity> entities, IFilterSettings settings);
    }
}
