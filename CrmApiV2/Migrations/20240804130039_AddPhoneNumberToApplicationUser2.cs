using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrmApiV2.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberToApplicationUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f292e30-6c75-42a7-a6c0-c2425a997171");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b256911-cb30-467c-a538-a8e4bfe76f3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f41236f-2472-458b-9854-f8f82260891e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6dc509c3-8075-45dc-a2a2-be307fd6e801");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a037b7cf-5679-4941-99ba-48c0e90204ff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4436c89-04b6-4d7c-98b9-1278caf2e5ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7681d30-a185-473c-90f9-e9ee51b787e1");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0817d8e8-fec7-4139-a4c5-97d049c7f3ad", null, "Super Admin", "SUPER_ADMIN" },
                    { "5cb744ef-e086-4a58-9670-8648e85af768", null, "Seo", "SEO" },
                    { "9562bf99-3ca2-4e87-a198-264eccbc9ddb", null, "Sales", "SALES" },
                    { "be1ae4e8-473b-4bd1-8f07-d5f162d6aaa7", null, "Content Writer", "CONTENT_WRITER" },
                    { "c9a78277-a64e-4738-8cd0-41eddfaa0114", null, "Admin", "ADMIN" },
                    { "dbfd9ab3-dcf6-4f32-9c00-fc0e29a33791", null, "Developer", "DEVELOPER" },
                    { "fff03597-25d1-48cd-824c-5f2acf2b30b0", null, "Academic Writer", "ACADEMIC_WRITER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0817d8e8-fec7-4139-a4c5-97d049c7f3ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5cb744ef-e086-4a58-9670-8648e85af768");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9562bf99-3ca2-4e87-a198-264eccbc9ddb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be1ae4e8-473b-4bd1-8f07-d5f162d6aaa7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9a78277-a64e-4738-8cd0-41eddfaa0114");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dbfd9ab3-dcf6-4f32-9c00-fc0e29a33791");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fff03597-25d1-48cd-824c-5f2acf2b30b0");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f292e30-6c75-42a7-a6c0-c2425a997171", null, "Developer", "DEVELOPER" },
                    { "4b256911-cb30-467c-a538-a8e4bfe76f3f", null, "Admin", "ADMIN" },
                    { "4f41236f-2472-458b-9854-f8f82260891e", null, "Academic Writer", "ACADEMIC_WRITER" },
                    { "6dc509c3-8075-45dc-a2a2-be307fd6e801", null, "Content Writer", "CONTENT_WRITER" },
                    { "a037b7cf-5679-4941-99ba-48c0e90204ff", null, "Sales", "SALES" },
                    { "a4436c89-04b6-4d7c-98b9-1278caf2e5ce", null, "Seo", "SEO" },
                    { "c7681d30-a185-473c-90f9-e9ee51b787e1", null, "Super Admin", "SUPER_ADMIN" }
                });
        }
    }
}
