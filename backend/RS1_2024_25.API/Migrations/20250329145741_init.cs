using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RS1_2024_25.API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    AmenityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmenityText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.AmenityID);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    GenderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.GenderID);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    RuleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuleText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.RuleID);
                });

            migrationBuilder.CreateTable(
                name: "Toiletries",
                columns: table => new
                {
                    ToiletryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toiletries", x => x.ToiletryID);
                });

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Administrator_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyAuthenticationTokens",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyAuthenticationTokens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MyAuthenticationTokens_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "TwoFactorAuths",
                columns: table => new
                {
                    TwoFactorAuthId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    AuthTokenHash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwoFactorAuths", x => x.TwoFactorAuthId);
                    table.ForeignKey(
                        name: "FK_TwoFactorAuths_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenderID = table.Column<int>(type: "int", nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Owner_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Owner_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Owner_Gender_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Gender",
                        principalColumn: "GenderID");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenderID = table.Column<int>(type: "int", nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_User_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_User_Gender_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Gender",
                        principalColumn: "GenderID");
                });

            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    ApartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerNight = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    AccountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.ApartmentId);
                    table.ForeignKey(
                        name: "FK_Apartments_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Apartments_Owner_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Owner",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "OwnerImages",
                columns: table => new
                {
                    OwnerImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    ImageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerImages", x => x.OwnerImageID);
                    table.ForeignKey(
                        name: "FK_OwnerImages_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ImageID");
                    table.ForeignKey(
                        name: "FK_OwnerImages_Owner_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Owner",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "OwnerReviews",
                columns: table => new
                {
                    OwnerReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerReviews", x => x.OwnerReviewID);
                    table.ForeignKey(
                        name: "FK_OwnerReviews_Owner_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Owner",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK_OwnerReviews_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    UserImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    ImageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.UserImageID);
                    table.ForeignKey(
                        name: "FK_UserImages_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ImageID");
                    table.ForeignKey(
                        name: "FK_UserImages_User_AccountID",
                        column: x => x.AccountID,
                        principalTable: "User",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "ApartmentAmenities",
                columns: table => new
                {
                    ApartmentAmenityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    AmenityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentAmenities", x => x.ApartmentAmenityID);
                    table.ForeignKey(
                        name: "FK_ApartmentAmenities_Amenities_AmenityID",
                        column: x => x.AmenityID,
                        principalTable: "Amenities",
                        principalColumn: "AmenityID");
                    table.ForeignKey(
                        name: "FK_ApartmentAmenities_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "ApartmentId");
                });

            migrationBuilder.CreateTable(
                name: "ApartmentImages",
                columns: table => new
                {
                    ApartmentImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    ImageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentImages", x => x.ApartmentImageID);
                    table.ForeignKey(
                        name: "FK_ApartmentImages_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "ApartmentId");
                    table.ForeignKey(
                        name: "FK_ApartmentImages_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ImageID");
                });

            migrationBuilder.CreateTable(
                name: "ApartmentRules",
                columns: table => new
                {
                    ApartmentRuleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    RuleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentRules", x => x.ApartmentRuleID);
                    table.ForeignKey(
                        name: "FK_ApartmentRules_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "ApartmentId");
                    table.ForeignKey(
                        name: "FK_ApartmentRules_Rules_RuleID",
                        column: x => x.RuleID,
                        principalTable: "Rules",
                        principalColumn: "RuleID");
                });

            migrationBuilder.CreateTable(
                name: "ApartmentToiletries",
                columns: table => new
                {
                    ApartmentToiletryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    ToiletryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentToiletries", x => x.ApartmentToiletryID);
                    table.ForeignKey(
                        name: "FK_ApartmentToiletries_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "ApartmentId");
                    table.ForeignKey(
                        name: "FK_ApartmentToiletries_Toiletries_ToiletryID",
                        column: x => x.ToiletryID,
                        principalTable: "Toiletries",
                        principalColumn: "ToiletryID");
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    FavoriteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    UserAccountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.FavoriteID);
                    table.ForeignKey(
                        name: "FK_Favorites_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK_Favorites_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "ApartmentId");
                    table.ForeignKey(
                        name: "FK_Favorites_User_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "User",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserAccountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservations_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK_Reservations_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "ApartmentId");
                    table.ForeignKey(
                        name: "FK_Reservations_User_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "User",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAccountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Reviews_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK_Reviews_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "ApartmentId");
                    table.ForeignKey(
                        name: "FK_Reviews_User_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "User",
                        principalColumn: "AccountID");
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountID", "Email", "FirstName", "LastName", "Password", "ResetToken", "ResetTokenExpiry", "Username" },
                values: new object[,]
                {
                    { 1, "admin@example.com", "Admin", "User", "AdminPass", null, null, "admin" },
                    { 2, "superadmin@example.com", "Super", "Admin", "SuperPass", null, null, "superadmin" },
                    { 3, "izelrepuh@example.com", "Izel", "Repuh", "SuperPass", null, null, "izelrepuh" },
                    { 4, "amaromer@example.com", "Amar", "Omer", "SuperAmar", null, null, "amaromer" },
                    { 5, "johndoe@example.com", "John", "Doe", "JohnPass", null, null, "johndoe" },
                    { 6, "janedoe@example.com", "Jane", "Doe", "JanePass", null, null, "janedoe" },
                    { 7, "xxxx@example.com", "Xkorisnik", "PKorisnik", "xxxxx", null, null, "xxxxx" },
                    { 8, "yyyy@example.com", "YYKorisnik", "YYPrezime", "YYYXX", null, null, "yyyy" },
                    { 9, "izel@gmail.com", "Izel", "Repuh", "Izel", null, null, "Izel" },
                    { 10, "maida@gmail.com", "Maida", "Kovac", "Maida", null, null, "Maida" },
                    { 11, "owner@gmail.com", "Admin", "Admin", "Admin", null, null, "Admin" },
                    { 12, "izellapin@gmail.com", "Izel    ", "Repuh", "password", null, null, "izellapin" }
                });

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "AmenityID", "AmenityText" },
                values: new object[,]
                {
                    { 1, "Besplatan parking" },
                    { 2, "Klima uređaj" },
                    { 3, "Veš mašina" },
                    { 4, "Pogled s terase" },
                    { 5, "Bazen" },
                    { 6, "Sauna" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Bosna i Hercegovina" },
                    { 2, "Hrvatska" },
                    { 3, "Srbija" },
                    { 4, "Makedonija" },
                    { 5, "Bugarska" },
                    { 6, "Crna Gora" },
                    { 7, "Albanija" },
                    { 8, "Grčka" }
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "GenderID", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageID", "ImagePath" },
                values: new object[,]
                {
                    { 1, "/images/image2.jpg" },
                    { 2, "/images/room1.jpg" },
                    { 3, "/images/toilet2.jpg" },
                    { 4, "/images/room1.jpg" },
                    { 5, "/images/image2.jpg" },
                    { 6, "/images/male.png" },
                    { 7, "/images/female.png" }
                });

            migrationBuilder.InsertData(
                table: "Rules",
                columns: new[] { "RuleID", "RuleText" },
                values: new object[,]
                {
                    { 1, "Zabranjeno pusenje" },
                    { 2, "Zabranjene zabave" },
                    { 3, "Dozvoljeni ljubimci" },
                    { 4, "Zabranjeno prekoracenje kapaciteta osoba" },
                    { 5, "Zabranjeno NESTO" }
                });

            migrationBuilder.InsertData(
                table: "Toiletries",
                columns: new[] { "ToiletryID", "Name" },
                values: new object[,]
                {
                    { 1, "Sapun" },
                    { 2, "Šampon" },
                    { 3, "Regenerator" },
                    { 4, "Fen" },
                    { 5, "Peškiri" }
                });

            migrationBuilder.InsertData(
                table: "Administrator",
                columns: new[] { "AccountID", "Image" },
                values: new object[,]
                {
                    { 1, null },
                    { 2, null },
                    { 3, null },
                    { 4, null }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Sarajevo" },
                    { 2, 1, "Mostar" },
                    { 3, 1, "Tuzla" },
                    { 4, 2, "Zagreb" },
                    { 5, 2, "Rijeka" },
                    { 6, 2, "Pula" },
                    { 7, 3, "Beograd" },
                    { 8, 3, "Novi Sad" },
                    { 9, 4, "Skopje" },
                    { 10, 4, "Ohrid" },
                    { 11, 5, "Sofia" },
                    { 12, 5, "Varna" },
                    { 13, 6, "Budva" },
                    { 14, 6, "Bar" },
                    { 15, 6, "Kotor" },
                    { 16, 7, "Tirana" },
                    { 17, 7, "Vlorë" },
                    { 18, 7, "Durrës" },
                    { 19, 8, "Atena" },
                    { 20, 8, "Thessaloniki" },
                    { 21, 8, "Patras" },
                    { 22, 8, "Corfu" }
                });

            migrationBuilder.InsertData(
                table: "Apartments",
                columns: new[] { "ApartmentId", "AccountID", "Adress", "CityId", "Description", "Name", "PricePerNight" },
                values: new object[,]
                {
                    { 1, null, "Maršala Tita 25, Sarajevo", 1, "A stylish and modern apartment in a prime location, perfect for travelers.", "Apartment Marshal", 50 },
                    { 2, null, "Ferhadija 12, Sarajevo", 1, "Charming apartment near Baščaršija with traditional decor.", "Old Town Retreat", 65 },
                    { 3, null, "Zmaja od Bosne 15, Sarajevo", 1, "Modern apartment with a beautiful city view.", "City Panorama Apartment", 80 },
                    { 4, null, "Rade Bitange 8, Mostar", 2, "A cozy and charming space with elegant decor.", "Apartment Charm", 70 },
                    { 5, null, "Maršala Tita 9, Mostar", 2, "Stunning apartment with a direct view of the Old Bridge.", "Bridge View Apartment", 90 },
                    { 6, null, "Gojka Vukovića 21, Mostar", 2, "Quiet and relaxing stay in the heart of Herzegovina.", "Herzegovina Hideaway", 75 },
                    { 7, null, "Slatina 10, Tuzla", 3, "Bright and spacious apartment with plenty of natural light.", "Apartment Sun", 50 },
                    { 8, null, "Trg Slobode 5, Tuzla", 3, "Apartment next to the famous Pannonian Lakes.", "Central Park Apartment", 60 },
                    { 9, null, "Aleja Alije Izetbegovića 16, Tuzla", 3, "Well-furnished and comfortable apartment for travelers.", "Modern City Stay", 55 },
                    { 10, null, "Ilica 42, Zagreb", 4, "A luxury apartment with high-end amenities.", "Apartment Exclusive", 150 },
                    { 11, null, "Savska 77, Zagreb", 4, "Penthouse with a skyline view of Zagreb.", "Zagreb Skyline", 180 },
                    { 12, null, "Radićeva 6, Zagreb", 4, "Charming old town apartment near historical landmarks.", "Upper Town Retreat", 140 },
                    { 13, null, "Obala 45, Rijeka", 5, "A luxurious apartment right by the Adriatic Sea.", "Seaside Escape", 120 },
                    { 14, null, "Korzo 10, Rijeka", 5, "Beautiful apartment overlooking the Rijeka harbor.", "Port View Apartment", 130 },
                    { 15, null, "Verdijeva 3, Rijeka", 5, "Stay in an elegant historic coastal apartment.", "Historic Coastal Home", 110 },
                    { 16, null, "Bjelašnica 12, Pula", 6, "A cozy retreat with a breathtaking mountain view.", "Mountain View Lodge", 80 },
                    { 17, null, "Flavijevska 1, Pula", 6, "Apartment next to the famous Pula Arena.", "Roman Amphitheater Stay", 95 },
                    { 18, null, "Verudela 20, Pula", 6, "Enjoy the Adriatic coast in this beachfront apartment.", "Pula Beachfront Getaway", 100 },
                    { 19, null, "Knez Mihailova 28, Beograd", 7, "Modern apartment in the city center.", "Belgrade Central", 110 },
                    { 20, null, "Bulevar Zorana Đinđića 35, Beograd", 7, "Relaxing stay by the Sava River.", "Riverside Apartment", 120 },
                    { 21, null, "Skadarska 9, Beograd", 7, "A unique apartment in the famous bohemian quarter.", "Luxury Skadarlija Stay", 130 },
                    { 22, null, "Bulevar Oslobođenja 22, Novi Sad", 8, "Modern loft in the heart of the city.", "Urban Loft", 95 },
                    { 23, null, "Podgradje 3, Novi Sad", 8, "Stay next to the historical Petrovaradin Fortress.", "Petrovaradin Fortress Stay", 85 },
                    { 24, null, "Kej žrtava racije 10, Novi Sad", 8, "Relax by the beautiful Danube River.", "River Danube Apartment", 90 },
                    { 25, null, "Stari Grad 17, Skopje", 9, "Stay in a beautifully restored historic building.", "Historic Downtown Apartment", 70 },
                    { 26, null, "Keј 13 Noemvri 5, Skopje", 9, "Perfect location next to the iconic Stone Bridge.", "Stone Bridge View", 75 },
                    { 27, null, "Macedonia Square 1, Skopje", 9, "Spacious and elegant suite in the city center.", "Luxury Skopje Suite", 100 },
                    { 31, null, "Keј Marshal Tito 11, Ohrid", 10, "Stunning views of Lake Ohrid.", "Ohrid Lake View", 80 },
                    { 32, null, "Car Samoil Street 5, Ohrid", 10, "Charming apartment in Ohrid's historic center.", "Old Town Retreat", 70 },
                    { 33, null, "Macedonia Street 15, Ohrid", 10, "High-end suite with a private balcony and lake view.", "Luxury Lakeside Suite", 120 },
                    { 34, null, "Vitosha Blvd 15, Sofia", 11, "Penthouse apartment in the heart of Sofia.", "Sofia Panorama", 110 },
                    { 35, null, "Aleksandar Stamboliyski Blvd 22, Sofia", 11, "Modern loft in downtown Sofia.", "Sofia Central Loft", 95 },
                    { 36, null, "Tsarigradsko Shose 30, Sofia", 11, "Premium business-class apartment.", "Luxury Business Suite", 130 },
                    { 37, null, "Golden Sands 5, Varna", 12, "Beachfront apartment in Varna.", "Varna Sea View", 100 },
                    { 38, null, "Slivnitsa Blvd 8, Varna", 12, "Luxury retreat near Varna Beach.", "Coastal Paradise", 110 },
                    { 39, null, "Opalchenska Street 12, Varna", 12, "Cozy apartment near Varna Cathedral.", "Historic Varna Stay", 90 },
                    { 40, null, "Slovenska Plaža 3, Budva", 13, "Luxury stay near the Budva beaches.", "Budva Riviera", 120 },
                    { 41, null, "Marina Street 10, Bar", 14, "Modern apartment overlooking the Adriatic Sea.", "Bar Harbor View", 95 },
                    { 42, null, "Kotor Bay Road 10, Kotor", 15, "Enjoy breathtaking bay views from this stunning apartment.", "Bay of Kotor Retreat", 115 },
                    { 43, null, "Blloku District 6, Tirana", 16, "Modern loft in downtown Tirana.", "Tirana Central Loft", 90 },
                    { 44, null, "Rruga Dhimiter Konomi 8, Vlorë", 17, "Beautiful beachfront apartment.", "Vlorë Beachfront Getaway", 110 },
                    { 45, null, "Port Road 7, Durrës", 18, "Luxury apartment with a stunning harbor view.", "Durrës Marina View", 120 },
                    { 46, null, "Nikis Avenue 8, Thessaloniki", 20, "Amazing view of the iconic White Tower.", "White Tower View", 125 },
                    { 47, null, "Skyline Tower, Vasilissis Sofias Ave 10, Athens", 19, "Exclusive penthouse with panoramic views.", "Luxury Penthouse", 250 },
                    { 48, null, "Agiou Nikolaou 10, Patras", 21, "Modern apartment in the heart of Patras.", "Patras City Center", 110 },
                    { 49, null, "Paleokastritsa Road 5, Corfu", 22, "Luxury stay with direct beach access.", "Corfu Beachfront Escape", 140 }
                });

            migrationBuilder.InsertData(
                table: "Owner",
                columns: new[] { "AccountID", "CityID", "CreatedAt", "GenderID", "Phone" },
                values: new object[,]
                {
                    { 9, 1, new DateTime(2025, 3, 29, 14, 57, 40, 910, DateTimeKind.Utc).AddTicks(7485), 2, "061-000-111" },
                    { 10, 2, new DateTime(2025, 3, 29, 14, 57, 40, 910, DateTimeKind.Utc).AddTicks(7608), 2, "061-000-222" },
                    { 11, 3, new DateTime(2025, 3, 29, 14, 57, 40, 910, DateTimeKind.Utc).AddTicks(7621), 1, "061-000-333" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "AccountID", "CityID", "CreatedAt", "GenderID", "Phone" },
                values: new object[,]
                {
                    { 5, 1, new DateTime(2025, 3, 29, 14, 57, 40, 910, DateTimeKind.Utc).AddTicks(6730), 1, "+38761000111" },
                    { 6, 2, new DateTime(2025, 3, 29, 14, 57, 40, 910, DateTimeKind.Utc).AddTicks(6894), 2, "+38761000222" },
                    { 7, 3, new DateTime(2025, 3, 29, 14, 57, 40, 910, DateTimeKind.Utc).AddTicks(6896), 1, "+38761000222" },
                    { 8, 4, new DateTime(2025, 3, 29, 14, 57, 40, 910, DateTimeKind.Utc).AddTicks(6897), 2, "+38761000222" },
                    { 12, 4, new DateTime(2025, 3, 29, 14, 57, 40, 910, DateTimeKind.Utc).AddTicks(6899), 2, "+38761000222" }
                });

            migrationBuilder.InsertData(
                table: "ApartmentAmenities",
                columns: new[] { "ApartmentAmenityID", "AmenityID", "ApartmentId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "ApartmentImages",
                columns: new[] { "ApartmentImageID", "ApartmentId", "ImageID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 },
                    { 6, 6, 1 },
                    { 7, 7, 2 },
                    { 8, 8, 3 },
                    { 9, 9, 4 },
                    { 10, 10, 5 },
                    { 11, 11, 1 },
                    { 12, 12, 2 },
                    { 13, 13, 3 },
                    { 14, 14, 4 },
                    { 15, 15, 5 },
                    { 16, 16, 1 },
                    { 17, 17, 2 },
                    { 18, 18, 3 },
                    { 19, 19, 4 },
                    { 20, 20, 5 },
                    { 21, 21, 1 },
                    { 22, 22, 2 },
                    { 23, 23, 3 },
                    { 24, 24, 4 },
                    { 25, 25, 5 },
                    { 26, 26, 1 },
                    { 27, 27, 2 },
                    { 28, 31, 3 },
                    { 29, 32, 4 },
                    { 30, 33, 5 },
                    { 31, 34, 1 },
                    { 32, 35, 2 },
                    { 33, 36, 3 },
                    { 34, 37, 4 },
                    { 35, 38, 5 },
                    { 36, 39, 1 },
                    { 37, 40, 2 },
                    { 38, 41, 3 },
                    { 39, 42, 4 },
                    { 40, 43, 5 },
                    { 41, 44, 1 },
                    { 42, 45, 2 },
                    { 43, 46, 3 },
                    { 44, 47, 4 },
                    { 45, 48, 5 },
                    { 46, 49, 1 }
                });

            migrationBuilder.InsertData(
                table: "ApartmentRules",
                columns: new[] { "ApartmentRuleID", "ApartmentId", "RuleID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 1, 4 },
                    { 5, 4, 5 }
                });

            migrationBuilder.InsertData(
                table: "ApartmentToiletries",
                columns: new[] { "ApartmentToiletryID", "ApartmentId", "ToiletryID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 4, 5 }
                });

            migrationBuilder.InsertData(
                table: "OwnerImages",
                columns: new[] { "OwnerImageID", "AccountID", "ImageID" },
                values: new object[,]
                {
                    { 1, 9, 6 },
                    { 2, 10, 6 },
                    { 3, 11, 7 }
                });

            migrationBuilder.InsertData(
                table: "UserImages",
                columns: new[] { "UserImageID", "AccountID", "ImageID" },
                values: new object[,]
                {
                    { 1, 5, 7 },
                    { 2, 6, 7 },
                    { 3, 7, 6 },
                    { 4, 8, 6 },
                    { 5, 12, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentAmenities_AmenityID",
                table: "ApartmentAmenities",
                column: "AmenityID");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentAmenities_ApartmentId",
                table: "ApartmentAmenities",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentImages_ApartmentId",
                table: "ApartmentImages",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentImages_ImageID",
                table: "ApartmentImages",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentRules_ApartmentId",
                table: "ApartmentRules",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentRules_RuleID",
                table: "ApartmentRules",
                column: "RuleID");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_AccountID",
                table: "Apartments",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_CityId",
                table: "Apartments",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentToiletries_ApartmentId",
                table: "ApartmentToiletries",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentToiletries_ToiletryID",
                table: "ApartmentToiletries",
                column: "ToiletryID");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_AccountID",
                table: "Favorites",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ApartmentId",
                table: "Favorites",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserAccountID",
                table: "Favorites",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_MyAuthenticationTokens_AccountId",
                table: "MyAuthenticationTokens",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_CityID",
                table: "Owner",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_GenderID",
                table: "Owner",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerImages_AccountID",
                table: "OwnerImages",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerImages_ImageID",
                table: "OwnerImages",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerReviews_OwnerID",
                table: "OwnerReviews",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerReviews_UserID",
                table: "OwnerReviews",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AccountID",
                table: "Reservations",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ApartmentId",
                table: "Reservations",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserAccountID",
                table: "Reservations",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AccountID",
                table: "Reviews",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ApartmentId",
                table: "Reviews",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserAccountID",
                table: "Reviews",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TwoFactorAuths_AccountId",
                table: "TwoFactorAuths",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_CityID",
                table: "User",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_User_GenderID",
                table: "User",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_AccountID",
                table: "UserImages",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_ImageID",
                table: "UserImages",
                column: "ImageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "ApartmentAmenities");

            migrationBuilder.DropTable(
                name: "ApartmentImages");

            migrationBuilder.DropTable(
                name: "ApartmentRules");

            migrationBuilder.DropTable(
                name: "ApartmentToiletries");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "MyAuthenticationTokens");

            migrationBuilder.DropTable(
                name: "OwnerImages");

            migrationBuilder.DropTable(
                name: "OwnerReviews");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "TwoFactorAuths");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Toiletries");

            migrationBuilder.DropTable(
                name: "Apartments");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
