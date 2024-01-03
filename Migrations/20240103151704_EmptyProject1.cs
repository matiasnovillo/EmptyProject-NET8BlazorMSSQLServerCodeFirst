using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmptyProject.Migrations
{
    /// <inheritdoc />
    public partial class EmptyProject1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuFatherId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    URLPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconURLPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "RoleMenu",
                columns: table => new
                {
                    RoleMenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenu", x => x.RoleMenuId);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    TestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Boolean = table.Column<byte>(type: "tinyint", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Decimal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ForeignKeyDropdown = table.Column<int>(type: "int", nullable: false),
                    ForeignKeyOptions = table.Column<int>(type: "int", nullable: false),
                    Integer = table.Column<int>(type: "int", nullable: false),
                    Basic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(380)", maxLength: 380, nullable: false),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HexColour = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextArea = table.Column<string>(type: "text", maxLength: 2000, nullable: false),
                    TextEditor = table.Column<string>(type: "text", maxLength: 2000, nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.TestId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "MenuId", "Active", "IconURLPath", "MenuFatherId", "Name", "Order", "URLPath" },
                values: new object[,]
                {
                    { 1, (byte)1, "", 0, "BasicCore", 100, "" },
                    { 2, (byte)1, "", 1, "Failure", 0, "/BasicCore/Failure" },
                    { 3, (byte)1, "", 1, "Parameter", 0, "/BasicCore/Parameter" },
                    { 4, (byte)1, "", 0, "BasicCulture", 200, "" },
                    { 5, (byte)1, "", 4, "City", 0, "/BasicCulture/City" },
                    { 6, (byte)1, "", 4, "State", 0, "/BasicCulture/State" },
                    { 7, (byte)1, "", 4, "Country", 0, "/BasicCulture/Country" },
                    { 8, (byte)1, "", 4, "Planet", 0, "/BasicCulture/Planet" },
                    { 9, (byte)1, "", 4, "Sex", 0, "/BasicCulture/Sex" },
                    { 10, (byte)1, "", 0, "CMSCore", 300, "" },
                    { 11, (byte)1, "", 10, "User", 0, "/CMSCore/User" },
                    { 12, (byte)1, "", 10, "Role", 0, "/CMSCore/Role" },
                    { 13, (byte)1, "", 10, "Menu", 0, "/CMSCore/Menu" },
                    { 14, (byte)1, "", 10, "Permission", 0, "/CMSCore/Permission" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator" },
                    { 2, "Client" }
                });

            migrationBuilder.InsertData(
                table: "RoleMenu",
                columns: new[] { "RoleMenuId", "MenuId", "RoleId" },
                values: new object[,]
                {
                    { 1, 10, 1 },
                    { 2, 14, 1 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "Password", "RoleId" },
                values: new object[] { 1, "novillo.matias1@gmail.com", "Pq5FM4q7dDtlZBGcn0w8P0XjnEPDlTCcLUY5/bWVcuVJ4/kXRyHp62hPgry0R/ur+kEspHc+HK6XqqvA8OLXLw==", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "RoleMenu");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
