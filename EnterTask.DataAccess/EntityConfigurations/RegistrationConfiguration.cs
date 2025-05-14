using EnterTask.Data.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterTask.DataAccess.EntityConfigurations
{
    internal class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.ToTable(nameof(Registration));

            builder.HasKey(r => new { r.ParticipantId, r.EventId });

            builder.Property(r => r.Date)
                .IsRequired();

            builder.HasIndex(r => new { r.ParticipantId, r.EventId})
                .IsUnique();

            builder.HasOne(r => r.Participant)
                .WithMany(p => p.Registrations)
                .HasForeignKey(r => r.ParticipantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Registration_Participant_Id");

            builder.HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Registration_Event_Id");
        }
    }
}
