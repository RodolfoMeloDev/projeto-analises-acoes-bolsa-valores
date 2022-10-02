using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Data.Migrations
{
    public partial class RecreatedTableTicker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickers_Segments_SegmentId",
                table: "Tickers");

            migrationBuilder.DropIndex(
                name: "IX_Tickers_SegmentId",
                table: "Tickers");

            migrationBuilder.DropColumn(
                name: "BaseTicker",
                table: "Tickers");

            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Tickers");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Tickers");

            migrationBuilder.DropColumn(
                name: "SegmentId",
                table: "Tickers");

            migrationBuilder.AddColumn<int>(
                name: "BaseTickerId",
                table: "Tickers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickers_BaseTickerId",
                table: "Tickers",
                column: "BaseTickerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickers_BaseTickers_BaseTickerId",
                table: "Tickers",
                column: "BaseTickerId",
                principalTable: "BaseTickers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickers_BaseTickers_BaseTickerId",
                table: "Tickers");

            migrationBuilder.DropIndex(
                name: "IX_Tickers_BaseTickerId",
                table: "Tickers");

            migrationBuilder.DropColumn(
                name: "BaseTickerId",
                table: "Tickers");

            migrationBuilder.AddColumn<string>(
                name: "BaseTicker",
                table: "Tickers",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Tickers",
                type: "character varying(18)",
                maxLength: 18,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Tickers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SegmentId",
                table: "Tickers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickers_SegmentId",
                table: "Tickers",
                column: "SegmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickers_Segments_SegmentId",
                table: "Tickers",
                column: "SegmentId",
                principalTable: "Segments",
                principalColumn: "Id");
        }
    }
}
