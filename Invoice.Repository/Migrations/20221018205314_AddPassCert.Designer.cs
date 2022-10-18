﻿// <auto-generated />
using System;
using Invoice.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Invoice.Repository.Migrations
{
    [DbContext(typeof(InvoiceContext))]
    [Migration("20221018205314_AddPassCert")]
    partial class AddPassCert
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Invoice.Entities.Issuer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("BetaCertificate")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("BetaCertificatePasword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstablishmentCode")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("GeoCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<decimal>("IssuerId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("IssuerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssuerType")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<byte[]>("ProdCertificate")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ProdCertificatePasword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Issuers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c9567ea8-8ab4-4b72-b171-25a228386b9d"),
                            Address = "PSJE. LIMATAMBO 121",
                            Department = "SAN MARTIN",
                            District = "TARAPOTO",
                            EstablishmentCode = "0000",
                            GeoCode = "220901",
                            IssuerId = 20606022779m,
                            IssuerName = "SWIFTLINE SAC",
                            IssuerType = "6",
                            Province = "SAN MARTIN"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
