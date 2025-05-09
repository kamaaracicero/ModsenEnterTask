using EnterTask.Data.DataEntities;
using EnterTask.DataAccess.CustomConverters;
using EnterTask.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EnterTask.DataAccess.DbContexts
{
    internal class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        { }

        public DbSet<Event> Events { get; set; } = null!;

        public DbSet<EventChange> EventsChange { get; set; } = null!;

        public DbSet<Participant> Participants { get; set; } = null!;

        public DbSet<Registration> Registrations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new EventChangeConfiguration());
            modelBuilder.ApplyConfiguration(new ParticipantConfiguration());
            modelBuilder.ApplyConfiguration(new RegistrationConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");

            base.ConfigureConventions(configurationBuilder);
        }
    }
}
