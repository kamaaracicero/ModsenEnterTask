using EnterTask.Data.DataEntities;
using EnterTask.Data.Exceptions;
using EnterTask.Data.Repository;
using EnterTask.Logic.Repositories.Related.Resolvers;
using Microsoft.Extensions.DependencyInjection;

namespace EnterTask.Logic.Repositories.Related
{
    [Obsolete]
    internal class EventRelatedRepository : IRelatedRepository<Event>
    {
        private readonly IServiceProvider _provider;

        public EventRelatedRepository(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<RepositoryResult> AddRelatedAsync<TRelated>(Event data, TRelated related)
            where TRelated : class, IDataEntity, new()
        {
            try
            {
                var resolver = _provider.GetService<IRelatedEntityResolver<Event, TRelated>>();

                if (resolver == null)
                    throw new RepositoryException(this, $"Relation resolver for [{nameof(TRelated)}] not found!");

                await resolver.AddRelatedAsync(data, related);
                return new RepositoryResult(true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult(false) {
                    Errors = new List<Exception> { ex }
                };
            }
        }

        public async Task<RepositoryResult<IEnumerable<TRelated>>> GetRelatedAsync<TRelated>(Event data)
            where TRelated : class, IDataEntity, new()
        {
            try
            {
                var resolver = _provider.GetService<IRelatedEntityResolver<Event, TRelated>>();

                if (resolver == null)
                    throw new RepositoryException(this, $"Relation resolver for [{nameof(TRelated)}] not found!");

                var res = await resolver.GetRelatedAsync(data);
                return new RepositoryResult<IEnumerable<TRelated>>(res, true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult<IEnumerable<TRelated>>([], false) {
                    Errors = new List<Exception> { ex }
                };
            }
        }

        public async Task<RepositoryResult> RemoveRelatedAsync<TRelated>(Event data, TRelated related)
            where TRelated : class, IDataEntity, new()
        {
            try
            {
                var resolver = _provider.GetService<IRelatedEntityResolver<Event, TRelated>>();

                if (resolver == null)
                    throw new RepositoryException(this, $"Relation resolver for [{nameof(TRelated)}] not found!");

                await resolver.RemoveRelatedAsync(data, related);
                return new RepositoryResult(true);
            }
            catch (Exception ex)
            {
                return new RepositoryResult(false) {
                    Errors = new List<Exception> { ex }
                };
            }
        }
    }
}
