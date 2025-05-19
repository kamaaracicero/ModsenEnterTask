using EnterTask.Data.DataEntities;
using EnterTask.DataAccess.DbContexts;
using EnterTask.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.UnitTests.Repositories
{
    public class EventImageRepositoryTests
    {
        private DbContextOptions<MainDbContext> CreateInMemoryOptions()
            => new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        [Fact]
        public async Task AddAsync_ShouldAddEventImage()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            var context = new MainDbContext(options);
            var repository = new EventImageRepository(context);

            var entity = new EventImage(1, 1, Convert.FromBase64String("testdata"));
            await repository.AddAsync(entity);

            context.EventImages.Should().ContainSingle(e => e.Number == 1);

        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectEventImage()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            var context = new MainDbContext(options);
            var entity = new EventImage(1, 1, Convert.FromBase64String("testdata"));
            context.EventImages.Add(entity);
            await context.SaveChangesAsync();

            var repository = new EventImageRepository(context);

            // Act
            var result = await repository.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.Number.Should().Be(1);
        }

    }
}
