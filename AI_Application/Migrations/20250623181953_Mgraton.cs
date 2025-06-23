using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Application.Migrations
{
    /// <inheritdoc />
    public partial class Mgraton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CauHoiId",
                table: "PhanHois",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NguoiNhan",
                table: "PhanHois",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PhanHois_CauHoiId",
                table: "PhanHois",
                column: "CauHoiId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanHois_CauHois_CauHoiId",
                table: "PhanHois",
                column: "CauHoiId",
                principalTable: "CauHois",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhanHois_CauHois_CauHoiId",
                table: "PhanHois");

            migrationBuilder.DropIndex(
                name: "IX_PhanHois_CauHoiId",
                table: "PhanHois");

            migrationBuilder.DropColumn(
                name: "CauHoiId",
                table: "PhanHois");

            migrationBuilder.DropColumn(
                name: "NguoiNhan",
                table: "PhanHois");
        }
    }
}
