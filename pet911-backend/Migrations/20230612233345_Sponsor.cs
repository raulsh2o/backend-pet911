using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pet911_backend.Migrations
{
    /// <inheritdoc />
    public partial class Sponsor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Sponsored",
                table: "Service",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sponsored",
                table: "Service");
        }
    }
}
