using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartClassAPI.Migrations
{
    public partial class v14delMoTa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoTa",
                table: "TinhTrangPhongHoc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MoTa",
                table: "TinhTrangPhongHoc",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
