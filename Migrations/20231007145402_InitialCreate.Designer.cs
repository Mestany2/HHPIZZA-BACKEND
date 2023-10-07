﻿// <auto-generated />
using HHPIZZA_BACKEND;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HHPIZZA_BACKEND.Migrations
{
    [DbContext(typeof(HhpizzaDbContext))]
    [Migration("20231007145402_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HHPIZZA_BACKEND.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Cheese Pizza",
                            Price = 4.99m
                        },
                        new
                        {
                            Id = 2,
                            Name = "Wings",
                            Price = 8.99m
                        });
                });

            modelBuilder.Entity("HHPIZZA_BACKEND.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OrderType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PaymentId")
                        .HasColumnType("integer");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ReviewId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Tip")
                        .HasColumnType("numeric");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.HasIndex("ReviewId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "My@yahoo.com",
                            Name = "Customer One",
                            OrderType = "Phone",
                            PaymentId = 1,
                            Phone = "615-123-4567",
                            ReviewId = 1,
                            Status = "Open",
                            Tip = 5.45m,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Email = "Myfg@yahoo.com",
                            Name = "Customer Two",
                            OrderType = "Online",
                            PaymentId = 2,
                            Phone = "615-143-4667",
                            ReviewId = 2,
                            Status = "Closed",
                            Tip = 2.45m,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("HHPIZZA_BACKEND.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Payments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Credit Card"
                        },
                        new
                        {
                            Id = 2,
                            Type = "Cash"
                        });
                });

            modelBuilder.Entity("HHPIZZA_BACKEND.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Great",
                            Rating = 5
                        },
                        new
                        {
                            Id = 2,
                            Content = "The best",
                            Rating = 4
                        });
                });

            modelBuilder.Entity("HHPIZZA_BACKEND.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isStaff")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Jack Smith",
                            Uid = "4d56asd6",
                            isStaff = true
                        },
                        new
                        {
                            Id = 2,
                            Name = "Mike James",
                            Uid = "4d56afdgdfgsd6",
                            isStaff = false
                        });
                });

            modelBuilder.Entity("ItemOrder", b =>
                {
                    b.Property<int>("itemsId")
                        .HasColumnType("integer");

                    b.Property<int>("ordersId")
                        .HasColumnType("integer");

                    b.HasKey("itemsId", "ordersId");

                    b.HasIndex("ordersId");

                    b.ToTable("ItemOrder");
                });

            modelBuilder.Entity("HHPIZZA_BACKEND.Models.Order", b =>
                {
                    b.HasOne("HHPIZZA_BACKEND.Models.Payment", "payment")
                        .WithMany()
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HHPIZZA_BACKEND.Models.Review", "Review")
                        .WithOne("Order")
                        .HasForeignKey("HHPIZZA_BACKEND.Models.Order", "ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HHPIZZA_BACKEND.Models.User", "user")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");

                    b.Navigation("payment");

                    b.Navigation("user");
                });

            modelBuilder.Entity("ItemOrder", b =>
                {
                    b.HasOne("HHPIZZA_BACKEND.Models.Item", null)
                        .WithMany()
                        .HasForeignKey("itemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HHPIZZA_BACKEND.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("ordersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HHPIZZA_BACKEND.Models.Review", b =>
                {
                    b.Navigation("Order")
                        .IsRequired();
                });

            modelBuilder.Entity("HHPIZZA_BACKEND.Models.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
