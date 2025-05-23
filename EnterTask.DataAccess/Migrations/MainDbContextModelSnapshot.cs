﻿// <auto-generated />
using System;
using EnterTask.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnterTask.DataAccess.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EnterTask.Data.DataEntities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("MaxPeopleCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Event", null, t =>
                        {
                            t.HasCheckConstraint("ck_Event_MaxPeopleCount_Positive", "[MaxPeopleCount] >= 0");
                        });
                });

            modelBuilder.Entity("EnterTask.Data.DataEntities.EventChange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("NewValue")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("OldValue")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("ParamName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventChange", (string)null);
                });

            modelBuilder.Entity("EnterTask.Data.DataEntities.EventImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId", "Number")
                        .IsUnique();

                    b.ToTable("EventImage", (string)null);
                });

            modelBuilder.Entity("EnterTask.Data.DataEntities.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Participant", (string)null);
                });

            modelBuilder.Entity("EnterTask.Data.DataEntities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ParticipantId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("ParticipantId")
                        .IsUnique();

                    b.ToTable("Person", (string)null);
                });

            modelBuilder.Entity("EnterTask.Data.DataEntities.Registration", b =>
                {
                    b.Property<int>("ParticipantId")
                        .HasColumnType("int");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.HasKey("ParticipantId", "EventId");

                    b.HasIndex("EventId");

                    b.HasIndex("ParticipantId", "EventId")
                        .IsUnique();

                    b.ToTable("Registration", (string)null);
                });

            modelBuilder.Entity("EnterTask.Data.DataEntities.EventChange", b =>
                {
                    b.HasOne("EnterTask.Data.DataEntities.Event", "Event")
                        .WithMany("Changes")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_EventChange_Event_Id");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EnterTask.Data.DataEntities.EventImage", b =>
                {
                    b.HasOne("EnterTask.Data.DataEntities.Event", "Event")
                        .WithMany("Images")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_EventImage_Event_Id");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EnterTask.Data.DataEntities.Person", b =>
                {
                    b.HasOne("EnterTask.Data.DataEntities.Participant", "Participant")
                        .WithOne("Person")
                        .HasForeignKey("EnterTask.Data.DataEntities.Person", "ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_Person_Participant_Id");

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("EnterTask.Data.DataEntities.Registration", b =>
                {
                    b.HasOne("EnterTask.Data.DataEntities.Event", "Event")
                        .WithMany("Registrations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_Registration_Event_Id");

                    b.HasOne("EnterTask.Data.DataEntities.Participant", "Participant")
                        .WithMany("Registrations")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_Registration_Participant_Id");

                    b.Navigation("Event");

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("EnterTask.Data.DataEntities.Event", b =>
                {
                    b.Navigation("Changes");

                    b.Navigation("Images");

                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("EnterTask.Data.DataEntities.Participant", b =>
                {
                    b.Navigation("Person")
                        .IsRequired();

                    b.Navigation("Registrations");
                });
#pragma warning restore 612, 618
        }
    }
}
