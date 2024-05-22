﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Miratorg.TimeKeeper.DataAccess.Contexts;

#nullable disable

namespace Miratorg.TimeKeeper.DataAccess.Migrations
{
    [DbContext(typeof(TimeKeeperDbContext))]
    [Migration("20240522154513_Update024")]
    partial class Update024
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.AbsenceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AbsenceCode")
                        .HasColumnType("int");

                    b.Property<string>("AbsenceDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RepDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Absences", "dbo");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.CustomTypeWorkEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CustomTypeWorks");
                });

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

                    b.Property<Guid>("Guid1C")
                        .HasColumnType("uniqueidentifier");

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

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.ManualScudEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Input")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Output")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserAutorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("ManualScuds");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.PlanEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Begin")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CustomTypeWorkId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlanType")
                        .HasColumnType("int");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TypeOverWorkId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CustomTypeWorkId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TypeOverWorkId");

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

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.ScudInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Input")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Output")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("ScudInfos");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.StoreEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StoreId1C")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.StoreLimitEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Limit")
                        .HasColumnType("int");

                    b.Property<int>("Mouth")
                        .HasColumnType("int");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("StoreLimits");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.TypeOverWorkEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeOverWorks");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.TypeTimeZupEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumberCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TypeTimeZups");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.AbsenceEntity", b =>
                {
                    b.HasOne("Miratorg.TimeKeeper.DataAccess.Entities.EmployeeEntity", "Employee")
                        .WithMany("Absences")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
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

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.ManualScudEntity", b =>
                {
                    b.HasOne("Miratorg.TimeKeeper.DataAccess.Entities.EmployeeEntity", "Employee")
                        .WithMany("ManualScuds")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.PlanEntity", b =>
                {
                    b.HasOne("Miratorg.TimeKeeper.DataAccess.Entities.CustomTypeWorkEntity", "CustomTypeWork")
                        .WithMany()
                        .HasForeignKey("CustomTypeWorkId");

                    b.HasOne("Miratorg.TimeKeeper.DataAccess.Entities.EmployeeEntity", "Employee")
                        .WithMany("Plans")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Miratorg.TimeKeeper.DataAccess.Entities.TypeOverWorkEntity", "TypeOverWork")
                        .WithMany()
                        .HasForeignKey("TypeOverWorkId");

                    b.Navigation("CustomTypeWork");

                    b.Navigation("Employee");

                    b.Navigation("TypeOverWork");
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

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.ScudInfo", b =>
                {
                    b.HasOne("Miratorg.TimeKeeper.DataAccess.Entities.EmployeeEntity", "Employee")
                        .WithMany("ScudInfos")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.StoreLimitEntity", b =>
                {
                    b.HasOne("Miratorg.TimeKeeper.DataAccess.Entities.StoreEntity", "Store")
                        .WithMany("Limits")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.EmployeeEntity", b =>
                {
                    b.Navigation("Absences");

                    b.Navigation("ManualScuds");

                    b.Navigation("Plans");

                    b.Navigation("ScudInfos");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.ScheduleEntity", b =>
                {
                    b.Navigation("Dates");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Miratorg.TimeKeeper.DataAccess.Entities.StoreEntity", b =>
                {
                    b.Navigation("Limits");
                });
#pragma warning restore 612, 618
        }
    }
}
