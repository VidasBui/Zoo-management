﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zoo_management.Data;

#nullable disable

namespace Zoo_management.Migrations
{
    [DbContext(typeof(ZooDbContext))]
    partial class ZooDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Zoo_management.Data.Entities.Animal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Amount")
                        .HasColumnType("smallint");

                    b.Property<Guid>("EnclosureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Food")
                        .HasColumnType("int");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("EnclosureId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("Zoo_management.Data.Entities.Enclosure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Location")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<short>("assignedAnimalCount")
                        .HasColumnType("smallint");

                    b.Property<bool>("availableForCarnivores")
                        .HasColumnType("bit");

                    b.Property<bool>("availableForHerbivores")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Enclosures");
                });

            modelBuilder.Entity("Zoo_management.Data.Entities.EnclosureObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EnclosureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("EnclosureId");

                    b.ToTable("EnclosureObjects");
                });

            modelBuilder.Entity("Zoo_management.Data.Entities.Animal", b =>
                {
                    b.HasOne("Zoo_management.Data.Entities.Enclosure", "Enclosure")
                        .WithMany()
                        .HasForeignKey("EnclosureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enclosure");
                });

            modelBuilder.Entity("Zoo_management.Data.Entities.EnclosureObject", b =>
                {
                    b.HasOne("Zoo_management.Data.Entities.Enclosure", "Enclosure")
                        .WithMany()
                        .HasForeignKey("EnclosureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enclosure");
                });
#pragma warning restore 612, 618
        }
    }
}
