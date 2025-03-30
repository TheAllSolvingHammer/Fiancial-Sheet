﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProektTSPGlaven.Models;

#nullable disable

namespace ProektTSPGlaven.Migrations
{
    [DbContext(typeof(FinancesContext))]
    [Migration("20250330095645_AddTransactionsAndAccounts")]
    partial class AddTransactionsAndAccounts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProektTSPGlaven.Models.Account", b =>
                {
                    b.Property<int>("accountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("accountID"));

                    b.Property<decimal>("balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("createdAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.HasKey("accountID");

                    b.HasIndex("userID");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("ProektTSPGlaven.Models.TransactionEntity", b =>
                {
                    b.Property<int>("transactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("transactionID"));

                    b.Property<int>("accountID")
                        .HasColumnType("int");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("createdAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("transactionID");

                    b.HasIndex("accountID");

                    b.ToTable("TransactionEntity");
                });

            modelBuilder.Entity("ProektTSPGlaven.Models.User", b =>
                {
                    b.Property<int>("userID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userID"));

                    b.Property<DateTime>("createdAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("hashedPassword")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("userID");

                    b.ToTable("users");
                });

            modelBuilder.Entity("ProektTSPGlaven.Models.Account", b =>
                {
                    b.HasOne("ProektTSPGlaven.Models.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("userID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProektTSPGlaven.Models.TransactionEntity", b =>
                {
                    b.HasOne("ProektTSPGlaven.Models.Account", "account")
                        .WithMany("Transactions")
                        .HasForeignKey("accountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("account");
                });

            modelBuilder.Entity("ProektTSPGlaven.Models.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("ProektTSPGlaven.Models.User", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
