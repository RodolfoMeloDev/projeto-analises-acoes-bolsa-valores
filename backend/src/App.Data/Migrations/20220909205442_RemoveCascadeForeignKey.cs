using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Data.Migrations
{
    public partial class RemoveCascadeForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArquivosImportacao_Usuarios_UsuarioId",
                table: "ArquivosImportacao");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosArquivoImportacao_ArquivosImportacao_ArquivoImpor~",
                table: "HistoricosArquivoImportacao");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosArquivoImportacao_Tickers_TickerId",
                table: "HistoricosArquivoImportacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Segmentos_SubSetores_SubSetorId",
                table: "Segmentos");

            migrationBuilder.DropForeignKey(
                name: "FK_SubSetores_Setores_SetorId",
                table: "SubSetores");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickers_Segmentos_SegmentoId",
                table: "Tickers");

            migrationBuilder.AddForeignKey(
                name: "FK_ArquivosImportacao_Usuarios_UsuarioId",
                table: "ArquivosImportacao",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosArquivoImportacao_ArquivosImportacao_ArquivoImpor~",
                table: "HistoricosArquivoImportacao",
                column: "ArquivoImportacaoId",
                principalTable: "ArquivosImportacao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosArquivoImportacao_Tickers_TickerId",
                table: "HistoricosArquivoImportacao",
                column: "TickerId",
                principalTable: "Tickers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Segmentos_SubSetores_SubSetorId",
                table: "Segmentos",
                column: "SubSetorId",
                principalTable: "SubSetores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubSetores_Setores_SetorId",
                table: "SubSetores",
                column: "SetorId",
                principalTable: "Setores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickers_Segmentos_SegmentoId",
                table: "Tickers",
                column: "SegmentoId",
                principalTable: "Segmentos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArquivosImportacao_Usuarios_UsuarioId",
                table: "ArquivosImportacao");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosArquivoImportacao_ArquivosImportacao_ArquivoImpor~",
                table: "HistoricosArquivoImportacao");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosArquivoImportacao_Tickers_TickerId",
                table: "HistoricosArquivoImportacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Segmentos_SubSetores_SubSetorId",
                table: "Segmentos");

            migrationBuilder.DropForeignKey(
                name: "FK_SubSetores_Setores_SetorId",
                table: "SubSetores");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickers_Segmentos_SegmentoId",
                table: "Tickers");

            migrationBuilder.AddForeignKey(
                name: "FK_ArquivosImportacao_Usuarios_UsuarioId",
                table: "ArquivosImportacao",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosArquivoImportacao_ArquivosImportacao_ArquivoImpor~",
                table: "HistoricosArquivoImportacao",
                column: "ArquivoImportacaoId",
                principalTable: "ArquivosImportacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosArquivoImportacao_Tickers_TickerId",
                table: "HistoricosArquivoImportacao",
                column: "TickerId",
                principalTable: "Tickers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Segmentos_SubSetores_SubSetorId",
                table: "Segmentos",
                column: "SubSetorId",
                principalTable: "SubSetores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubSetores_Setores_SetorId",
                table: "SubSetores",
                column: "SetorId",
                principalTable: "Setores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickers_Segmentos_SegmentoId",
                table: "Tickers",
                column: "SegmentoId",
                principalTable: "Segmentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
