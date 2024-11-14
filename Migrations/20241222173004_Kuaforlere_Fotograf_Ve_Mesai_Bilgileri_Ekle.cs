using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberKuaforRandevu.Migrations
{
    /// <inheritdoc />
    public partial class Kuaforlere_Fotograf_Ve_Mesai_Bilgileri_Ekle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Fotograf",
                table: "Kuaforler",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MesaiBaslangic",
                table: "Kuaforler",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MesaiBitis",
                table: "Kuaforler",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fotograf",
                table: "Kuaforler");

            migrationBuilder.DropColumn(
                name: "MesaiBaslangic",
                table: "Kuaforler");

            migrationBuilder.DropColumn(
                name: "MesaiBitis",
                table: "Kuaforler");
        }
    }
}
