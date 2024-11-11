using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerberKuaforRandevu.Migrations
{
    /// <inheritdoc />
    public partial class Kuafor_Yetenek_Uzmanlik_Siniflarini_Ekle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kuaforler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kuaforler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kuaforler_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Yetenekler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yetenekler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KuaforlerUzmanliklar",
                columns: table => new
                {
                    KuaforId = table.Column<int>(type: "int", nullable: false),
                    YetenekId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KuaforlerUzmanliklar", x => new { x.KuaforId, x.YetenekId });
                    table.ForeignKey(
                        name: "FK_KuaforlerUzmanliklar_Kuaforler_KuaforId",
                        column: x => x.KuaforId,
                        principalTable: "Kuaforler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KuaforlerUzmanliklar_Yetenekler_YetenekId",
                        column: x => x.YetenekId,
                        principalTable: "Yetenekler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KuaforlerYetenekler",
                columns: table => new
                {
                    KuaforId = table.Column<int>(type: "int", nullable: false),
                    YetenekId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KuaforlerYetenekler", x => new { x.KuaforId, x.YetenekId });
                    table.ForeignKey(
                        name: "FK_KuaforlerYetenekler_Kuaforler_KuaforId",
                        column: x => x.KuaforId,
                        principalTable: "Kuaforler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KuaforlerYetenekler_Yetenekler_YetenekId",
                        column: x => x.YetenekId,
                        principalTable: "Yetenekler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kuaforler_KullaniciId",
                table: "Kuaforler",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_KuaforlerUzmanliklar_YetenekId",
                table: "KuaforlerUzmanliklar",
                column: "YetenekId");

            migrationBuilder.CreateIndex(
                name: "IX_KuaforlerYetenekler_YetenekId",
                table: "KuaforlerYetenekler",
                column: "YetenekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KuaforlerUzmanliklar");

            migrationBuilder.DropTable(
                name: "KuaforlerYetenekler");

            migrationBuilder.DropTable(
                name: "Kuaforler");

            migrationBuilder.DropTable(
                name: "Yetenekler");
        }
    }
}
