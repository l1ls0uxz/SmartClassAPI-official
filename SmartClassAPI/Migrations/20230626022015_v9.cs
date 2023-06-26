using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartClassAPI.Migrations
{
    public partial class v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTai",
                table: "TaiLieuHocTap",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayTai",
                table: "TaiLieuHocTap");
        }
    }
}
