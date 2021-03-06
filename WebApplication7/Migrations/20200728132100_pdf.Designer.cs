﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication7.Models;

namespace WebApplication7.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20200728132100_pdf")]
    partial class pdf
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication7.Models.Programme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Programme");
                });

            modelBuilder.Entity("WebApplication7.Models.Supervisor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("EMail")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("FullName");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Betreuer");
                });

            modelBuilder.Entity("WebApplication7.Models.Thesis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Bachelor");

                    b.Property<int?>("ContentVal");

                    b.Property<int>("ContentWt");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int?>("DifficultyVal");

                    b.Property<int>("DifficultyWt");

                    b.Property<string>("Evaluation");

                    b.Property<DateTime?>("Filing");

                    b.Property<double>("Grade");

                    b.Property<DateTime?>("LastModified");

                    b.Property<int?>("LayoutVal");

                    b.Property<int>("LayoutWt");

                    b.Property<int?>("LiteratureVal");

                    b.Property<int>("LiteratureWt");

                    b.Property<bool>("Master");

                    b.Property<int?>("NoveltyVal");

                    b.Property<int>("NoveltyWt");

                    b.Property<string>("PdfPath");

                    b.Property<int?>("ProgrammeId");

                    b.Property<DateTime?>("Registration");

                    b.Property<int?>("RichnessVal");

                    b.Property<int>("RichnessWt");

                    b.Property<int>("Status");

                    b.Property<string>("Strenghts");

                    b.Property<int?>("StructureVal");

                    b.Property<int>("StrucutreWt");

                    b.Property<int?>("StudentId");

                    b.Property<string>("StudentName");

                    b.Property<int?>("StyleVal");

                    b.Property<int>("StyleWt");

                    b.Property<string>("Summary");

                    b.Property<int?>("SupervisorId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int?>("Type");

                    b.Property<string>("Weaknesses");

                    b.Property<int?>("WeightSum");

                    b.HasKey("Id");

                    b.HasIndex("ProgrammeId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("Thesen");
                });

            modelBuilder.Entity("WebApplication7.Models.Thesis", b =>
                {
                    b.HasOne("WebApplication7.Models.Programme", "Programme")
                        .WithMany("Thesen")
                        .HasForeignKey("ProgrammeId");

                    b.HasOne("WebApplication7.Models.Supervisor", "Supervisor")
                        .WithMany("Thesen")
                        .HasForeignKey("SupervisorId");
                });
#pragma warning restore 612, 618
        }
    }
}
