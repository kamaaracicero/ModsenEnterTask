namespace EnterTask.Logic.Search
{
    public interface IEFSearch<TEntity, TParam>
    {
        Task<IEnumerable<TEntity>> SearchAsync(IQueryable<TEntity> entities, TParam param);
    }
}
