using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Data.Migrations
{
    public partial class NewColumnsInTableHistoryTicker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CAGRLucro",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CrescimentoEsperado",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Dpa",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Lpa",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MediaCrescimento",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Payout",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Roe",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CAGRLucro",
                table: "HistoricoTickers");

            migrationBuilder.DropColumn(
                name: "CrescimentoEsperado",
                table: "HistoricoTickers");

            migrationBuilder.DropColumn(
                name: "Dpa",
                table: "HistoricoTickers");

            migrationBuilder.DropColumn(
                name: "Lpa",
                table: "HistoricoTickers");

            migrationBuilder.DropColumn(
                name: "MediaCrescimento",
                table: "HistoricoTickers");

            migrationBuilder.DropColumn(
                name: "Payout",
                table: "HistoricoTickers");

            migrationBuilder.DropColumn(
                name: "Roe",
                table: "HistoricoTickers");
        }
    }
}
