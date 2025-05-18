using EnterTask.Data.DataEntities;

namespace EnterTask.Application.Infrastructure.Comparers
{
    public interface IEntityComparer<in TEntity> : IComparer<TEntity>
        where TEntity : class, IDataEntity
    {
    }
}
