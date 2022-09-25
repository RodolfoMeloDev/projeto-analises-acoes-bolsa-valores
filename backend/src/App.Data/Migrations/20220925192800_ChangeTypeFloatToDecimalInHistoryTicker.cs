using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Data.Migrations
{
    public partial class ChangeTypeFloatToDecimalInHistoryTicker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ValorMercado",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Roic",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecoValorPatrimonial",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecoUnitario",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecoLucro",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "MargemEbit",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "LiquidezMediaDiaria",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EvEbit",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "DividendYield",
                table: "HistoricoTickers",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ValorMercado",
                table: "HistoricoTickers",
                type: "real",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Roic",
                table: "HistoricoTickers",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<float>(
                name: "PrecoValorPatrimonial",
                table: "HistoricoTickers",
                type: "real",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "PrecoUnitario",
                table: "HistoricoTickers",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<float>(
                name: "PrecoLucro",
                table: "HistoricoTickers",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<float>(
                name: "MargemEbit",
                table: "HistoricoTickers",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<float>(
                name: "LiquidezMediaDiaria",
                table: "HistoricoTickers",
                type: "real",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "EvEbit",
                table: "HistoricoTickers",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<float>(
                name: "DividendYield",
                table: "HistoricoTickers",
                type: "real",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);
        }
    }
}
