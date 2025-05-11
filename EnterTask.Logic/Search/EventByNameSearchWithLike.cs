using EnterTask.Data.DataEntities;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.Logic.Search
{
    internal class EventByNameSearchWithLike : ISearch<Event, string>
    {
        // Поиск с помощью LIKE зависит от настроек коллации базы данных
        public async Task<IEnumerable<Event>> SearchAsync(IQueryable<Event> entities, string param)
        {
            var normalizedParam = param.ToLower().Trim();
            if (string.IsNullOrEmpty(normalizedParam))
                return [];

            var res = await entities
                .AsNoTracking()
                .Where(e => EF.Functions.Like(e.Name, $"{normalizedParam}"))
                .ToListAsync();

            return res;
        }
    }
}
