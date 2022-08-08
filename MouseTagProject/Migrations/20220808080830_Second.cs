using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MouseTagProject.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_Candidates_CandidateId",
                table: "Technologies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDates_Candidates_CandidateId",
                table: "UserDates");

            migrationBuilder.DropIndex(
                name: "IX_Technologies_CandidateId",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Technologies");

            migrationBuilder.AlterColumn<int>(
                name: "CandidateId",
                table: "UserDates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CandidateTechnology",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    TechnologyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateTechnology", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateTechnology_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateTechnology_Technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateTechnology_CandidateId",
                table: "CandidateTechnology",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateTechnology_TechnologyId",
                table: "CandidateTechnology",
                column: "TechnologyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDates_Candidates_CandidateId",
                table: "UserDates",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDates_Candidates_CandidateId",
                table: "UserDates");

            migrationBuilder.DropTable(
                name: "CandidateTechnology");

            migrationBuilder.AlterColumn<int>(
                name: "CandidateId",
                table: "UserDates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "Technologies",
                type: "int",
                nullable: true);

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
                name: "FK_UserDates_Candidates_CandidateId",
                table: "UserDates",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id");
        }
    }
}
