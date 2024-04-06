﻿// <auto-generated />
using System;
using Expendium.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Expendium.Data.Postgres.Migrations
{
    [DbContext(typeof(ExpendiumDbContext))]
    [Migration("20240311074658_TransactionsInitial")]
    partial class TransactionsInitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AccountBudget", b =>
                {
                    b.Property<int>("AccountsAccountId")
                        .HasColumnType("integer");

                    b.Property<int>("BudgetsBudgetId")
                        .HasColumnType("integer");

                    b.HasKey("AccountsAccountId", "BudgetsBudgetId");

                    b.HasIndex("BudgetsBudgetId");

                    b.ToTable("AccountBudget");
                });

            modelBuilder.Entity("Expendium.Data.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AccountId"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Expendium.Data.Entities.Budget", b =>
                {
                    b.Property<int>("BudgetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BudgetId"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("BudgetId");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("Expendium.Data.Entities.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ExpenseId"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<int>("BudgetId")
                        .HasColumnType("integer");

                    b.Property<int?>("CategoryExpenseCategoryId")
                        .HasColumnType("integer");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.Property<short>("PaymentFrequency")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)4)
                        .HasColumnName("PaymentFrequencyId");

                    b.HasKey("ExpenseId");

                    b.HasIndex("BudgetId");

                    b.HasIndex("CategoryExpenseCategoryId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PaymentFrequency");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Expendium.Data.Entities.ExpenseCategory", b =>
                {
                    b.Property<int>("ExpenseCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ExpenseCategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.HasKey("ExpenseCategoryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            ExpenseCategoryId = 1,
                            Name = "Housing"
                        },
                        new
                        {
                            ExpenseCategoryId = 2,
                            Name = "Utilities"
                        },
                        new
                        {
                            ExpenseCategoryId = 3,
                            Name = "Food"
                        },
                        new
                        {
                            ExpenseCategoryId = 4,
                            Name = "Savings & Investment"
                        },
                        new
                        {
                            ExpenseCategoryId = 5,
                            Name = "Entertainment"
                        },
                        new
                        {
                            ExpenseCategoryId = 6,
                            Name = "Miscellaneous"
                        },
                        new
                        {
                            ExpenseCategoryId = 7,
                            Name = "Transportation"
                        },
                        new
                        {
                            ExpenseCategoryId = 8,
                            Name = "Personal Services"
                        });
                });

            modelBuilder.Entity("Expendium.Data.Entities.PaymentFrequencyLookup", b =>
                {
                    b.Property<short>("PaymentFrequencyId")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("character varying(35)");

                    b.HasKey("PaymentFrequencyId");

                    b.ToTable("PaymentFrequencyTypes");

                    b.HasData(
                        new
                        {
                            PaymentFrequencyId = (short)1,
                            Name = "Daily"
                        },
                        new
                        {
                            PaymentFrequencyId = (short)2,
                            Name = "Weekly"
                        },
                        new
                        {
                            PaymentFrequencyId = (short)3,
                            Name = "BiWeekly"
                        },
                        new
                        {
                            PaymentFrequencyId = (short)4,
                            Name = "Monthly"
                        },
                        new
                        {
                            PaymentFrequencyId = (short)5,
                            Name = "Quarterly"
                        },
                        new
                        {
                            PaymentFrequencyId = (short)6,
                            Name = "SemiAnnual"
                        },
                        new
                        {
                            PaymentFrequencyId = (short)7,
                            Name = "Annually"
                        });
                });

            modelBuilder.Entity("Expendium.Data.Entities.Transaction", b =>
                {
                    b.Property<long>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("TransactionId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<string>("Category")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

                    b.Property<string>("SourceAccount")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)0);

                    b.Property<DateTimeOffset>("TransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TransactionSignature")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TransactionId");

                    b.HasIndex("Category");

                    b.HasIndex("SourceAccount");

                    b.HasIndex("Status");

                    b.HasIndex("TransactionSignature");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("AccountBudget", b =>
                {
                    b.HasOne("Expendium.Data.Entities.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Expendium.Data.Entities.Budget", null)
                        .WithMany()
                        .HasForeignKey("BudgetsBudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Expendium.Data.Entities.Expense", b =>
                {
                    b.HasOne("Expendium.Data.Entities.Budget", "Budget")
                        .WithMany("Expenses")
                        .HasForeignKey("BudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Expendium.Data.Entities.ExpenseCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryExpenseCategoryId");

                    b.HasOne("Expendium.Data.Entities.ExpenseCategory", null)
                        .WithMany("Expenses")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Expendium.Data.Entities.PaymentFrequencyLookup", null)
                        .WithMany()
                        .HasForeignKey("PaymentFrequency")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Budget");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Expendium.Data.Entities.Budget", b =>
                {
                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("Expendium.Data.Entities.ExpenseCategory", b =>
                {
                    b.Navigation("Expenses");
                });
#pragma warning restore 612, 618
        }
    }
}