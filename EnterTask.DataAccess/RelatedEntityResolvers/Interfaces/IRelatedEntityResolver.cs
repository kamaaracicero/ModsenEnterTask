namespace EnterTask.DataAccess.RelatedEntityResolvers
{
    public interface IRelatedEntityResolver<TParent, TRelated>
    {
        Task<IEnumerable<TRelated>> GetRelatedAsync(TParent entity);

        Task<IEnumerable<TRelated>> GetRelatedAsync(params object[] keyValues);

        Task<bool> CheckKey(params object[] keyValues);
    }
}
