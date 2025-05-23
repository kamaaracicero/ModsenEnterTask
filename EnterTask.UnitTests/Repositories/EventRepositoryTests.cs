﻿using EnterTask.Data.DataEntities;
using EnterTask.DataAccess.DbContexts;
using EnterTask.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;

namespace EnterTask.UnitTests.Repositories
{
    public class EventRepositoryTests
    {
        private DbContextOptions<MainDbContext> CreateInMemoryOptions()
            => new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        [Fact]
        public async Task AddAsync_ShouldAddEvent()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            var context = new MainDbContext(options);
            var repository = new EventRepository(context);

            var @event = new Event("test1", "description1", DateTime.Now.AddYears(1), "Some Place", "Category", 1000);

            // Act
            await repository.AddAsync(@event);

            // Assert
            context.Events.Should().ContainSingle(e => e.Name == "test1");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectEvent()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            var context = new MainDbContext(options);
            var @event = new Event { Id = 1, Name = "Some Event", Description = "Some Event Description" };
            context.Events.Add(@event);
            await context.SaveChangesAsync();

            var repository = new EventRepository(context);

            // Act
            var result = await repository.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be("Some Event");
        }
    }
}
