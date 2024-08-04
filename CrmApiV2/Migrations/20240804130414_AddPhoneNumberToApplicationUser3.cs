using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrmApiV2.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberToApplicationUser3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "CreatedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53fc232b-19f8-4e5f-9d63-eddaf045cb0e", null, "Developer", "DEVELOPER" },
                    { "5d419dfb-fa73-446e-ae20-11c94d0912d4", null, "Super Admin", "SUPER_ADMIN" },
                    { "779c03b7-b49f-44c9-8a32-4f3f84ace6db", null, "Admin", "ADMIN" },
                    { "b0b1fa74-5542-4844-aa7e-2ec46ee7b722", null, "Academic Writer", "ACADEMIC_WRITER" },
                    { "b2aade6b-50aa-4086-9d60-accd09faccd5", null, "Seo", "SEO" },
                    { "bd8daf67-6bbd-422d-ac86-42f10a8aa9d1", null, "Sales", "SALES" },
                    { "eec4b03f-f304-4021-9ff5-56108547a65e", null, "Content Writer", "CONTENT_WRITER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53fc232b-19f8-4e5f-9d63-eddaf045cb0e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d419dfb-fa73-446e-ae20-11c94d0912d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "779c03b7-b49f-44c9-8a32-4f3f84ace6db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0b1fa74-5542-4844-aa7e-2ec46ee7b722");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2aade6b-50aa-4086-9d60-accd09faccd5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd8daf67-6bbd-422d-ac86-42f10a8aa9d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eec4b03f-f304-4021-9ff5-56108547a65e");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
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
    }
}
