using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MouseTagProject.Migrations
{
    public partial class ThirdInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDates_Candidates_CandidateId",
                table: "UserDates");

            migrationBuilder.DropIndex(
                name: "IX_UserDates_CandidateId",
                table: "UserDates");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "UserDates");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserDates");

            migrationBuilder.CreateTable(
                name: "CandidateUserDate",
                columns: table => new
                {
                    CandidatesId = table.Column<int>(type: "int", nullable: false),
                    WhenWasContactedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateUserDate", x => new { x.CandidatesId, x.WhenWasContactedId });
                    table.ForeignKey(
                        name: "FK_CandidateUserDate_Candidates_CandidatesId",
                        column: x => x.CandidatesId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateUserDate_UserDates_WhenWasContactedId",
                        column: x => x.WhenWasContactedId,
                        principalTable: "UserDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateUserDate_WhenWasContactedId",
                table: "CandidateUserDate",
                column: "WhenWasContactedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateUserDate");

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "UserDates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserDates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserDates_CandidateId",
                table: "UserDates",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDates_Candidates_CandidateId",
                table: "UserDates",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id");
        }
    }
}
