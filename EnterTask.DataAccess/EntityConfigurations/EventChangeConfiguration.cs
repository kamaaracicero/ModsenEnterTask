using EnterTask.Data.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterTask.DataAccess.EntityConfigurations
{
    internal class EventChangeConfiguration : IEntityTypeConfiguration<EventChange>
    {
        public void Configure(EntityTypeBuilder<EventChange> builder)
        {
            builder.ToTable(nameof(EventChange));

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.EventId)
                .IsRequired();

            builder.Property(e => e.Message)
                .IsRequired();

            builder.HasOne(e => e.Event)
                .WithMany(ev => ev.Changes)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_EventChange_Event_Id");
        }
    }
}
