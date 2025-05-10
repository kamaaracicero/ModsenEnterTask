using EnterTask.Data.DataEntities;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.Logic.Search
{
    internal class EventByNameSearch : ISearch<Event, string>
    {
        public async Task<IEnumerable<Event>> SearchAsync(IQueryable<Event> entities, string param)
        {
            var normalizedParam = param.ToLower().Trim();
            if (string.IsNullOrEmpty(normalizedParam))
                return [];

            var res = await entities
                .AsNoTracking()
                .Where(e => e.Name.StartsWith(normalizedParam, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return res;
        }
    }
}
