using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrmApiV2.Migrations
{
    /// <inheritdoc />
    public partial class addIsComplete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "Projects");

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
    }
}
