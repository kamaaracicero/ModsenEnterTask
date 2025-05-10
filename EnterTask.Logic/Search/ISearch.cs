namespace EnterTask.Logic.Search
{
    public interface ISearch<TEntity, TParam>
    {
        Task<IEnumerable<TEntity>> SearchAsync(IQueryable<TEntity> entities, TParam param);
    }
}
