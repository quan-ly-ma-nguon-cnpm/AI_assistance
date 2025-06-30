using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Application.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TieuDe",
                table: "PhanHoiCauHois");

            migrationBuilder.DropColumn(
                name: "NguoiDatCauHoi",
                table: "CauHois");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TieuDe",
                table: "PhanHoiCauHois",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NguoiDatCauHoi",
                table: "CauHois",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
