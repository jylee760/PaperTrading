﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaperTradingApi.Data;

#nullable disable

namespace PaperTradingApi.Migrations
{
    [DbContext(typeof(PersonDbContext))]
    [Migration("20240921153813_First")]
    partial class First
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PaperTradingApi.Models.StockDetails", b =>
                {
                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("StockTicker")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable((string)null);

                    b.ToView("StockDetails", (string)null);
                });

            modelBuilder.Entity("PaperTradingApi.Models.UserDetails", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("AllTimeMoney")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CurrentMoney")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserName");

                    b.ToTable("UserDetail");
                });

            modelBuilder.Entity("PaperTradingApi.Models.UserOrders", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("OrderType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("StockTicker")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserName", "Timestamp");

                    b.ToTable("UserOrder");
                });

            modelBuilder.Entity("PaperTradingApi.Models.UserOrders", b =>
                {
                    b.HasOne("PaperTradingApi.Models.UserDetails", "User")
                        .WithMany()
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
