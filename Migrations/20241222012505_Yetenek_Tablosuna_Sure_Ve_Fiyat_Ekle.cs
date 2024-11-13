using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberKuaforRandevu.Migrations
{
    /// <inheritdoc />
    public partial class Yetenek_Tablosuna_Sure_Ve_Fiyat_Ekle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Fiyat",
                table: "Yetenekler",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Sure",
                table: "Yetenekler",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fiyat",
                table: "Yetenekler");

            migrationBuilder.DropColumn(
                name: "Sure",
                table: "Yetenekler");
        }
    }
}
