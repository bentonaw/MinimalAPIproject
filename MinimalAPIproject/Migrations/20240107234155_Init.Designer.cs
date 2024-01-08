﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MinimalAPIproject.Data;

#nullable disable

namespace MinimalAPIproject.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240107234155_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MinimalAPIproject.Models.Interest", b =>
                {
                    b.Property<int>("InterestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InterestId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InterestId");

                    b.ToTable("Interests");
                });

            modelBuilder.Entity("MinimalAPIproject.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("MinimalAPIproject.Models.PersonInterest", b =>
                {
                    b.Property<int>("PersonInterestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonInterestId"), 1L, 1);

                    b.Property<int>("InterestId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("PersonInterestId");

                    b.HasIndex("InterestId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonInterests");
                });

            modelBuilder.Entity("MinimalAPIproject.Models.PersonInterestLink", b =>
                {
                    b.Property<int>("PersonInterestLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonInterestLinkId"), 1L, 1);

                    b.Property<int>("InterestId")
                        .HasColumnType("int");

                    b.Property<string>("LinkToInterest")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("PersonInterestLinkId");

                    b.HasIndex("InterestId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonInterestLinks");
                });

            modelBuilder.Entity("MinimalAPIproject.Models.PhoneNumber", b =>
                {
                    b.Property<int>("PhoneNumberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhoneNumberId"), 1L, 1);

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("PhoneNumberId");

                    b.HasIndex("PersonId");

                    b.ToTable("PhoneNumbers");
                });

            modelBuilder.Entity("MinimalAPIproject.Models.PersonInterest", b =>
                {
                    b.HasOne("MinimalAPIproject.Models.Interest", "Interest")
                        .WithMany("PersonInterests")
                        .HasForeignKey("InterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinimalAPIproject.Models.Person", "Person")
                        .WithMany("PersonInterests")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interest");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("MinimalAPIproject.Models.PersonInterestLink", b =>
                {
                    b.HasOne("MinimalAPIproject.Models.Interest", "Interest")
                        .WithMany("PersonInterestLinks")
                        .HasForeignKey("InterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinimalAPIproject.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interest");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("MinimalAPIproject.Models.PhoneNumber", b =>
                {
                    b.HasOne("MinimalAPIproject.Models.Person", "Person")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("MinimalAPIproject.Models.Interest", b =>
                {
                    b.Navigation("PersonInterestLinks");

                    b.Navigation("PersonInterests");
                });

            modelBuilder.Entity("MinimalAPIproject.Models.Person", b =>
                {
                    b.Navigation("PersonInterests");

                    b.Navigation("PhoneNumbers");
                });
#pragma warning restore 612, 618
        }
    }
}
