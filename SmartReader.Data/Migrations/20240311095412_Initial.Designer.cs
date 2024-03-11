﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartReader.Data;

#nullable disable

namespace SmartReader.Data.Migrations
{
    [DbContext(typeof(SmartReaderDbContext))]
    [Migration("20240311095412_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("SmartReader.Core.Domain.Config", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(95)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Configs");
                });

            modelBuilder.Entity("SmartReader.Core.Domain.Ct.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FacilityName")
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext");

                    b.Property<string>("NUPI")
                        .HasColumnType("longtext");

                    b.Property<string>("Occupation")
                        .HasColumnType("longtext");

                    b.Property<string>("PatientID")
                        .HasColumnType("longtext");

                    b.Property<int>("PatientPK")
                        .HasColumnType("int");

                    b.Property<string>("Pkv")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("RegistrationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SiteCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("SmartReader.Core.Domain.Ct.PatientPharmacy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DispenseDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Drug")
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Duration")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime?>("ExpectedReturn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PatientPK")
                        .HasColumnType("int");

                    b.Property<string>("PeriodTaken")
                        .HasColumnType("longtext");

                    b.Property<string>("ProphylaxisType")
                        .HasColumnType("longtext");

                    b.Property<string>("Provider")
                        .HasColumnType("longtext");

                    b.Property<string>("RegimenLine")
                        .HasColumnType("longtext");

                    b.Property<int>("SiteCode")
                        .HasColumnType("int");

                    b.Property<string>("TreatmentType")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("PatientPharmacies");
                });

            modelBuilder.Entity("SmartReader.Core.Domain.Ct.PatientVisit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("EDD")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal?>("Height")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime?>("LMP")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PatientPK")
                        .HasColumnType("int");

                    b.Property<string>("Pregnant")
                        .HasColumnType("longtext");

                    b.Property<string>("Service")
                        .HasColumnType("longtext");

                    b.Property<int>("SiteCode")
                        .HasColumnType("int");

                    b.Property<string>("VisitBy")
                        .HasColumnType("longtext");

                    b.Property<string>("VisitType")
                        .HasColumnType("longtext");

                    b.Property<string>("WABStage")
                        .HasColumnType("longtext");

                    b.Property<int?>("WHOStage")
                        .HasColumnType("int");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("PatientVisits");
                });

            modelBuilder.Entity("SmartReader.Core.Domain.Extract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EndPoint")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsPriority")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Sql")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Extracts");
                });

            modelBuilder.Entity("SmartReader.Core.Domain.Registry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Display")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Registries");
                });
#pragma warning restore 612, 618
        }
    }
}
