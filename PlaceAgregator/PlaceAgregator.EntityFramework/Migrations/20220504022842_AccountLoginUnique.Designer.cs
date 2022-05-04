﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PlaceAgregator.EntityFramework;

#nullable disable

namespace PlaceAgregator.EntityFramework.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220504022842_AccountLoginUnique")]
    partial class AccountLoginUnique
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EventTypePlace", b =>
                {
                    b.Property<int>("AvailableEventsId")
                        .HasColumnType("integer");

                    b.Property<int>("PlacesId")
                        .HasColumnType("integer");

                    b.HasKey("AvailableEventsId", "PlacesId");

                    b.HasIndex("PlacesId");

                    b.ToTable("EventTypePlace");
                });

            modelBuilder.Entity("PermissionPlace", b =>
                {
                    b.Property<int>("PermissionsId")
                        .HasColumnType("integer");

                    b.Property<int>("PlacesId")
                        .HasColumnType("integer");

                    b.HasKey("PermissionsId", "PlacesId");

                    b.HasIndex("PlacesId");

                    b.ToTable("PermissionPlace");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.BookingRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("EnrollDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GuestsQuantity")
                        .HasColumnType("integer");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.HasIndex("UserId");

                    b.ToTable("BookingRequests");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.BookingRequestServiceItem", b =>
                {
                    b.Property<int>("ServiceItemId")
                        .HasColumnType("integer");

                    b.Property<int>("BookingRequestId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("ServiceItemId", "BookingRequestId");

                    b.HasIndex("BookingRequestId");

                    b.ToTable("BookingRequestServiceItems");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.BuildingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BuildingTypes");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Charge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int?>("FromHoursQuantity")
                        .HasColumnType("integer");

                    b.Property<int>("Per")
                        .HasColumnType("integer");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<int?>("RateId")
                        .HasColumnType("integer");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.HasIndex("RateId");

                    b.ToTable("Charges");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int?>("FromGuestQuantity")
                        .HasColumnType("integer");

                    b.Property<int>("Per")
                        .HasColumnType("integer");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<int?>("RateId")
                        .HasColumnType("integer");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.HasIndex("RateId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.EventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EventTypes");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Bytes")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("AdministratorRule")
                        .HasColumnType("integer");

                    b.Property<float?>("Area")
                        .HasColumnType("real");

                    b.Property<decimal?>("BaseRate")
                        .HasColumnType("numeric");

                    b.Property<int>("BookingHorizonInDays")
                        .HasColumnType("integer");

                    b.Property<int?>("BuildingTypeId")
                        .HasColumnType("integer");

                    b.Property<int?>("Capacity")
                        .HasColumnType("integer");

                    b.Property<float?>("CellingHeight")
                        .HasColumnType("real");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("FemaleToiletsQuantity")
                        .HasColumnType("integer");

                    b.Property<int?>("Floor")
                        .HasColumnType("integer");

                    b.Property<int?>("FloorsQuantity")
                        .HasColumnType("integer");

                    b.Property<bool>("HasDisabledEntrance")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasElevator")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasFreightElevator")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("MailIndex")
                        .HasColumnType("text");

                    b.Property<int>("MaleToiletsQuantity")
                        .HasColumnType("integer");

                    b.Property<int?>("ParkingSpace")
                        .HasColumnType("integer");

                    b.Property<int?>("ParkingType")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("bytea");

                    b.Property<float?>("Rating")
                        .HasColumnType("real");

                    b.Property<int>("SharedToiletsQuantity")
                        .HasColumnType("integer");

                    b.Property<int?>("SmokingRule")
                        .HasColumnType("integer");

                    b.Property<int>("SocketsQuantity")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int?>("WaterType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BuildingTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.PlacePhoto", b =>
                {
                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<int>("PhotoId")
                        .HasColumnType("integer");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.HasKey("PlaceId", "PhotoId");

                    b.HasIndex("PhotoId");

                    b.ToTable("PlacePhotos");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Prohibition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Prohibitions");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Rate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Rule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.ServiceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsCountable")
                        .HasColumnType("boolean");

                    b.Property<int?>("MaxCount")
                        .HasColumnType("integer");

                    b.Property<int>("Per")
                        .HasColumnType("integer");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("ServiceItems");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FamilyName")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PlaceProhibition", b =>
                {
                    b.Property<int>("PlacesId")
                        .HasColumnType("integer");

                    b.Property<int>("ProhibitionsId")
                        .HasColumnType("integer");

                    b.HasKey("PlacesId", "ProhibitionsId");

                    b.HasIndex("ProhibitionsId");

                    b.ToTable("PlaceProhibition");
                });

            modelBuilder.Entity("PlaceRule", b =>
                {
                    b.Property<int>("PlacesId")
                        .HasColumnType("integer");

                    b.Property<int>("RulesId")
                        .HasColumnType("integer");

                    b.HasKey("PlacesId", "RulesId");

                    b.HasIndex("RulesId");

                    b.ToTable("PlaceRule");
                });

            modelBuilder.Entity("EventTypePlace", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.EventType", null)
                        .WithMany()
                        .HasForeignKey("AvailableEventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Entities.Place", null)
                        .WithMany()
                        .HasForeignKey("PlacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PermissionPlace", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Entities.Place", null)
                        .WithMany()
                        .HasForeignKey("PlacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Admin", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.Account", "Account")
                        .WithOne("Admin")
                        .HasForeignKey("PlaceAgregator.Entities.Admin", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.BookingRequest", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.Place", "Place")
                        .WithMany("BookingRequests")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Entities.User", "User")
                        .WithMany("BookingRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.BookingRequestServiceItem", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.BookingRequest", "BookingRequest")
                        .WithMany("ServiceItems")
                        .HasForeignKey("BookingRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Entities.ServiceItem", "ServiceItem")
                        .WithMany()
                        .HasForeignKey("ServiceItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookingRequest");

                    b.Navigation("ServiceItem");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Charge", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.Place", "Place")
                        .WithMany("Charges")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Entities.Rate", "Rate")
                        .WithMany()
                        .HasForeignKey("RateId");

                    b.OwnsOne("PlaceAgregator.Entities.TimeInterval", "TimeInterval", b1 =>
                        {
                            b1.Property<int>("ChargeId")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime>("EndTime")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<bool>("Friday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Monday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Saturday")
                                .HasColumnType("boolean");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime>("StartTime")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<bool>("Sunday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Thusday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Tuesday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Wednesday")
                                .HasColumnType("boolean");

                            b1.HasKey("ChargeId");

                            b1.ToTable("Charges");

                            b1.WithOwner()
                                .HasForeignKey("ChargeId");
                        });

                    b.Navigation("Place");

                    b.Navigation("Rate");

                    b.Navigation("TimeInterval")
                        .IsRequired();
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Comment", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.Place", "Place")
                        .WithMany("Comments")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Discount", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.Place", "Place")
                        .WithMany("Discounts")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Entities.Rate", "Rate")
                        .WithMany()
                        .HasForeignKey("RateId");

                    b.OwnsOne("PlaceAgregator.Entities.TimeInterval", "TimeInterval", b1 =>
                        {
                            b1.Property<int>("DiscountId")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime>("EndTime")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<bool>("Friday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Monday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Saturday")
                                .HasColumnType("boolean");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime>("StartTime")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<bool>("Sunday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Thusday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Tuesday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Wednesday")
                                .HasColumnType("boolean");

                            b1.HasKey("DiscountId");

                            b1.ToTable("Discounts");

                            b1.WithOwner()
                                .HasForeignKey("DiscountId");
                        });

                    b.Navigation("Place");

                    b.Navigation("Rate");

                    b.Navigation("TimeInterval")
                        .IsRequired();
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Manager", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.Account", "Account")
                        .WithOne("Manager")
                        .HasForeignKey("PlaceAgregator.Entities.Manager", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Place", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.BuildingType", "BuildingType")
                        .WithMany()
                        .HasForeignKey("BuildingTypeId");

                    b.HasOne("PlaceAgregator.Entities.User", "User")
                        .WithMany("Places")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuildingType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.PlacePhoto", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.Photo", "Photo")
                        .WithMany("PlacePhotos")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Entities.Place", "Place")
                        .WithMany("Photos")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photo");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Rate", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.Place", "Place")
                        .WithMany("Rates")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("PlaceAgregator.Entities.TimeInterval", "TimeInterval", b1 =>
                        {
                            b1.Property<int>("RateId")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime>("EndTime")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<bool>("Friday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Monday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Saturday")
                                .HasColumnType("boolean");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime>("StartTime")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<bool>("Sunday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Thusday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Tuesday")
                                .HasColumnType("boolean");

                            b1.Property<bool>("Wednesday")
                                .HasColumnType("boolean");

                            b1.HasKey("RateId");

                            b1.ToTable("Rates");

                            b1.WithOwner()
                                .HasForeignKey("RateId");
                        });

                    b.Navigation("Place");

                    b.Navigation("TimeInterval")
                        .IsRequired();
                });

            modelBuilder.Entity("PlaceAgregator.Entities.ServiceItem", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.Place", "Place")
                        .WithMany("ServiceItems")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.User", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.Account", "Account")
                        .WithOne("User")
                        .HasForeignKey("PlaceAgregator.Entities.User", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("PlaceProhibition", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.Place", null)
                        .WithMany()
                        .HasForeignKey("PlacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Entities.Prohibition", null)
                        .WithMany()
                        .HasForeignKey("ProhibitionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlaceRule", b =>
                {
                    b.HasOne("PlaceAgregator.Entities.Place", null)
                        .WithMany()
                        .HasForeignKey("PlacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Entities.Rule", null)
                        .WithMany()
                        .HasForeignKey("RulesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Account", b =>
                {
                    b.Navigation("Admin");

                    b.Navigation("Manager");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.BookingRequest", b =>
                {
                    b.Navigation("ServiceItems");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Photo", b =>
                {
                    b.Navigation("PlacePhotos");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.Place", b =>
                {
                    b.Navigation("BookingRequests");

                    b.Navigation("Charges");

                    b.Navigation("Comments");

                    b.Navigation("Discounts");

                    b.Navigation("Photos");

                    b.Navigation("Rates");

                    b.Navigation("ServiceItems");
                });

            modelBuilder.Entity("PlaceAgregator.Entities.User", b =>
                {
                    b.Navigation("BookingRequests");

                    b.Navigation("Comments");

                    b.Navigation("Places");
                });
#pragma warning restore 612, 618
        }
    }
}
