﻿// <auto-generated />
using System;
using DataLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLibrary.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240326124226_RemovedSomeSeedData")]
    partial class RemovedSomeSeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ModelsLibrary.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<string>("QRCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SessionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.HasIndex("UserId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("ModelsLibrary.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductCount")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ProductPrice")
                        .HasColumnType("float");

                    b.Property<double>("ProductWeight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("orderItems");
                });

            modelBuilder.Entity("ModelsLibrary.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Barcode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PeriodEnd")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("PeriodEnd");

                    b.Property<DateTime>("PeriodStart")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("PeriodStart");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("StoreId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Barcode")
                        .IsUnique()
                        .HasFilter("[Barcode] IS NOT NULL");

                    b.HasIndex("StoreId");

                    b.ToTable("products", (string)null);

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                            {
                                ttb.UseHistoryTable("productsHistory");
                                ttb
                                    .HasPeriodStart("PeriodStart")
                                    .HasColumnName("PeriodStart");
                                ttb
                                    .HasPeriodEnd("PeriodEnd")
                                    .HasColumnName("PeriodEnd");
                            }));
                });

            modelBuilder.Entity("ModelsLibrary.Models.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.HasIndex("UserId");

                    b.ToTable("shoppingCart");
                });

            modelBuilder.Entity("ModelsLibrary.Models.ShoppingCartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("shoppingCartItems");
                });

            modelBuilder.Entity("ModelsLibrary.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("stores");
                });

            modelBuilder.Entity("ModelsLibrary.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ResetPasswordExpire")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TemporalPassword")
                        .HasColumnType("bit");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.Property<string>("VerificationCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("VerifyEmailExpire")
                        .HasColumnType("datetime2");

                    b.Property<int?>("WorkingInStoreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkingInStoreId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("ModelsLibrary.Models.Order", b =>
                {
                    b.HasOne("ModelsLibrary.Models.Store", "Store")
                        .WithMany("Orders")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ModelsLibrary.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelsLibrary.Models.OrderItem", b =>
                {
                    b.HasOne("ModelsLibrary.Models.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ModelsLibrary.Models.Product", b =>
                {
                    b.HasOne("ModelsLibrary.Models.Store", "Store")
                        .WithMany("Products")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK_Product_Store");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("ModelsLibrary.Models.ShoppingCart", b =>
                {
                    b.HasOne("ModelsLibrary.Models.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelsLibrary.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelsLibrary.Models.ShoppingCartItem", b =>
                {
                    b.HasOne("ModelsLibrary.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelsLibrary.Models.ShoppingCart", "ShoppingCart")
                        .WithMany("Items")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("ModelsLibrary.Models.Store", b =>
                {
                    b.HasOne("ModelsLibrary.Models.User", "User")
                        .WithMany("OwnedStores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelsLibrary.Models.User", b =>
                {
                    b.HasOne("ModelsLibrary.Models.Store", "WorkingInStore")
                        .WithMany("Sellers")
                        .HasForeignKey("WorkingInStoreId");

                    b.Navigation("WorkingInStore");
                });

            modelBuilder.Entity("ModelsLibrary.Models.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("ModelsLibrary.Models.ShoppingCart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("ModelsLibrary.Models.Store", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Products");

                    b.Navigation("Sellers");
                });

            modelBuilder.Entity("ModelsLibrary.Models.User", b =>
                {
                    b.Navigation("OwnedStores");
                });
#pragma warning restore 612, 618
        }
    }
}
