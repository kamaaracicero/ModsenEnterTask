namespace EnterTask.Logic.Search
{
    public interface ISearch<TEntity, TParam>
    {
        IEnumerable<TEntity> Search(IEnumerable<TEntity> entities, TParam param);
    }
}
