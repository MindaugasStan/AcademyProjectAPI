using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MouseTagProject.Migrations
{
    public partial class SecondInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_Candidates_CandidateId",
                table: "Technologies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDates_Candidates_CandidateId1",
                table: "UserDates");

            migrationBuilder.DropIndex(
                name: "IX_UserDates_CandidateId1",
                table: "UserDates");

            migrationBuilder.DropIndex(
                name: "IX_Technologies_CandidateId",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "CandidateId1",
                table: "UserDates");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Technologies");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserDates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "WillBeContacted",
                table: "Candidates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CandidateTechnology",
                columns: table => new
                {
                    CandidatesId = table.Column<int>(type: "int", nullable: false),
                    TechnologiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateTechnology", x => new { x.CandidatesId, x.TechnologiesId });
                    table.ForeignKey(
                        name: "FK_CandidateTechnology_Candidates_CandidatesId",
                        column: x => x.CandidatesId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateTechnology_Technologies_TechnologiesId",
                        column: x => x.TechnologiesId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateTechnology_TechnologiesId",
                table: "CandidateTechnology",
                column: "TechnologiesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateTechnology");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserDates");

            migrationBuilder.DropColumn(
                name: "WillBeContacted",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "CandidateId1",
                table: "UserDates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "Technologies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDates_CandidateId1",
                table: "UserDates",
                column: "CandidateId1");

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_CandidateId",
                table: "Technologies",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technologies_Candidates_CandidateId",
                table: "Technologies",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDates_Candidates_CandidateId1",
                table: "UserDates",
                column: "CandidateId1",
                principalTable: "Candidates",
                principalColumn: "Id");
        }
    }
}
