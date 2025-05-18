using EnterTask.Data.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterTask.DataAccess.EntityConfigurations
{
    internal class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable(nameof(Person));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ParticipantId)
                .IsRequired();

            builder.Property(x => x.Login)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property(x => x.Role)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(x => x.Login)
                .IsUnique();

            builder.HasOne(x => x.Participant)
                .WithOne(x => x.Person)
                .HasForeignKey<Person>(x => x.ParticipantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Person_Participant_Id");
        }
    }
}
