using EnterTask.Data.DataEntities;
using EnterTask.Data.Exceptions;
using EnterTask.Data.Repository;
using EnterTask.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.Logic.Repositories.Tracking
{
    [Obsolete]
    internal class EventChangeTrackingRepository : ITrackingRepository<Event, EventChange>
    {
        private readonly MainDbContext _mainDbContext;

        public EventChangeTrackingRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<RepositoryResult> TrackChanges(Event entity)
        {
            try
            {
                // Если до этого entity был получен с помощью метода репозитория GetById,
                // здесь сравнение будет происходить между двумя экземплярами одного и того же объекта
                var original = await _mainDbContext.Events.FirstOrDefaultAsync(e => e.Id == entity.Id);

                if (original != null) {
                    var changes = GetChanges(original, entity);

                    if (changes.Any()) {
                        await _mainDbContext.EventsChange.AddRangeAsync(changes);
                        await _mainDbContext.SaveChangesAsync();
                    }

                    return new RepositoryResult(true);
                }
                else {
                    return new RepositoryResult(false) {
                        Errors = new List<Exception> {
                            new RepositoryException(this, $"Element with id {entity.Id} not found!")
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new RepositoryResult(false) {
                    Errors = new List<Exception> { ex }
                };
            }
        }

        public ICollection<EventChange> GetChanges(Event original, Event update)
        {
            List<EventChange> changes = new List<EventChange>();

            CheckForNameUpdate(changes, original, update);
            CheckForDescriptionUpdate(changes, original, update);
            CheckForStartUpdate(changes, original, update);
            CheckForPlaceUpdate(changes, original, update);
            CheckForCategoryUpdate(changes, original, update);
            CheckForMaxPeopleCountUpdate(changes, original, update);
            // Изменение картинки не отслеживается в связи с методом контроллера. Нужно переделать
            // Тем более пока неизвесто, нужно ли это вообще отслеживать, возможно значения будут
            // содержать не ссылку на картинку, а саму картинку, переведенную в строку.
            // Тогда размер базы данных будет очень быстро разрастаться, из за хранения всех изменений картинки

            return changes;
        }

        private void CheckForNameUpdate(ICollection<EventChange> changes, Event original, Event update)
        {
            if (string.Compare(original.Name, update.Name, StringComparison.OrdinalIgnoreCase) != 0) {
                var change = new EventChange(
                    update.Id,
                    DateTime.Now,
                    nameof(update.Name),
                    original.Name,
                    update.Name);

                changes.Add(change);
            }
        }

        private void CheckForDescriptionUpdate(ICollection<EventChange> changes, Event original, Event update)
        {
            if (string.Compare(original.Description, update.Description, StringComparison.OrdinalIgnoreCase) != 0) {
                var change = new EventChange(
                    update.Id,
                    DateTime.Now,
                    nameof(update.Description),
                    original.Description,
                    update.Description);

                changes.Add(change);
            }
        }

        private void CheckForStartUpdate(ICollection<EventChange> changes, Event original, Event update)
        {
            if (!AreEqualToMinute(original.Start, update.Start)) {
                var change = new EventChange(
                    update.Id,
                    DateTime.Now,
                    nameof(update.Start),
                    original.Start.ToString(),
                    update.Start.ToString());

                changes.Add(change);
            }
        }

        private void CheckForPlaceUpdate(ICollection<EventChange> changes, Event original, Event update)
        {
            if (string.Compare(original.Place, update.Place, StringComparison.OrdinalIgnoreCase) != 0) {
                var change = new EventChange(
                    update.Id,
                    DateTime.Now,
                    nameof(update.Place),
                    original.Place,
                    update.Place);

                changes.Add(change);
            }
        }

        private void CheckForCategoryUpdate(ICollection<EventChange> changes, Event original, Event update)
        {
            if (string.Compare(original.Category, update.Category, StringComparison.OrdinalIgnoreCase) != 0) {
                var change = new EventChange(
                    update.Id,
                    DateTime.Now,
                    nameof(update.Category),
                    original.Category,
                    update.Category);

                changes.Add(change);
            }
        }

        private void CheckForMaxPeopleCountUpdate(ICollection<EventChange> changes, Event original, Event update)
        {
            if (original.MaxPeopleCount != update.MaxPeopleCount) {
                var change = new EventChange(
                    update.Id,
                    DateTime.Now,
                    nameof(update.MaxPeopleCount),
                    original.MaxPeopleCount.ToString(),
                    update.MaxPeopleCount.ToString());

                changes.Add(change);
            }
        }

        private bool AreEqualToMinute(DateTime dt1, DateTime dt2)
        {
            return dt1.Year == dt2.Year &&
                   dt1.Month == dt2.Month &&
                   dt1.Day == dt2.Day &&
                   dt1.Hour == dt2.Hour &&
                   dt1.Minute == dt2.Minute;
        }
    }
}
