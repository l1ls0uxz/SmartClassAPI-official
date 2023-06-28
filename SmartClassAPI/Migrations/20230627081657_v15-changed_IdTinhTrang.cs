using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartClassAPI.Migrations
{
    public partial class v15changed_IdTinhTrang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TinhTrang",
                table: "PhongHoc",
                newName: "IdTinhTrang");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdTinhTrang",
                table: "PhongHoc",
                newName: "TinhTrang");
        }
    }
}
