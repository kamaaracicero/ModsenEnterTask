using EnterTask.Data.DataEntities;
using EnterTask.DataAccess.DbContexts;
using EnterTask.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.UnitTests.Repositories
{
    public class RegistrationRepositoryTests
    {
        private DbContextOptions<MainDbContext> CreateInMemoryOptions()
            => new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        [Fact]
        public async Task AddAsync_ShouldAddRegistration()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            var context = new MainDbContext(options);
            var repository = new RegistrationRepository(context);

            var registration = new Registration(1, 1, DateOnly.FromDateTime(DateTime.Now));

            // Act
            await repository.AddAsync(registration);

            // Assert
            context.Registrations.Should().ContainSingle(e => e.EventId == 1 && e.ParticipantId == 1);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectRegistration()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            var context = new MainDbContext(options);
            var registration = new Registration { EventId = 1, ParticipantId = 1 };
            context.Registrations.Add(registration);
            await context.SaveChangesAsync();

            var repository = new RegistrationRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result!.Count().Should().Be(1);
        }
    }
}
