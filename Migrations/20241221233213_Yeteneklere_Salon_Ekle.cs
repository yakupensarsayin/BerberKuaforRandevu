using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberKuaforRandevu.Migrations
{
    /// <inheritdoc />
    public partial class Yeteneklere_Salon_Ekle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "Yetenekler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Yetenekler_SalonId",
                table: "Yetenekler",
                column: "SalonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Yetenekler_Salonlar_SalonId",
                table: "Yetenekler",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Yetenekler_Salonlar_SalonId",
                table: "Yetenekler");

            migrationBuilder.DropIndex(
                name: "IX_Yetenekler_SalonId",
                table: "Yetenekler");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "Yetenekler");
        }
    }
}
