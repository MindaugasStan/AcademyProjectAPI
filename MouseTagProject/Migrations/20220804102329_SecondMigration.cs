using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MouseTagProject.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDates_Candidates_CandidateId1",
                table: "UserDates");

            migrationBuilder.DropIndex(
                name: "IX_UserDates_CandidateId1",
                table: "UserDates");

            migrationBuilder.DropColumn(
                name: "CandidateId1",
                table: "UserDates");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "UserDates",
                newName: "Date");

            migrationBuilder.AddColumn<DateTime>(
                name: "WillBeContacted",
                table: "Candidates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WillBeContacted",
                table: "Candidates");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "UserDates",
                newName: "DateTime");

            migrationBuilder.AddColumn<int>(
                name: "CandidateId1",
                table: "UserDates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDates_CandidateId1",
                table: "UserDates",
                column: "CandidateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDates_Candidates_CandidateId1",
                table: "UserDates",
                column: "CandidateId1",
                principalTable: "Candidates",
                principalColumn: "Id");
        }
    }
}
