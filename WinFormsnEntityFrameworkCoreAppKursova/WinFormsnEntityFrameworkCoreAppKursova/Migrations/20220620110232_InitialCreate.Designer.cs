﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WinFormsnEntityFrameworkCoreAppKursova.Models;

#nullable disable

namespace WinFormsnEntityFrameworkCoreAppKursova.Migrations
{
    [DbContext(typeof(ExcursionContext))]
    [Migration("20220620110232_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BusExcursion", b =>
                {
                    b.Property<int>("BusesId")
                        .HasColumnType("int");

                    b.Property<int>("ExcursionsId")
                        .HasColumnType("int");

                    b.HasKey("BusesId", "ExcursionsId");

                    b.HasIndex("ExcursionsId");

                    b.ToTable("BusExcursion");
                });

            modelBuilder.Entity("BusExcursionType", b =>
                {
                    b.Property<int>("BusesId")
                        .HasColumnType("int");

                    b.Property<int>("ExcursionTypesId")
                        .HasColumnType("int");

                    b.HasKey("BusesId", "ExcursionTypesId");

                    b.HasIndex("ExcursionTypesId");

                    b.ToTable("BusExcursionType");
                });

            modelBuilder.Entity("WinFormsnEntityFrameworkCoreAppKursova.Models.Bus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BDriverId")
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("FuelConsumption")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BDriverId")
                        .IsUnique();

                    b.ToTable("Buses");
                });

            modelBuilder.Entity("WinFormsnEntityFrameworkCoreAppKursova.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Deposit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("WinFormsnEntityFrameworkCoreAppKursova.Models.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("WinFormsnEntityFrameworkCoreAppKursova.Models.Excursion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfExcursions")
                        .HasColumnType("datetime2");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Distance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("ExcCustomerId")
                        .HasColumnType("int");

                    b.Property<int>("ExcTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfTourists")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ExcCustomerId");

                    b.HasIndex("ExcTypeId");

                    b.ToTable("Excursions");
                });

            modelBuilder.Entity("WinFormsnEntityFrameworkCoreAppKursova.Models.ExcursionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExcursionTypes");
                });

            modelBuilder.Entity("BusExcursion", b =>
                {
                    b.HasOne("WinFormsnEntityFrameworkCoreAppKursova.Models.Bus", null)
                        .WithMany()
                        .HasForeignKey("BusesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WinFormsnEntityFrameworkCoreAppKursova.Models.Excursion", null)
                        .WithMany()
                        .HasForeignKey("ExcursionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusExcursionType", b =>
                {
                    b.HasOne("WinFormsnEntityFrameworkCoreAppKursova.Models.Bus", null)
                        .WithMany()
                        .HasForeignKey("BusesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WinFormsnEntityFrameworkCoreAppKursova.Models.ExcursionType", null)
                        .WithMany()
                        .HasForeignKey("ExcursionTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WinFormsnEntityFrameworkCoreAppKursova.Models.Bus", b =>
                {
                    b.HasOne("WinFormsnEntityFrameworkCoreAppKursova.Models.Driver", "BDriver")
                        .WithOne("DBus")
                        .HasForeignKey("WinFormsnEntityFrameworkCoreAppKursova.Models.Bus", "BDriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BDriver");
                });

            modelBuilder.Entity("WinFormsnEntityFrameworkCoreAppKursova.Models.Excursion", b =>
                {
                    b.HasOne("WinFormsnEntityFrameworkCoreAppKursova.Models.Customer", "ExcCustomer")
                        .WithMany("Excursions")
                        .HasForeignKey("ExcCustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WinFormsnEntityFrameworkCoreAppKursova.Models.ExcursionType", "ExcType")
                        .WithMany("Excursions")
                        .HasForeignKey("ExcTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExcCustomer");

                    b.Navigation("ExcType");
                });

            modelBuilder.Entity("WinFormsnEntityFrameworkCoreAppKursova.Models.Customer", b =>
                {
                    b.Navigation("Excursions");
                });

            modelBuilder.Entity("WinFormsnEntityFrameworkCoreAppKursova.Models.Driver", b =>
                {
                    b.Navigation("DBus");
                });

            modelBuilder.Entity("WinFormsnEntityFrameworkCoreAppKursova.Models.ExcursionType", b =>
                {
                    b.Navigation("Excursions");
                });
#pragma warning restore 612, 618
        }
    }
}
