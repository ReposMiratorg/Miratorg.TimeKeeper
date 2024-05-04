﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Miratorg.TimeKeeper.DataAccess.Contexts;

#nullable disable

namespace Miratorg.TimeKeeper.DataAccess.Migrations
{
    [DbContext(typeof(TimeKeeperDbContext))]
    partial class TimeKeeperDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.EmployeeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BossId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodeNav")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ScheduleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BossId");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("StoreId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.PlanEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateInput")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateKey")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOutput")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PlanType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.ScheduleDateEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ScheduleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeBegin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TimeEnd")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("ScheduleDates");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.ScheduleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.StoreEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.EmployeeEntity", b =>
                {
                    b.HasOne("Miratorg.TimeKeeper.DataAccess.Entities.EmployeeEntity", "Boss")
                        .WithMany()
                        .HasForeignKey("BossId");

                    b.HasOne("Miratorg.TimeKeeper.DataAccess.Entities.ScheduleEntity", "Schedule")
                        .WithMany("Employees")
                        .HasForeignKey("ScheduleId");

                    b.HasOne("Miratorg.TimeKeeper.DataAccess.Entities.StoreEntity", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId");

                    b.Navigation("Boss");

                    b.Navigation("Schedule");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.PlanEntity", b =>
                {
                    b.HasOne("Miratorg.TimeKeeper.DataAccess.Entities.EmployeeEntity", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.ScheduleDateEntity", b =>
                {
                    b.HasOne("Miratorg.TimeKeeper.DataAccess.Entities.ScheduleEntity", "Schedule")
                        .WithMany("Dates")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.ScheduleEntity", b =>
                {
                    b.Navigation("Dates");

                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
