using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RandevuSistemi.Migrations
{
    /// <inheritdoc />
    public partial class logyapilanmasi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    KullaniciID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciSifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciOnay = table.Column<bool>(type: "bit", nullable: false),
                    KullaniciTuru = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.KullaniciID);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    RandevuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByKullanici = table.Column<int>(type: "int", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaslangicSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    BitisSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    RandevuSuresi = table.Column<int>(type: "int", nullable: false),
                    RandevuTur = table.Column<int>(type: "int", nullable: false),
                    RandevuOnay = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.RandevuID);
                });

            migrationBuilder.CreateTable(
                name: "RandevuTurleri",
                columns: table => new
                {
                    RandevuTurID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RandevuTurAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandevuTurleri", x => x.RandevuTurID);
                });

            migrationBuilder.CreateTable(
                name: "AlinanRandevular",
                columns: table => new
                {
                    AlinanRandevuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RandevuID = table.Column<int>(type: "int", nullable: false),
                    KullaniciID = table.Column<int>(type: "int", nullable: false),
                    RandevuOnay = table.Column<bool>(type: "bit", nullable: false),
                    RandevuIptal = table.Column<bool>(type: "bit", nullable: false),
                    RandevuAciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandevuTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RandevuSaati = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlinanRandevular", x => x.AlinanRandevuID);
                    table.ForeignKey(
                        name: "FK_AlinanRandevular_Randevular_RandevuID",
                        column: x => x.RandevuID,
                        principalTable: "Randevular",
                        principalColumn: "RandevuID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlinanRandevular_RandevuID",
                table: "AlinanRandevular",
                column: "RandevuID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlinanRandevular");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "RandevuTurleri");

            migrationBuilder.DropTable(
                name: "Randevular");
        }
    }
}
