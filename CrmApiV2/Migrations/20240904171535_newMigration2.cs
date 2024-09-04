using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmApiV2.Migrations
{
    /// <inheritdoc />
    public partial class newMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalBreakTime",
                table: "DailyUserSummaries");

            migrationBuilder.DropColumn(
                name: "TotalWorkingTime",
                table: "DailyUserSummaries");

            migrationBuilder.AddColumn<long>(
                name: "TotalBreakTimeInSeconds",
                table: "DailyUserSummaries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TotalWorkingTimeInSeconds",
                table: "DailyUserSummaries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalBreakTimeInSeconds",
                table: "DailyUserSummaries");

            migrationBuilder.DropColumn(
                name: "TotalWorkingTimeInSeconds",
                table: "DailyUserSummaries");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalBreakTime",
                table: "DailyUserSummaries",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalWorkingTime",
                table: "DailyUserSummaries",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
