using EnterTask.Data.Repository;

namespace EnterTask.Logic.Repositories.Tracking
{
    public interface ITrackingRepository<TEntity, TTrackingEntity>
    {
        Task<RepositoryResult> TrackChanges(TEntity entity);

        ICollection<TTrackingEntity> GetChanges(TEntity original, TEntity update);
    }
}
