﻿// <auto-generated />
using System;
using CoinPurse.Data;
using Expendium.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoinPurse.Data.Mssql.Migrations
{
    [DbContext(typeof(ExpendiumDbContext))]
    [Migration("20231218054406_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AccountBudget", b =>
                {
                    b.Property<int>("AccountsAccountId")
                        .HasColumnType("int");

                    b.Property<int>("BudgetsBudgetId")
                        .HasColumnType("int");

                    b.HasKey("AccountsAccountId", "BudgetsBudgetId");

                    b.HasIndex("BudgetsBudgetId");

                    b.ToTable("AccountBudget");
                });

            modelBuilder.Entity("Expendium.Data.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Expendium.Data.Entities.Budget", b =>
                {
                    b.Property<int>("BudgetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BudgetId"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("BudgetId");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("Expendium.Data.Entities.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseId"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("BudgetId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryExpenseCategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

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
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseCategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("ExpenseCategoryId");

                    b.ToTable("ExpenseCategory");

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
                        .HasColumnType("nvarchar(35)");

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
