using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrmApiV2.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06488da0-3e06-48bf-922d-328a4886fb61");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11f635a2-7c4e-430a-93a7-cfc68e568077");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cdafdbb-f739-4e7b-b14d-ecc7589f2f9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "606b8f6b-43e0-4baa-bd46-5f4741a10b30");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66b7aa12-fec9-4e7c-b45d-e7e47be92afa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7dd89a5a-39cd-4be4-9a39-6603c5395f71");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87672760-26e3-4907-9f9f-af2ee92a6697");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06488da0-3e06-48bf-922d-328a4886fb61", null, "Developer", "DEVELOPER" },
                    { "11f635a2-7c4e-430a-93a7-cfc68e568077", null, "Admin", "ADMIN" },
                    { "1cdafdbb-f739-4e7b-b14d-ecc7589f2f9e", null, "Content Writer", "CONTENT_WRITER" },
                    { "606b8f6b-43e0-4baa-bd46-5f4741a10b30", null, "Seo", "SEO" },
                    { "66b7aa12-fec9-4e7c-b45d-e7e47be92afa", null, "Academic Writer", "ACADEMIC_WRITER" },
                    { "7dd89a5a-39cd-4be4-9a39-6603c5395f71", null, "Sales", "SALES" },
                    { "87672760-26e3-4907-9f9f-af2ee92a6697", null, "Super Admin", "SUPER_ADMIN" }
                });
        }
    }
}
