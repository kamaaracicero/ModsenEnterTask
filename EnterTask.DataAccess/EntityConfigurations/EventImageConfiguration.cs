using EnterTask.Data.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Tracing;

namespace EnterTask.DataAccess.EntityConfigurations
{
    internal class EventImageConfiguration : IEntityTypeConfiguration<EventImage>
    {
        public void Configure(EntityTypeBuilder<EventImage> builder)
        {
            builder.ToTable(nameof(EventImage));

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.EventId)
                .IsRequired();

            builder.Property(e => e.Number)
                .IsRequired();

            builder.Property(e => e.Data)
                .IsRequired();

            builder.HasIndex(e => new { e.EventId, e.Number })
                .IsUnique();

            builder.HasOne(e => e.Event)
                .WithMany(e => e.Images)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_EventImage_Event_Id");
        }
    }
}
