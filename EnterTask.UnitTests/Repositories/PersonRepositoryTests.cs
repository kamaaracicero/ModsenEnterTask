using EnterTask.Data.DataEntities;
using EnterTask.DataAccess.DbContexts;
using EnterTask.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.UnitTests.Repositories
{
    public class PersonRepositoryTests
    {
        private DbContextOptions<MainDbContext> CreateInMemoryOptions()
            => new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        [Fact]
        public async Task AddAsync_ShouldAddPerson()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            var context = new MainDbContext(options);
            var repository = new PersonRepository(context);

            var entity = new Person(1, "login", "password", "role");

            // Act
            await repository.AddAsync(entity);

            // Assert
            context.Persons.Should().ContainSingle(e => e.Login == "login");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectPerson()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            var context = new MainDbContext(options);
            var entity = new Person(1, "login", "password", "role");
            context.Persons.Add(entity);
            await context.SaveChangesAsync();

            var repository = new PersonRepository(context);

            // Act
            var result = await repository.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.Login.Should().Be("login");
        }
    }
}
