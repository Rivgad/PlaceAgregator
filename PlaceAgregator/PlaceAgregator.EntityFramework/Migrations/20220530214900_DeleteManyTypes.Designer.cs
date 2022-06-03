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
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220530214900_DeleteManyTypes")]
    partial class DeleteManyTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EventTypePlace", b =>
                {
                    b.Property<int>("EventTypesId")
                        .HasColumnType("integer");

                    b.Property<int>("PlacesId")
                        .HasColumnType("integer");

                    b.HasKey("EventTypesId", "PlacesId");

                    b.HasIndex("PlacesId");

                    b.ToTable("EventTypePlace");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Patronimyc")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.BookingRequest", b =>
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

                    b.Property<int>("GuestsQuantity")
                        .HasColumnType("integer");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.HasIndex("UserId");

                    b.ToTable("BookingRequests");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.BookingRequestServiceItem", b =>
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

            modelBuilder.Entity("PlaceAgregator.Shared.Models.Charge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int>("FromGuestsQuantity")
                        .HasColumnType("integer");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Procents")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("Charges");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.Comment", b =>
                {
                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<bool>("IsBlocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastEditTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PlaceId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FromHoursQuantity")
                        .HasColumnType("integer");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Procents")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("Area")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("BaseRate")
                        .HasColumnType("numeric");

                    b.Property<int?>("BookingHorizonInDays")
                        .HasColumnType("integer");

                    b.Property<int?>("Capacity")
                        .HasColumnType("integer");

                    b.Property<decimal?>("CellingHeight")
                        .HasColumnType("numeric");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("FemaleToiletsQuantity")
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

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<int?>("MaleToiletsQuantity")
                        .HasColumnType("integer");

                    b.Property<int?>("ParkingSpace")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("bytea");

                    b.Property<decimal>("Rating")
                        .HasColumnType("numeric");

                    b.Property<int?>("SharedToiletsQuantity")
                        .HasColumnType("integer");

                    b.Property<int?>("SocketsQuantity")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.PlacePhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Value")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("PlacePhotos");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.ServiceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int>("MaxQuantity")
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

            modelBuilder.Entity("PlaceAgregator.Shared.Models.Types.EventType", b =>
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

            modelBuilder.Entity("PlaceAgregator.Shared.Models.Types.Prohibition", b =>
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

            modelBuilder.Entity("EventTypePlace", b =>
                {
                    b.HasOne("PlaceAgregator.Shared.Models.Types.EventType", null)
                        .WithMany()
                        .HasForeignKey("EventTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Shared.Models.Place", null)
                        .WithMany()
                        .HasForeignKey("PlacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PlaceAgregator.Shared.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PlaceAgregator.Shared.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Shared.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PlaceAgregator.Shared.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.BookingRequest", b =>
                {
                    b.HasOne("PlaceAgregator.Shared.Models.Place", "Place")
                        .WithMany("BookingRequests")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Shared.Models.AppUser", "User")
                        .WithMany("BookingRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.BookingRequestServiceItem", b =>
                {
                    b.HasOne("PlaceAgregator.Shared.Models.BookingRequest", "BookingRequest")
                        .WithMany("ServiceItems")
                        .HasForeignKey("BookingRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Shared.Models.ServiceItem", "ServiceItem")
                        .WithMany("BookingRequestServiceItems")
                        .HasForeignKey("ServiceItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookingRequest");

                    b.Navigation("ServiceItem");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.Charge", b =>
                {
                    b.HasOne("PlaceAgregator.Shared.Models.Place", "Place")
                        .WithMany("Charges")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.Comment", b =>
                {
                    b.HasOne("PlaceAgregator.Shared.Models.Place", "Place")
                        .WithMany("Comments")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Shared.Models.AppUser", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.Discount", b =>
                {
                    b.HasOne("PlaceAgregator.Shared.Models.Place", "Place")
                        .WithMany("Discounts")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.Place", b =>
                {
                    b.HasOne("PlaceAgregator.Shared.Models.AppUser", "User")
                        .WithMany("Places")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("PlaceAgregator.Shared.Models.Shedule", "Shedule", b1 =>
                        {
                            b1.Property<int>("PlaceId")
                                .HasColumnType("integer");

                            b1.Property<bool?>("Friday")
                                .IsRequired()
                                .HasColumnType("boolean");

                            b1.Property<bool?>("Monday")
                                .IsRequired()
                                .HasColumnType("boolean");

                            b1.Property<bool?>("Saturday")
                                .IsRequired()
                                .HasColumnType("boolean");

                            b1.Property<bool?>("Sunday")
                                .IsRequired()
                                .HasColumnType("boolean");

                            b1.Property<bool?>("Thuesday")
                                .IsRequired()
                                .HasColumnType("boolean");

                            b1.Property<bool?>("Thursday")
                                .IsRequired()
                                .HasColumnType("boolean");

                            b1.Property<bool?>("Wednesday")
                                .IsRequired()
                                .HasColumnType("boolean");

                            b1.HasKey("PlaceId");

                            b1.ToTable("Places");

                            b1.WithOwner()
                                .HasForeignKey("PlaceId");
                        });

                    b.Navigation("Shedule");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.PlacePhoto", b =>
                {
                    b.HasOne("PlaceAgregator.Shared.Models.Place", "Place")
                        .WithMany("Photos")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.ServiceItem", b =>
                {
                    b.HasOne("PlaceAgregator.Shared.Models.Place", "Place")
                        .WithMany("ServiceItems")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });

            modelBuilder.Entity("PlaceProhibition", b =>
                {
                    b.HasOne("PlaceAgregator.Shared.Models.Place", null)
                        .WithMany()
                        .HasForeignKey("PlacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlaceAgregator.Shared.Models.Types.Prohibition", null)
                        .WithMany()
                        .HasForeignKey("ProhibitionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.AppUser", b =>
                {
                    b.Navigation("BookingRequests");

                    b.Navigation("Comments");

                    b.Navigation("Places");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.BookingRequest", b =>
                {
                    b.Navigation("ServiceItems");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.Place", b =>
                {
                    b.Navigation("BookingRequests");

                    b.Navigation("Charges");

                    b.Navigation("Comments");

                    b.Navigation("Discounts");

                    b.Navigation("Photos");

                    b.Navigation("ServiceItems");
                });

            modelBuilder.Entity("PlaceAgregator.Shared.Models.ServiceItem", b =>
                {
                    b.Navigation("BookingRequestServiceItems");
                });
#pragma warning restore 612, 618
        }
    }
}
