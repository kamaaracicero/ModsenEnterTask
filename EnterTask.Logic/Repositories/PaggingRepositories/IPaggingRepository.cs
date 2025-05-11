using EnterTask.Data.DataEntities;
using EnterTask.Data.Repository;

namespace EnterTask.Logic.Repositories.PaggingRepositories
{
    // Пагинацию возможно надо было оформить не как репозиторий, а как универсальный класс.
    // Но такой вариант не нагружает оперативку большим количеством элементов из базы данных
    public interface IPaggingRepository<TEntity>
        where TEntity : class, IDataEntity, new()
    {
        Task<RepositoryResult<IEnumerable<TEntity>>> GetPage(int page, int pageSize);

        Task<RepositoryResult<(int pages, int elements)>> GetTotalCount(int pageSize);
    }
}
