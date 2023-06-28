using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartClassAPI.Migrations
{
    public partial class v16_addForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PhongHoc_IdTinhTrang",
                table: "PhongHoc",
                column: "IdTinhTrang");

            migrationBuilder.AddForeignKey(
                name: "FK_PhongHoc_TinhTrangPhongHoc_IdTinhTrang",
                table: "PhongHoc",
                column: "IdTinhTrang",
                principalTable: "TinhTrangPhongHoc",
                principalColumn: "IdTinhTrang",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhongHoc_TinhTrangPhongHoc_IdTinhTrang",
                table: "PhongHoc");

            migrationBuilder.DropIndex(
                name: "IX_PhongHoc_IdTinhTrang",
                table: "PhongHoc");
        }
    }
}
