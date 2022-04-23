using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PlaceAgregator.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuildingTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Bytes = table.Column<byte[]>(type: "bytea", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    FileExtension = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prohibitions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prohibitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    FamilyName = table.Column<string>(type: "text", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    FamilyName = table.Column<string>(type: "text", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    FamilyName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    BaseRate = table.Column<decimal>(type: "numeric", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Adress = table.Column<string>(type: "text", nullable: false),
                    MailIndex = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Area = table.Column<float>(type: "real", nullable: false),
                    CellingHeight = table.Column<float>(type: "real", nullable: false),
                    WaterType = table.Column<int>(type: "integer", nullable: true),
                    SocketsQuantity = table.Column<int>(type: "integer", nullable: false),
                    MaleToiletsQuantity = table.Column<int>(type: "integer", nullable: false),
                    FemaleToiletsQuantity = table.Column<int>(type: "integer", nullable: false),
                    SharedToiletsQuantity = table.Column<int>(type: "integer", nullable: false),
                    BuildingTypeId = table.Column<long>(type: "bigint", nullable: false),
                    ParkingType = table.Column<int>(type: "integer", nullable: false),
                    ParkingSpace = table.Column<int>(type: "integer", nullable: false),
                    Floor = table.Column<int>(type: "integer", nullable: false),
                    FloorsQuantity = table.Column<int>(type: "integer", nullable: false),
                    HasElevator = table.Column<bool>(type: "boolean", nullable: false),
                    HasFreightElevator = table.Column<bool>(type: "boolean", nullable: false),
                    HasDisabledEntrance = table.Column<bool>(type: "boolean", nullable: false),
                    BookingHorizonInDays = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Places_BuildingTypes_BuildingTypeId",
                        column: x => x.BuildingTypeId,
                        principalTable: "BuildingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Places_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PlaceId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EnrollDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GuestsQuantity = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingRequests_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    PlaceId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventTypePlace",
                columns: table => new
                {
                    AvailableEventsId = table.Column<long>(type: "bigint", nullable: false),
                    PlacesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypePlace", x => new { x.AvailableEventsId, x.PlacesId });
                    table.ForeignKey(
                        name: "FK_EventTypePlace_EventTypes_AvailableEventsId",
                        column: x => x.AvailableEventsId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTypePlace_Places_PlacesId",
                        column: x => x.PlacesId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionPlace",
                columns: table => new
                {
                    PermissionsId = table.Column<long>(type: "bigint", nullable: false),
                    PlacesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionPlace", x => new { x.PermissionsId, x.PlacesId });
                    table.ForeignKey(
                        name: "FK_PermissionPlace_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionPlace_Places_PlacesId",
                        column: x => x.PlacesId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlacePhotos",
                columns: table => new
                {
                    PlaceId = table.Column<long>(type: "bigint", nullable: false),
                    PhotoId = table.Column<long>(type: "bigint", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacePhotos", x => new { x.PlaceId, x.PhotoId });
                    table.ForeignKey(
                        name: "FK_PlacePhotos_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlacePhotos_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaceProhibition",
                columns: table => new
                {
                    PlacesId = table.Column<long>(type: "bigint", nullable: false),
                    ProhibitionsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceProhibition", x => new { x.PlacesId, x.ProhibitionsId });
                    table.ForeignKey(
                        name: "FK_PlaceProhibition_Places_PlacesId",
                        column: x => x.PlacesId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceProhibition_Prohibitions_ProhibitionsId",
                        column: x => x.ProhibitionsId,
                        principalTable: "Prohibitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaceRule",
                columns: table => new
                {
                    PlacesId = table.Column<long>(type: "bigint", nullable: false),
                    RulesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceRule", x => new { x.PlacesId, x.RulesId });
                    table.ForeignKey(
                        name: "FK_PlaceRule_Places_PlacesId",
                        column: x => x.PlacesId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceRule_Rules_RulesId",
                        column: x => x.RulesId,
                        principalTable: "Rules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlaceId = table.Column<long>(type: "bigint", nullable: false),
                    TimeInterval_StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeInterval_EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeInterval_StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeInterval_EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeInterval_Monday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Tuesday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Wednesday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Thusday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Friday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Saturday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Sunday = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rates_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlaceId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    IsCountable = table.Column<bool>(type: "boolean", nullable: false),
                    MaxCount = table.Column<int>(type: "integer", nullable: true),
                    Per = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceItems_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Charges",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlaceId = table.Column<long>(type: "bigint", nullable: false),
                    RateId = table.Column<long>(type: "bigint", nullable: true),
                    TimeInterval_StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeInterval_EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeInterval_StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeInterval_EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeInterval_Monday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Tuesday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Wednesday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Thusday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Friday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Saturday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Sunday = table.Column<bool>(type: "boolean", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Per = table.Column<int>(type: "integer", nullable: false),
                    FromHoursQuantity = table.Column<int>(type: "integer", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charges_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Charges_Rates_RateId",
                        column: x => x.RateId,
                        principalTable: "Rates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlaceId = table.Column<long>(type: "bigint", nullable: false),
                    RateId = table.Column<long>(type: "bigint", nullable: true),
                    TimeInterval_StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeInterval_EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeInterval_StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeInterval_EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeInterval_Monday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Tuesday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Wednesday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Thusday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Friday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Saturday = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInterval_Sunday = table.Column<bool>(type: "boolean", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Per = table.Column<int>(type: "integer", nullable: false),
                    FromGuestQuantity = table.Column<int>(type: "integer", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discounts_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Discounts_Rates_RateId",
                        column: x => x.RateId,
                        principalTable: "Rates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookingRequestServiceItems",
                columns: table => new
                {
                    BookingRequestId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceItemId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRequestServiceItems", x => new { x.ServiceItemId, x.BookingRequestId });
                    table.ForeignKey(
                        name: "FK_BookingRequestServiceItems_BookingRequests_BookingRequestId",
                        column: x => x.BookingRequestId,
                        principalTable: "BookingRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingRequestServiceItems_ServiceItems_ServiceItemId",
                        column: x => x.ServiceItemId,
                        principalTable: "ServiceItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_AccountId",
                table: "Admins",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingRequests_PlaceId",
                table: "BookingRequests",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRequests_UserId",
                table: "BookingRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRequestServiceItems_BookingRequestId",
                table: "BookingRequestServiceItems",
                column: "BookingRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_PlaceId",
                table: "Charges",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_RateId",
                table: "Charges",
                column: "RateId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PlaceId",
                table: "Comments",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_PlaceId",
                table: "Discounts",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_RateId",
                table: "Discounts",
                column: "RateId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTypePlace_PlacesId",
                table: "EventTypePlace",
                column: "PlacesId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_AccountId",
                table: "Managers",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPlace_PlacesId",
                table: "PermissionPlace",
                column: "PlacesId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacePhotos_PhotoId",
                table: "PlacePhotos",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceProhibition_ProhibitionsId",
                table: "PlaceProhibition",
                column: "ProhibitionsId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceRule_RulesId",
                table: "PlaceRule",
                column: "RulesId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_BuildingTypeId",
                table: "Places",
                column: "BuildingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_UserId",
                table: "Places",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_PlaceId",
                table: "Rates",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_PlaceId",
                table: "ServiceItems",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountId",
                table: "Users",
                column: "AccountId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "BookingRequestServiceItems");

            migrationBuilder.DropTable(
                name: "Charges");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "EventTypePlace");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "PermissionPlace");

            migrationBuilder.DropTable(
                name: "PlacePhotos");

            migrationBuilder.DropTable(
                name: "PlaceProhibition");

            migrationBuilder.DropTable(
                name: "PlaceRule");

            migrationBuilder.DropTable(
                name: "BookingRequests");

            migrationBuilder.DropTable(
                name: "ServiceItems");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Prohibitions");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "BuildingTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
