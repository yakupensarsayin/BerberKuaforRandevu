using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberKuaforRandevu.Migrations
{
    /// <inheritdoc />
    public partial class Salonlar_Kuaforler_Ve_Salon_Turleri_Iliskilerini_Ekle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "Kuaforler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SalonTurleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalonTurleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salonlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SalonTuruId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salonlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salonlar_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Salonlar_SalonTurleri_SalonTuruId",
                        column: x => x.SalonTuruId,
                        principalTable: "SalonTurleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kuaforler_SalonId",
                table: "Kuaforler",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Salonlar_AdminId",
                table: "Salonlar",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Salonlar_SalonTuruId",
                table: "Salonlar",
                column: "SalonTuruId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kuaforler_Salonlar_SalonId",
                table: "Kuaforler",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kuaforler_Salonlar_SalonId",
                table: "Kuaforler");

            migrationBuilder.DropTable(
                name: "Salonlar");

            migrationBuilder.DropTable(
                name: "SalonTurleri");

            migrationBuilder.DropIndex(
                name: "IX_Kuaforler_SalonId",
                table: "Kuaforler");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "Kuaforler");
        }
    }
}
