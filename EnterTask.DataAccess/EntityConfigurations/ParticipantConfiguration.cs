using EnterTask.Data.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterTask.DataAccess.EntityConfigurations
{
    internal class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable(nameof(Participant));

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Surname)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.DateOfBirth)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
