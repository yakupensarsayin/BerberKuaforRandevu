using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberKuaforRandevu.Migrations
{
    /// <inheritdoc />
    public partial class Fotograf_Yolu_Eklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotografYolu",
                table: "Salonlar",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotografYolu",
                table: "Salonlar");
        }
    }
}
