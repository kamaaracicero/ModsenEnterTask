using EnterTask.Data.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterTask.DataAccess.EntityConfigurations
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>

    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable(nameof(Event));

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(e => e.Start)
                .IsRequired();

            builder.Property(e => e.Place)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(e => e.Category)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.MaxPeopleCount)
                .IsRequired();

            builder.ToTable(t => t.HasCheckConstraint("ck_Event_MaxPeopleCount_Positive", "[MaxPeopleCount] >= 0"));

            builder.Property(e => e.Picture);
        }
    }
}
