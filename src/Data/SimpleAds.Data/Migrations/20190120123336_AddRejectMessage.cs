using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleAds.Data.Migrations
{
    public partial class AddRejectMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectMessage",
                table: "Ads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectMessage",
                table: "Ads");
        }
    }
}
