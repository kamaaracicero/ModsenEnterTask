using EnterTask.Data.DataEntities;

namespace EnterTask.Data.FilterSettings
{
    public interface IFilterSettings<TEntity>
        where TEntity : class, IDataEntity, new()
    { }
}
