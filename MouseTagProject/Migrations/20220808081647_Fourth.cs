using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MouseTagProject.Migrations
{
    public partial class Fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateTechnology_Candidates_CandidateId",
                table: "CandidateTechnology");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateTechnology_Technologies_TechnologyId",
                table: "CandidateTechnology");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateTechnology",
                table: "CandidateTechnology");

            migrationBuilder.RenameTable(
                name: "CandidateTechnology",
                newName: "CandidateTechnologies");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateTechnology_TechnologyId",
                table: "CandidateTechnologies",
                newName: "IX_CandidateTechnologies_TechnologyId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateTechnology_CandidateId",
                table: "CandidateTechnologies",
                newName: "IX_CandidateTechnologies_CandidateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateTechnologies",
                table: "CandidateTechnologies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateTechnologies_Candidates_CandidateId",
                table: "CandidateTechnologies",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateTechnologies_Technologies_TechnologyId",
                table: "CandidateTechnologies",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateTechnologies_Candidates_CandidateId",
                table: "CandidateTechnologies");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateTechnologies_Technologies_TechnologyId",
                table: "CandidateTechnologies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateTechnologies",
                table: "CandidateTechnologies");

            migrationBuilder.RenameTable(
                name: "CandidateTechnologies",
                newName: "CandidateTechnology");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateTechnologies_TechnologyId",
                table: "CandidateTechnology",
                newName: "IX_CandidateTechnology_TechnologyId");

            migrationBuilder.RenameIndex(
                name: "IX_CandidateTechnologies_CandidateId",
                table: "CandidateTechnology",
                newName: "IX_CandidateTechnology_CandidateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateTechnology",
                table: "CandidateTechnology",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateTechnology_Candidates_CandidateId",
                table: "CandidateTechnology",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateTechnology_Technologies_TechnologyId",
                table: "CandidateTechnology",
                column: "TechnologyId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
