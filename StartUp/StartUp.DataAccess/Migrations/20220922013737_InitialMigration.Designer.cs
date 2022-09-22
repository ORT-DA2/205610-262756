﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StartUp.DataAccess;

#nullable disable

namespace StartUp.DataAccess.Migrations
{
    [DbContext(typeof(StartUpContext))]
    [Migration("20220922013737_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("StartUp.Domain.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InvitationId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InvitationId");

                    b.ToTable("Administrators");
                });

            modelBuilder.Entity("StartUp.Domain.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InvitationId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InvitationId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("StartUp.Domain.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Rol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("StartUp.Domain.InvoiceLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int?>("MedicineId")
                        .HasColumnType("int");

                    b.Property<int?>("SaleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.HasIndex("SaleId");

                    b.ToTable("InvoiceLines");
                });

            modelBuilder.Entity("StartUp.Domain.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Measure")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<bool>("Prescription")
                        .HasColumnType("bit");

                    b.Property<string>("Presentation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("StartUp.Domain.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InvitationId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InvitationId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("StartUp.Domain.Petition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("MedicineCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RequestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("Petitions");
                });

            modelBuilder.Entity("StartUp.Domain.Pharmacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pharmacies");
                });

            modelBuilder.Entity("StartUp.Domain.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Requestes");
                });

            modelBuilder.Entity("StartUp.Domain.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("StartUp.Domain.Symptom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("MedicineId")
                        .HasColumnType("int");

                    b.Property<string>("SymptomDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.ToTable("Symptom");
                });

            modelBuilder.Entity("StartUp.Domain.Administrator", b =>
                {
                    b.HasOne("StartUp.Domain.Invitation", "Invitation")
                        .WithMany()
                        .HasForeignKey("InvitationId");

                    b.Navigation("Invitation");
                });

            modelBuilder.Entity("StartUp.Domain.Employee", b =>
                {
                    b.HasOne("StartUp.Domain.Invitation", "Invitation")
                        .WithMany()
                        .HasForeignKey("InvitationId");

                    b.HasOne("StartUp.Domain.Pharmacy", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyId");

                    b.Navigation("Invitation");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("StartUp.Domain.InvoiceLine", b =>
                {
                    b.HasOne("StartUp.Domain.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineId");

                    b.HasOne("StartUp.Domain.Sale", null)
                        .WithMany("Medicines")
                        .HasForeignKey("SaleId");

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("StartUp.Domain.Medicine", b =>
                {
                    b.HasOne("StartUp.Domain.Pharmacy", null)
                        .WithMany("Stock")
                        .HasForeignKey("PharmacyId");
                });

            modelBuilder.Entity("StartUp.Domain.Owner", b =>
                {
                    b.HasOne("StartUp.Domain.Invitation", "Invitation")
                        .WithMany()
                        .HasForeignKey("InvitationId");

                    b.Navigation("Invitation");
                });

            modelBuilder.Entity("StartUp.Domain.Petition", b =>
                {
                    b.HasOne("StartUp.Domain.Request", null)
                        .WithMany("Petitions")
                        .HasForeignKey("RequestId");
                });

            modelBuilder.Entity("StartUp.Domain.Request", b =>
                {
                    b.HasOne("StartUp.Domain.Pharmacy", null)
                        .WithMany("Requests")
                        .HasForeignKey("PharmacyId");
                });

            modelBuilder.Entity("StartUp.Domain.Symptom", b =>
                {
                    b.HasOne("StartUp.Domain.Medicine", null)
                        .WithMany("Symptoms")
                        .HasForeignKey("MedicineId");
                });

            modelBuilder.Entity("StartUp.Domain.Medicine", b =>
                {
                    b.Navigation("Symptoms");
                });

            modelBuilder.Entity("StartUp.Domain.Pharmacy", b =>
                {
                    b.Navigation("Requests");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("StartUp.Domain.Request", b =>
                {
                    b.Navigation("Petitions");
                });

            modelBuilder.Entity("StartUp.Domain.Sale", b =>
                {
                    b.Navigation("Medicines");
                });
#pragma warning restore 612, 618
        }
    }
}
