﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeGrow.DAL;

#nullable disable

namespace WeGrow.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WeGrow.DAL.Entities.Grow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BlobLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BlobName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HistoryFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Schedule_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("System_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Timelaps")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Schedule_Id");

                    b.HasIndex("System_Id");

                    b.ToTable("Grow");
                });

            modelBuilder.Entity("WeGrow.DAL.Entities.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("BlobLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BlobName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Is_Public")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<int>("Subject")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("WeGrow.DAL.Entities.ModuleInstance", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("LastResponse")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastResponseItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Module_Id")
                        .HasColumnType("int");

                    b.Property<string>("System_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("User_Id")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Module_Id");

                    b.HasIndex("System_Id");

                    b.ToTable("ModuleInstance");
                });

            modelBuilder.Entity("WeGrow.DAL.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("User_Id")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("WeGrow.DAL.Entities.Receipt", b =>
                {
                    b.Property<int>("Order_Id")
                        .HasColumnType("int");

                    b.Property<int>("Module_Id")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<decimal>("Cache_Price")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("Order_Id", "Module_Id");

                    b.HasIndex("Module_Id");

                    b.ToTable("Receipt");
                });

            modelBuilder.Entity("WeGrow.DAL.Entities.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BlobLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BlobName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<bool>("Is_Public")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User_Id")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("WeGrow.DAL.Entities.SystemInstance", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Is_Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("User_Id")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("SystemInstance");
                });

            modelBuilder.Entity("WeGrow.DAL.Entities.Grow", b =>
                {
                    b.HasOne("WeGrow.DAL.Entities.Schedule", "Schedule")
                        .WithMany("Grows")
                        .HasForeignKey("Schedule_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WeGrow.DAL.Entities.SystemInstance", "System")
                        .WithMany("Grows")
                        .HasForeignKey("System_Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Schedule");

                    b.Navigation("System");
                });

            modelBuilder.Entity("WeGrow.DAL.Entities.ModuleInstance", b =>
                {
                    b.HasOne("WeGrow.DAL.Entities.Module", "Module")
                        .WithMany("ModuleInstances")
                        .HasForeignKey("Module_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WeGrow.DAL.Entities.SystemInstance", "System")
                        .WithMany("ModuleInstances")
                        .HasForeignKey("System_Id")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Module");

                    b.Navigation("System");
                });

            modelBuilder.Entity("WeGrow.DAL.Entities.Receipt", b =>
                {
                    b.HasOne("WeGrow.DAL.Entities.Module", "Module")
                        .WithMany("Receipts")
                        .HasForeignKey("Module_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("WeGrow.DAL.Entities.Order", "Order")
                        .WithMany("Receipts")
                        .HasForeignKey("Order_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("WeGrow.DAL.Entities.SystemInstance", b =>
                {
                    b.HasOne("WeGrow.DAL.Entities.Schedule", "Schedule")
                        .WithMany("Systems")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("WeGrow.DAL.Entities.Module", b =>
                {
                    b.Navigation("ModuleInstances");

                    b.Navigation("Receipts");
                });

            modelBuilder.Entity("WeGrow.DAL.Entities.Order", b =>
                {
                    b.Navigation("Receipts");
                });

            modelBuilder.Entity("WeGrow.DAL.Entities.Schedule", b =>
                {
                    b.Navigation("Grows");

                    b.Navigation("Systems");
                });

            modelBuilder.Entity("WeGrow.DAL.Entities.SystemInstance", b =>
                {
                    b.Navigation("Grows");

                    b.Navigation("ModuleInstances");
                });
#pragma warning restore 612, 618
        }
    }
}
