using EnterTask.Data.DataEntities;

namespace EnterTask.Logic.Comparers
{
    public interface IEntityComparer<in TEntity> : IComparer<TEntity>
        where TEntity : class, IDataEntity
    {
    }
}
