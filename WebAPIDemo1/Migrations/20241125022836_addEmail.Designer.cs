﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebAPIDemo1.Data;

#nullable disable

namespace WebAPIDemo1.Migrations
{
    [DbContext(typeof(GameDataContext))]
    [Migration("20241125022836_addEmail")]
    partial class addEmail
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebAPIDemo1.Data.AdminCreate", b =>
                {
                    b.Property<int>("adminid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("adminid"));

                    b.Property<string>("confirmpassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("fullname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("mobilephone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("adminid");

                    b.ToTable("AdminCreates");
                });

            modelBuilder.Entity("WebAPIDemo1.Data.AdminLogin", b =>
                {
                    b.Property<int>("adminid")
                        .HasColumnType("integer");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("adminid");

                    b.ToTable("AdminLogins");
                });

            modelBuilder.Entity("WebAPIDemo1.Data.Cart", b =>
                {
                    b.Property<int>("cartid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("cartid"));

                    b.Property<DateTime>("addeddate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("customerid")
                        .HasColumnType("integer");

                    b.Property<int>("gameid")
                        .HasColumnType("integer");

                    b.Property<int>("quantity")
                        .HasColumnType("integer");

                    b.HasKey("cartid");

                    b.HasIndex("customerid");

                    b.HasIndex("gameid");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("WebAPIDemo1.Data.Checkout", b =>
                {
                    b.Property<int>("saleid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("saleid"));

                    b.Property<int>("customerid")
                        .HasColumnType("integer");

                    b.Property<string>("deliveryaddress1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("deliveryaddress2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("deliverycity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("deliverylandmark")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("deliverypincode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("discount")
                        .HasColumnType("numeric");

                    b.Property<string>("paymentnaration")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("saledate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("totalinvoiceamount")
                        .HasColumnType("numeric");

                    b.HasKey("saleid");

                    b.HasIndex("customerid");

                    b.ToTable("Checkouts");
                });

            modelBuilder.Entity("WebAPIDemo1.Data.Game", b =>
                {
                    b.Property<int>("gameid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("gameid"));

                    b.Property<int>("categoryid")
                        .HasColumnType("integer");

                    b.Property<string>("imageurl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<double>("price")
                        .HasColumnType("double precision");

                    b.Property<string>("summary")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("year")
                        .HasColumnType("integer");

                    b.HasKey("gameid");

                    b.HasIndex("categoryid");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("WebAPIDemo1.Data.GameCategory", b =>
                {
                    b.Property<int>("categoryid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("categoryid"));

                    b.Property<string>("categoryname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("categoryid");

                    b.ToTable("GameCategories");
                });

            modelBuilder.Entity("WebAPIDemo1.Data.Login", b =>
                {
                    b.Property<int>("customerid")
                        .HasColumnType("integer");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("customerid");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("WebAPIDemo1.Data.Register", b =>
                {
                    b.Property<int>("customerid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("customerid"));

                    b.Property<string>("confirmpassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("customerid");

                    b.ToTable("Registers");
                });

            modelBuilder.Entity("WebAPIDemo1.Data.AdminLogin", b =>
                {
                    b.HasOne("WebAPIDemo1.Data.AdminCreate", "AdminCreate")
                        .WithOne("AdminLogin")
                        .HasForeignKey("WebAPIDemo1.Data.AdminLogin", "adminid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdminCreate");
                });

            modelBuilder.Entity("WebAPIDemo1.Data.Cart", b =>
                {
                    b.HasOne("WebAPIDemo1.Data.Login", "Login")
                        .WithMany()
                        .HasForeignKey("customerid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPIDemo1.Data.Game", "Game")
                        .WithMany()
                        .HasForeignKey("gameid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Login");
                });

            modelBuilder.Entity("WebAPIDemo1.Data.Checkout", b =>
                {
                    b.HasOne("WebAPIDemo1.Data.Login", "Login")
                        .WithMany()
                        .HasForeignKey("customerid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Login");
                });

            modelBuilder.Entity("WebAPIDemo1.Data.Game", b =>
                {
                    b.HasOne("WebAPIDemo1.Data.GameCategory", "GameCategory")
                        .WithMany("Games")
                        .HasForeignKey("categoryid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameCategory");
                });

            modelBuilder.Entity("WebAPIDemo1.Data.Login", b =>
                {
                    b.HasOne("WebAPIDemo1.Data.Register", "Register")
                        .WithOne("Login")
                        .HasForeignKey("WebAPIDemo1.Data.Login", "customerid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Register");
                });

            modelBuilder.Entity("WebAPIDemo1.Data.AdminCreate", b =>
                {
                    b.Navigation("AdminLogin")
                        .IsRequired();
                });

            modelBuilder.Entity("WebAPIDemo1.Data.GameCategory", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("WebAPIDemo1.Data.Register", b =>
                {
                    b.Navigation("Login")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
