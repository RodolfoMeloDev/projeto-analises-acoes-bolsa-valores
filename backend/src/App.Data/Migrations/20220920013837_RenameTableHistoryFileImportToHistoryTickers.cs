using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.Data.Migrations
{
    public partial class RenameTableHistoryFileImportToHistoryTickers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricosArquivoImportacao");

            migrationBuilder.CreateTable(
                name: "HistoricoTickers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArquivoImportacaoId = table.Column<int>(type: "integer", nullable: false),
                    TickerId = table.Column<int>(type: "integer", nullable: false),
                    PrecoUnitario = table.Column<float>(type: "real", nullable: false),
                    PrecoLucro = table.Column<float>(type: "real", nullable: false),
                    Roic = table.Column<float>(type: "real", nullable: false),
                    EvEbit = table.Column<float>(type: "real", nullable: false),
                    MargemEbit = table.Column<float>(type: "real", nullable: false),
                    DividendYield = table.Column<float>(type: "real", nullable: true),
                    PrecoValorPatrimonial = table.Column<float>(type: "real", nullable: true),
                    LiquidezMediaDiaria = table.Column<float>(type: "real", nullable: true),
                    ValorMercado = table.Column<float>(type: "real", nullable: true),
                    VolumeFinanceiro = table.Column<float>(type: "real", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoTickers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoTickers_ArquivosImportacao_ArquivoImportacaoId",
                        column: x => x.ArquivoImportacaoId,
                        principalTable: "ArquivosImportacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HistoricoTickers_Tickers_TickerId",
                        column: x => x.TickerId,
                        principalTable: "Tickers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoTickers_ArquivoImportacaoId",
                table: "HistoricoTickers",
                column: "ArquivoImportacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoTickers_TickerId",
                table: "HistoricoTickers",
                column: "TickerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoTickers");

            migrationBuilder.CreateTable(
                name: "HistoricosArquivoImportacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArquivoImportacaoId = table.Column<int>(type: "integer", nullable: false),
                    TickerId = table.Column<int>(type: "integer", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DividendYield = table.Column<float>(type: "real", nullable: true),
                    EvEbit = table.Column<float>(type: "real", nullable: false),
                    LiquidezMediaDiaria = table.Column<float>(type: "real", nullable: true),
                    MargemEbit = table.Column<float>(type: "real", nullable: false),
                    PrecoLucro = table.Column<float>(type: "real", nullable: false),
                    PrecoUnitario = table.Column<float>(type: "real", nullable: false),
                    PrecoValorPatrimonial = table.Column<float>(type: "real", nullable: true),
                    Roic = table.Column<float>(type: "real", nullable: false),
                    ValorMercado = table.Column<float>(type: "real", nullable: true),
                    VolumeFinanceiro = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosArquivoImportacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosArquivoImportacao_ArquivosImportacao_ArquivoImpor~",
                        column: x => x.ArquivoImportacaoId,
                        principalTable: "ArquivosImportacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HistoricosArquivoImportacao_Tickers_TickerId",
                        column: x => x.TickerId,
                        principalTable: "Tickers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosArquivoImportacao_ArquivoImportacaoId",
                table: "HistoricosArquivoImportacao",
                column: "ArquivoImportacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosArquivoImportacao_TickerId",
                table: "HistoricosArquivoImportacao",
                column: "TickerId");
        }
    }
}
