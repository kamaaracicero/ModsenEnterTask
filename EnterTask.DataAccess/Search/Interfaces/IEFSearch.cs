namespace EnterTask.DataAccess.Search
{
    public interface IEFSearch<TEntity, TParam>
    {
        Task<IEnumerable<TEntity>> SearchAsync(IQueryable<TEntity> entities, TParam param);
    }
}
