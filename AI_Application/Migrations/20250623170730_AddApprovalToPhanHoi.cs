using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddApprovalToPhanHoi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DaDuyet",
                table: "PhanHoiCauHois",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PhanHoiCauHois_CauHoiId",
                table: "PhanHoiCauHois",
                column: "CauHoiId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanHoiCauHois_CauHois_CauHoiId",
                table: "PhanHoiCauHois",
                column: "CauHoiId",
                principalTable: "CauHois",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhanHoiCauHois_CauHois_CauHoiId",
                table: "PhanHoiCauHois");

            migrationBuilder.DropIndex(
                name: "IX_PhanHoiCauHois_CauHoiId",
                table: "PhanHoiCauHois");

            migrationBuilder.DropColumn(
                name: "DaDuyet",
                table: "PhanHoiCauHois");
        }
    }
}
