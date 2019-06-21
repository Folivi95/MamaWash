﻿// <auto-generated />
using System;
using MamaWash.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MamaWash.Migrations
{
    [DbContext(typeof(MamaWashContext))]
    partial class MamaWashContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MamaWash.Models.BankList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bank");

                    b.Property<string>("BankCode");

                    b.HasKey("ID");

                    b.ToTable("BankList");
                });

            modelBuilder.Entity("MamaWash.Models.Beneficiary", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName");

                    b.Property<int>("AccountNumber")
                        .HasMaxLength(10);

                    b.Property<int>("BankID");

                    b.Property<string>("RecipientCode");

                    b.HasKey("ID");

                    b.HasIndex("BankID");

                    b.ToTable("Beneficiary");
                });

            modelBuilder.Entity("MamaWash.Models.TransactionHistory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BeneficiariesID");

                    b.HasKey("ID");

                    b.HasIndex("BeneficiariesID");

                    b.ToTable("TransactionHistory");
                });

            modelBuilder.Entity("MamaWash.Models.Beneficiary", b =>
                {
                    b.HasOne("MamaWash.Models.BankList", "Bank")
                        .WithMany()
                        .HasForeignKey("BankID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MamaWash.Models.TransactionHistory", b =>
                {
                    b.HasOne("MamaWash.Models.Beneficiary")
                        .WithMany("Transactions")
                        .HasForeignKey("BeneficiariesID");
                });
#pragma warning restore 612, 618
        }
    }
}