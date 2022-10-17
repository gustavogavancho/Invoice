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
    [Migration("20221017002436_AddSenderInfo")]
    partial class AddSenderInfo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Invoice.Entities.Sender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
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

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SenderId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("SenderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderType")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("Id");

                    b.ToTable("Senders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cce03168-f901-4b23-ae9c-fc031d9dc888"),
                            Address = "PSJE. LIMATAMBO 121",
                            Department = "SAN MARTIN",
                            District = "TARAPOTO",
                            EstablishmentCode = "0000",
                            GeoCode = "220901",
                            Province = "SAN MARTIN",
                            SenderId = 20606022779m,
                            SenderName = "SWIFTLINE SAC",
                            SenderType = "6"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
