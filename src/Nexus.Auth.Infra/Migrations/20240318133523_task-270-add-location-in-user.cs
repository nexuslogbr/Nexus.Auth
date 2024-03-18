using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Auth.Infra.Migrations
{
    /// <inheritdoc />
    public partial class task270addlocationinuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "AspNetUsers");
        }
    }
}
