using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Data.Migrations
{
    public partial class RenameColumnStatusForAtivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tickers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubSetores");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Setores");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Segmentos");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "HistoricosArquivoImportacao");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ArquivosImportacao");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Tickers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "SubSetores",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Setores",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Segmentos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "HistoricosArquivoImportacao",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "ArquivosImportacao",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Tickers");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "SubSetores");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Setores");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Segmentos");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "HistoricosArquivoImportacao");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "ArquivosImportacao");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Usuarios",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Tickers",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SubSetores",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Setores",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Segmentos",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "HistoricosArquivoImportacao",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ArquivosImportacao",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");
        }
    }
}
