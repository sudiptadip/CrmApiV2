using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrmApiV2.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserProjectForeignKeys2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "247a5d47-b9df-4ffd-968b-3bacf501bd54");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "395e9f0d-e5c1-4654-a284-947731db68ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50255a06-a579-4890-80cb-6c2e4091e41d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b637e82-1c2e-4c66-a24b-256483a33a87");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b6432e3-93c4-43b6-80c3-51a11c133907");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9e0a392-b942-48c1-8fde-cedc9a438d67");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec3cf849-5d83-4c74-902e-4c004a5a38a9");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "219c4dcb-50f6-4726-ad5b-5d1a10a284dc", null, "Content Writer", "CONTENT_WRITER" },
                    { "36541b86-463e-4303-bf7f-90769e0a44ad", null, "Sales", "SALES" },
                    { "4299aef3-b3fe-49c5-8086-e4bb744dd70d", null, "Super Admin", "SUPER_ADMIN" },
                    { "760bd4a2-1d42-43f3-9fa7-4817732eb5a5", null, "Developer", "DEVELOPER" },
                    { "a72e34dc-384d-4ae7-a6ff-7f6a39b8abaf", null, "Academic Writer", "ACADEMIC_WRITER" },
                    { "ab73bb66-f34a-428e-b773-bf5c116362e7", null, "Admin", "ADMIN" },
                    { "f36998d1-25ba-4e2c-a26a-a13208428d7b", null, "Seo", "SEO" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "219c4dcb-50f6-4726-ad5b-5d1a10a284dc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36541b86-463e-4303-bf7f-90769e0a44ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4299aef3-b3fe-49c5-8086-e4bb744dd70d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "760bd4a2-1d42-43f3-9fa7-4817732eb5a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a72e34dc-384d-4ae7-a6ff-7f6a39b8abaf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab73bb66-f34a-428e-b773-bf5c116362e7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f36998d1-25ba-4e2c-a26a-a13208428d7b");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Projects",
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
                    { "247a5d47-b9df-4ffd-968b-3bacf501bd54", null, "Sales", "SALES" },
                    { "395e9f0d-e5c1-4654-a284-947731db68ce", null, "Super Admin", "SUPER_ADMIN" },
                    { "50255a06-a579-4890-80cb-6c2e4091e41d", null, "Content Writer", "CONTENT_WRITER" },
                    { "6b637e82-1c2e-4c66-a24b-256483a33a87", null, "Developer", "DEVELOPER" },
                    { "7b6432e3-93c4-43b6-80c3-51a11c133907", null, "Admin", "ADMIN" },
                    { "d9e0a392-b942-48c1-8fde-cedc9a438d67", null, "Seo", "SEO" },
                    { "ec3cf849-5d83-4c74-902e-4c004a5a38a9", null, "Academic Writer", "ACADEMIC_WRITER" }
                });
        }
    }
}
