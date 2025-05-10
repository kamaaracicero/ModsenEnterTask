using EnterTask.Data.DataEntities;

namespace EnterTask.Logic.Repositories.Related.Resolvers
{
    public interface IRelatedEntityResolver<TEntity, TRelated> : IDisposable
        where TEntity : class, IDataEntity, new()
        where TRelated : class, IDataEntity, new()
    {
        Task<IEnumerable<TRelated>> GetRelatedAsync(TEntity entity);

        Task AddRelatedAsync(TEntity entity, TRelated related);

        Task RemoveRelatedAsync(TEntity entity, TRelated related);
    }
}
