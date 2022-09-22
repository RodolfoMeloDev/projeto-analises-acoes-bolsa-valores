using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Data.Migrations
{
    public partial class RemoveColumnVolumeFinanceiroTableHistoryTicker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolumeFinanceiro",
                table: "HistoricoTickers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "VolumeFinanceiro",
                table: "HistoricoTickers",
                type: "real",
                nullable: true);
        }
    }
}
