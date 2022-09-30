using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Data.Migrations
{
    public partial class RenameColumnsAndTablesToEnglishs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArquivosImportacao_Usuarios_UsuarioId",
                table: "ArquivosImportacao");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoTickers_ArquivosImportacao_ArquivoImportacaoId",
                table: "HistoricoTickers");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoTickers_Tickers_TickerId",
                table: "HistoricoTickers");

            migrationBuilder.DropForeignKey(
                name: "FK_Segmentos_SubSetores_SubSetorId",
                table: "Segmentos");

            migrationBuilder.DropForeignKey(
                name: "FK_SubSetores_Setores_SetorId",
                table: "SubSetores");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickers_Segmentos_SegmentoId",
                table: "Tickers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubSetores",
                table: "SubSetores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Setores",
                table: "Setores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Segmentos",
                table: "Segmentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoricoTickers",
                table: "HistoricoTickers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArquivosImportacao",
                table: "ArquivosImportacao");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "SubSetores",
                newName: "SubSectors");

            migrationBuilder.RenameTable(
                name: "Setores",
                newName: "Sectors");

            migrationBuilder.RenameTable(
                name: "Segmentos",
                newName: "Segments");

            migrationBuilder.RenameTable(
                name: "HistoricoTickers",
                newName: "HistoryTickers");

            migrationBuilder.RenameTable(
                name: "ArquivosImportacao",
                newName: "FileImports");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Tickers",
                newName: "TypeTicker");

            migrationBuilder.RenameColumn(
                name: "SegmentoId",
                table: "Tickers",
                newName: "SegmentId");

            migrationBuilder.RenameColumn(
                name: "RecuperacaoJudicial",
                table: "Tickers",
                newName: "JudicialRecovery");

            migrationBuilder.RenameColumn(
                name: "Empresa",
                table: "Tickers",
                newName: "Company");

            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "Tickers",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "Tickers",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Tickers",
                newName: "Active");

            migrationBuilder.RenameIndex(
                name: "IX_Tickers_SegmentoId",
                table: "Tickers",
                newName: "IX_Tickers_SegmentId");

            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "Users",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "Users",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Users",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "SetorId",
                table: "SubSectors",
                newName: "SectorId");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "SubSectors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "SubSectors",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "SubSectors",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "SubSectors",
                newName: "Active");

            migrationBuilder.RenameIndex(
                name: "IX_SubSetores_SetorId",
                table: "SubSectors",
                newName: "IX_SubSectors_SectorId");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Sectors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "Sectors",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "Sectors",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Sectors",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "SubSetorId",
                table: "Segments",
                newName: "SubSectorId");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Segments",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "Segments",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "Segments",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Segments",
                newName: "Active");

            migrationBuilder.RenameIndex(
                name: "IX_Segmentos_SubSetorId",
                table: "Segments",
                newName: "IX_Segments_SubSectorId");

            migrationBuilder.RenameColumn(
                name: "ValorMercado",
                table: "HistoryTickers",
                newName: "Pvp");

            migrationBuilder.RenameColumn(
                name: "PrecoValorPatrimonial",
                table: "HistoryTickers",
                newName: "ProfitCAGR");

            migrationBuilder.RenameColumn(
                name: "PrecoLucro",
                table: "HistoryTickers",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "MediaCrescimento",
                table: "HistoryTickers",
                newName: "ExpectedGrowth");

            migrationBuilder.RenameColumn(
                name: "MargemEbit",
                table: "HistoryTickers",
                newName: "EbitMargin");

            migrationBuilder.RenameColumn(
                name: "LiquidezMediaDiaria",
                table: "HistoryTickers",
                newName: "MarketValue");

            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "HistoryTickers",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "HistoryTickers",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "CrescimentoEsperado",
                table: "HistoryTickers",
                newName: "AverageGrowth");

            migrationBuilder.RenameColumn(
                name: "CAGRLucro",
                table: "HistoryTickers",
                newName: "AverageDailyLiquidity");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "HistoryTickers",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "ArquivoImportacaoId",
                table: "HistoryTickers",
                newName: "FileImportId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricoTickers_TickerId",
                table: "HistoryTickers",
                newName: "IX_HistoryTickers_TickerId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricoTickers_ArquivoImportacaoId",
                table: "HistoryTickers",
                newName: "IX_HistoryTickers_FileImportId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "FileImports",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "TipoArquivo",
                table: "FileImports",
                newName: "TypeFile");

            migrationBuilder.RenameColumn(
                name: "NomeArquivo",
                table: "FileImports",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "FileImports",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "FileImports",
                newName: "DateFile");

            migrationBuilder.RenameColumn(
                name: "DataArquivo",
                table: "FileImports",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "FileImports",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "FileImports",
                newName: "Active");

            migrationBuilder.RenameIndex(
                name: "IX_ArquivosImportacao_UsuarioId",
                table: "FileImports",
                newName: "IX_FileImports_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubSectors",
                table: "SubSectors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sectors",
                table: "Sectors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Segments",
                table: "Segments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryTickers",
                table: "HistoryTickers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileImports",
                table: "FileImports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileImports_Users_UserId",
                table: "FileImports",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryTickers_FileImports_FileImportId",
                table: "HistoryTickers",
                column: "FileImportId",
                principalTable: "FileImports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryTickers_Tickers_TickerId",
                table: "HistoryTickers",
                column: "TickerId",
                principalTable: "Tickers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Segments_SubSectors_SubSectorId",
                table: "Segments",
                column: "SubSectorId",
                principalTable: "SubSectors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubSectors_Sectors_SectorId",
                table: "SubSectors",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickers_Segments_SegmentId",
                table: "Tickers",
                column: "SegmentId",
                principalTable: "Segments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileImports_Users_UserId",
                table: "FileImports");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryTickers_FileImports_FileImportId",
                table: "HistoryTickers");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryTickers_Tickers_TickerId",
                table: "HistoryTickers");

            migrationBuilder.DropForeignKey(
                name: "FK_Segments_SubSectors_SubSectorId",
                table: "Segments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubSectors_Sectors_SectorId",
                table: "SubSectors");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickers_Segments_SegmentId",
                table: "Tickers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubSectors",
                table: "SubSectors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Segments",
                table: "Segments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sectors",
                table: "Sectors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryTickers",
                table: "HistoryTickers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileImports",
                table: "FileImports");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "SubSectors",
                newName: "SubSetores");

            migrationBuilder.RenameTable(
                name: "Segments",
                newName: "Segmentos");

            migrationBuilder.RenameTable(
                name: "Sectors",
                newName: "Setores");

            migrationBuilder.RenameTable(
                name: "HistoryTickers",
                newName: "HistoricoTickers");

            migrationBuilder.RenameTable(
                name: "FileImports",
                newName: "ArquivosImportacao");

            migrationBuilder.RenameColumn(
                name: "TypeTicker",
                table: "Tickers",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "SegmentId",
                table: "Tickers",
                newName: "SegmentoId");

            migrationBuilder.RenameColumn(
                name: "JudicialRecovery",
                table: "Tickers",
                newName: "RecuperacaoJudicial");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Tickers",
                newName: "DataAlteracao");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Tickers",
                newName: "DataCadastro");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "Tickers",
                newName: "Empresa");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Tickers",
                newName: "Ativo");

            migrationBuilder.RenameIndex(
                name: "IX_Tickers_SegmentId",
                table: "Tickers",
                newName: "IX_Tickers_SegmentoId");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Usuarios",
                newName: "Senha");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Usuarios",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Usuarios",
                newName: "DataAlteracao");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Usuarios",
                newName: "DataCadastro");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Usuarios",
                newName: "Ativo");

            migrationBuilder.RenameColumn(
                name: "SectorId",
                table: "SubSetores",
                newName: "SetorId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SubSetores",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "SubSetores",
                newName: "DataAlteracao");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "SubSetores",
                newName: "DataCadastro");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "SubSetores",
                newName: "Ativo");

            migrationBuilder.RenameIndex(
                name: "IX_SubSectors_SectorId",
                table: "SubSetores",
                newName: "IX_SubSetores_SetorId");

            migrationBuilder.RenameColumn(
                name: "SubSectorId",
                table: "Segmentos",
                newName: "SubSetorId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Segmentos",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Segmentos",
                newName: "DataAlteracao");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Segmentos",
                newName: "DataCadastro");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Segmentos",
                newName: "Ativo");

            migrationBuilder.RenameIndex(
                name: "IX_Segments_SubSectorId",
                table: "Segmentos",
                newName: "IX_Segmentos_SubSetorId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Setores",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Setores",
                newName: "DataAlteracao");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Setores",
                newName: "DataCadastro");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Setores",
                newName: "Ativo");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "HistoricoTickers",
                newName: "PrecoLucro");

            migrationBuilder.RenameColumn(
                name: "Pvp",
                table: "HistoricoTickers",
                newName: "ValorMercado");

            migrationBuilder.RenameColumn(
                name: "ProfitCAGR",
                table: "HistoricoTickers",
                newName: "PrecoValorPatrimonial");

            migrationBuilder.RenameColumn(
                name: "MarketValue",
                table: "HistoricoTickers",
                newName: "LiquidezMediaDiaria");

            migrationBuilder.RenameColumn(
                name: "FileImportId",
                table: "HistoricoTickers",
                newName: "ArquivoImportacaoId");

            migrationBuilder.RenameColumn(
                name: "ExpectedGrowth",
                table: "HistoricoTickers",
                newName: "MediaCrescimento");

            migrationBuilder.RenameColumn(
                name: "EbitMargin",
                table: "HistoricoTickers",
                newName: "MargemEbit");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "HistoricoTickers",
                newName: "DataAlteracao");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "HistoricoTickers",
                newName: "DataCadastro");

            migrationBuilder.RenameColumn(
                name: "AverageGrowth",
                table: "HistoricoTickers",
                newName: "CrescimentoEsperado");

            migrationBuilder.RenameColumn(
                name: "AverageDailyLiquidity",
                table: "HistoricoTickers",
                newName: "CAGRLucro");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "HistoricoTickers",
                newName: "Ativo");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryTickers_TickerId",
                table: "HistoricoTickers",
                newName: "IX_HistoricoTickers_TickerId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryTickers_FileImportId",
                table: "HistoricoTickers",
                newName: "IX_HistoricoTickers_ArquivoImportacaoId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ArquivosImportacao",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "TypeFile",
                table: "ArquivosImportacao",
                newName: "TipoArquivo");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "ArquivosImportacao",
                newName: "NomeArquivo");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ArquivosImportacao",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "ArquivosImportacao",
                newName: "DataAlteracao");

            migrationBuilder.RenameColumn(
                name: "DateFile",
                table: "ArquivosImportacao",
                newName: "DataCadastro");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "ArquivosImportacao",
                newName: "DataArquivo");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "ArquivosImportacao",
                newName: "Ativo");

            migrationBuilder.RenameIndex(
                name: "IX_FileImports_UserId",
                table: "ArquivosImportacao",
                newName: "IX_ArquivosImportacao_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubSetores",
                table: "SubSetores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Segmentos",
                table: "Segmentos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Setores",
                table: "Setores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoricoTickers",
                table: "HistoricoTickers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArquivosImportacao",
                table: "ArquivosImportacao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArquivosImportacao_Usuarios_UsuarioId",
                table: "ArquivosImportacao",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoTickers_ArquivosImportacao_ArquivoImportacaoId",
                table: "HistoricoTickers",
                column: "ArquivoImportacaoId",
                principalTable: "ArquivosImportacao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoTickers_Tickers_TickerId",
                table: "HistoricoTickers",
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
    }
}
