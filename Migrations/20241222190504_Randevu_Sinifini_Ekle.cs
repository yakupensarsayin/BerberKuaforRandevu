using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberKuaforRandevu.Migrations
{
    /// <inheritdoc />
    public partial class Randevu_Sinifini_Ekle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KuaforId = table.Column<int>(type: "int", nullable: false),
                    YetenekId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Onayli = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Randevular_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevular_Kuaforler_KuaforId",
                        column: x => x.KuaforId,
                        principalTable: "Kuaforler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Randevular_Yetenekler_YetenekId",
                        column: x => x.YetenekId,
                        principalTable: "Yetenekler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KuaforId",
                table: "Randevular",
                column: "KuaforId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KullaniciId",
                table: "Randevular",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_YetenekId",
                table: "Randevular",
                column: "YetenekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Randevular");
        }
    }
}
