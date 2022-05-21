﻿// <auto-generated />
using System;
using AttendanceManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AttendanceManagement.Domain.Migrations
{
    [DbContext(typeof(AttendanceManagementDBContext))]
    [Migration("20220521122542_changeDataType_ClassModel")]
    partial class changeDataType_ClassModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AttendanceManagement.Domain.Models.Attendee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("CardID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("AttendanceManagement.Domain.Models.Class", b =>
                {
                    b.Property<int>("ClassID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassID"), 1L, 1);

                    b.Property<DateTime>("ClassDateEnd")
                        .HasColumnType("Date");

                    b.Property<DateTime>("ClassDateStart")
                        .HasColumnType("Date");

                    b.Property<TimeSpan>("ClassEndTime")
                        .HasColumnType("time");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("ClassStartTime")
                        .HasColumnType("time");

                    b.Property<string>("DaysOfWeek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClassID");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("AttendanceManagement.Domain.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventID"), 1L, 1);

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("Date");

                    b.Property<TimeSpan>("EventEndTime")
                        .HasColumnType("time");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("EventStartTime")
                        .HasColumnType("time");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("AttendeeClass", b =>
                {
                    b.Property<int>("AttendeesID")
                        .HasColumnType("int");

                    b.Property<int>("ClassesClassID")
                        .HasColumnType("int");

                    b.HasKey("AttendeesID", "ClassesClassID");

                    b.HasIndex("ClassesClassID");

                    b.ToTable("AttendeeClass");
                });

            modelBuilder.Entity("AttendeeEvent", b =>
                {
                    b.Property<int>("AttendeesID")
                        .HasColumnType("int");

                    b.Property<int>("EventsEventID")
                        .HasColumnType("int");

                    b.HasKey("AttendeesID", "EventsEventID");

                    b.HasIndex("EventsEventID");

                    b.ToTable("AttendeeEvent");
                });

            modelBuilder.Entity("AttendeeClass", b =>
                {
                    b.HasOne("AttendanceManagement.Domain.Models.Attendee", null)
                        .WithMany()
                        .HasForeignKey("AttendeesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AttendanceManagement.Domain.Models.Class", null)
                        .WithMany()
                        .HasForeignKey("ClassesClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AttendeeEvent", b =>
                {
                    b.HasOne("AttendanceManagement.Domain.Models.Attendee", null)
                        .WithMany()
                        .HasForeignKey("AttendeesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AttendanceManagement.Domain.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventsEventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
