using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsLeague.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReferee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Referees");

            migrationBuilder.DropColumn(
                name: "ExperienceYears",
                table: "Referees");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Referees");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Referees",
                newName: "Nationality");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Referees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Referees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Referees");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Referees");

            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "Referees",
                newName: "Name");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Referees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ExperienceYears",
                table: "Referees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Referees",
                type: "datetime2",
                nullable: true);
        }
    }
}
