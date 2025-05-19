using EnterTask.Data.DataEntities;
using EnterTask.DataAccess.DbContexts;
using EnterTask.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.UnitTests.Repositories
{
    public class ParticipantRepositoryTests
    {
        private DbContextOptions<MainDbContext> CreateInMemoryOptions()
            => new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        [Fact]
        public async Task AddAsync_ShouldAddParticipant()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            var context = new MainDbContext(options);
            var repository = new ParticipantRepository(context);

            var participant = new Participant("some", "surname",
                DateOnly.FromDateTime(DateTime.Now.AddYears(-25)), "some@email.com");

            // Act
            await repository.AddAsync(participant);

            // Assert
            context.Participants.Should().ContainSingle(e => e.Name == "some");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectParticipant()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            var context = new MainDbContext(options);
            var participant = new Participant { Id = 1, Name = "Some Name", Surname = "Some Surname" };
            context.Participants.Add(participant);
            await context.SaveChangesAsync();

            var repository = new ParticipantRepository(context);

            // Act
            var result = await repository.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be("Some Name");
        }
    }
}
