using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.Data.Migrations
{
    public partial class RecreatedDataBaseInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubSectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SectorId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubSectors_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FileImports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateFile = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TypeFile = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileImports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileImports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SubSectorId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Segments_SubSectors_SubSectorId",
                        column: x => x.SubSectorId,
                        principalTable: "SubSectors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BaseTicker = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Ticker = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Company = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CNPJ = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    TypeTicker = table.Column<int>(type: "integer", nullable: false),
                    JudicialRecovery = table.Column<bool>(type: "boolean", nullable: false),
                    SegmentId = table.Column<int>(type: "integer", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickers_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HistoryTickers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileImportId = table.Column<int>(type: "integer", nullable: false),
                    TickerId = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceByProfit = table.Column<decimal>(type: "numeric", nullable: false),
                    Roic = table.Column<decimal>(type: "numeric", nullable: false),
                    EvEbit = table.Column<decimal>(type: "numeric", nullable: false),
                    EbitMargin = table.Column<decimal>(type: "numeric", nullable: false),
                    Lpa = table.Column<decimal>(type: "numeric", nullable: false),
                    Vpa = table.Column<decimal>(type: "numeric", nullable: false),
                    Roe = table.Column<decimal>(type: "numeric", nullable: false),
                    ExpectedGrowth = table.Column<decimal>(type: "numeric", nullable: false),
                    AverageGrowth = table.Column<decimal>(type: "numeric", nullable: false),
                    DividendYield = table.Column<decimal>(type: "numeric", nullable: true),
                    Pvp = table.Column<decimal>(type: "numeric", nullable: true),
                    Dpa = table.Column<decimal>(type: "numeric", nullable: true),
                    Payout = table.Column<decimal>(type: "numeric", nullable: true),
                    ProfitCAGR = table.Column<decimal>(type: "numeric", nullable: true),
                    AverageDailyLiquidity = table.Column<decimal>(type: "numeric", nullable: true),
                    MarketValue = table.Column<decimal>(type: "numeric", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryTickers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryTickers_FileImports_FileImportId",
                        column: x => x.FileImportId,
                        principalTable: "FileImports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HistoryTickers_Tickers_TickerId",
                        column: x => x.TickerId,
                        principalTable: "Tickers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileImports_UserId",
                table: "FileImports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryTickers_FileImportId",
                table: "HistoryTickers",
                column: "FileImportId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryTickers_TickerId",
                table: "HistoryTickers",
                column: "TickerId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_SubSectorId",
                table: "Segments",
                column: "SubSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_SubSectors_SectorId",
                table: "SubSectors",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickers_SegmentId",
                table: "Tickers",
                column: "SegmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryTickers");

            migrationBuilder.DropTable(
                name: "FileImports");

            migrationBuilder.DropTable(
                name: "Tickers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Segments");

            migrationBuilder.DropTable(
                name: "SubSectors");

            migrationBuilder.DropTable(
                name: "Sectors");
        }
    }
}
