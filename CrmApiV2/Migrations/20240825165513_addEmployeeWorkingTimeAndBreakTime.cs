using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrmApiV2.Migrations
{
    /// <inheritdoc />
    public partial class addEmployeeWorkingTimeAndBreakTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15abfd00-0bb1-47dd-9c33-0083666a0b35");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2986227f-9bac-42dd-a34c-8fdc64c4d199");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3fa59375-11ae-4eca-b237-54995f4f2d0d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b124d77-3989-4341-8ee7-6f5778c5f699");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e5387c4-645d-4d76-993c-feeee0aac84f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9a5e372-7639-44e1-b4fb-aecccf645496");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c67d8cb6-6ecf-437b-a48a-52202250094f");

            migrationBuilder.CreateTable(
                name: "DailyUserSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalWorkingTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    TotalBreakTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyUserSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyUserSummaries_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTimeLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BreakTime = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTimeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTimeLogs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "277d90c7-fb50-474d-9105-efc9de138655", null, "Academic Writer", "ACADEMIC_WRITER" },
                    { "982a79c9-54a1-4cf4-a31a-7b0ed4ccb3ce", null, "Admin", "ADMIN" },
                    { "bca05d19-d084-4d00-9c40-47a46db0df4e", null, "Developer", "DEVELOPER" },
                    { "c63d32a7-47e0-4b4a-af98-d4c72223a7fd", null, "Super Admin", "SUPER_ADMIN" },
                    { "e9d46f3a-3a1f-4243-aebf-8e7f8a08595d", null, "Seo", "SEO" },
                    { "ebaad9b9-247a-4d23-b866-713d5dfc0b74", null, "Sales", "SALES" },
                    { "eec1a010-8bc8-488f-b451-7ad8bb503178", null, "Content Writer", "CONTENT_WRITER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyUserSummaries_ApplicationUserId",
                table: "DailyUserSummaries",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTimeLogs_ApplicationUserId",
                table: "UserTimeLogs",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyUserSummaries");

            migrationBuilder.DropTable(
                name: "UserTimeLogs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "277d90c7-fb50-474d-9105-efc9de138655");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "982a79c9-54a1-4cf4-a31a-7b0ed4ccb3ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bca05d19-d084-4d00-9c40-47a46db0df4e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c63d32a7-47e0-4b4a-af98-d4c72223a7fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9d46f3a-3a1f-4243-aebf-8e7f8a08595d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebaad9b9-247a-4d23-b866-713d5dfc0b74");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eec1a010-8bc8-488f-b451-7ad8bb503178");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15abfd00-0bb1-47dd-9c33-0083666a0b35", null, "Super Admin", "SUPER_ADMIN" },
                    { "2986227f-9bac-42dd-a34c-8fdc64c4d199", null, "Developer", "DEVELOPER" },
                    { "3fa59375-11ae-4eca-b237-54995f4f2d0d", null, "Seo", "SEO" },
                    { "7b124d77-3989-4341-8ee7-6f5778c5f699", null, "Admin", "ADMIN" },
                    { "8e5387c4-645d-4d76-993c-feeee0aac84f", null, "Academic Writer", "ACADEMIC_WRITER" },
                    { "a9a5e372-7639-44e1-b4fb-aecccf645496", null, "Sales", "SALES" },
                    { "c67d8cb6-6ecf-437b-a48a-52202250094f", null, "Content Writer", "CONTENT_WRITER" }
                });
        }
    }
}
