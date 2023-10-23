using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STGeneticsTechTestLeonardoMosquera.Server.Migrations
{
    /// <inheritdoc />
    public partial class localfinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Selected",
                table: "Animal",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Selected",
                table: "Animal");
        }
    }
}
