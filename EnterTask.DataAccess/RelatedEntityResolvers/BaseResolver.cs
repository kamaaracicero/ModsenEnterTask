using EnterTask.DataAccess.DbContexts;

namespace EnterTask.DataAccess.RelatedEntityResolvers
{
    internal abstract class BaseResolver
    {
        protected readonly MainDbContext _mainDbContext;

        protected BaseResolver(MainDbContext dbContext)
        {
            _mainDbContext = dbContext;
        }
    }
}
