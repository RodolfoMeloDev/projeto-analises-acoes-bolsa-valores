using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.Data.Migrations
{
    public partial class BaseInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Senha = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubSetores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SetorId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSetores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubSetores_Setores_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArquivosImportacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    NomeArquivo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivosImportacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArquivosImportacao_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Segmentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SubSetorId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segmentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Segmentos_SubSetores_SubSetorId",
                        column: x => x.SubSetorId,
                        principalTable: "SubSetores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BaseTicker = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Ticker = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Empresa = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CNPJ = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    Descricao = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Site = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    RecuperacaoJudicial = table.Column<bool>(type: "boolean", nullable: false),
                    SegmentoId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickers_Segmentos_SegmentoId",
                        column: x => x.SegmentoId,
                        principalTable: "Segmentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricosArquivoImportacao",
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
                    Status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosArquivoImportacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosArquivoImportacao_ArquivosImportacao_ArquivoImpor~",
                        column: x => x.ArquivoImportacaoId,
                        principalTable: "ArquivosImportacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricosArquivoImportacao_Tickers_TickerId",
                        column: x => x.TickerId,
                        principalTable: "Tickers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArquivosImportacao_UsuarioId",
                table: "ArquivosImportacao",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosArquivoImportacao_ArquivoImportacaoId",
                table: "HistoricosArquivoImportacao",
                column: "ArquivoImportacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosArquivoImportacao_TickerId",
                table: "HistoricosArquivoImportacao",
                column: "TickerId");

            migrationBuilder.CreateIndex(
                name: "IX_Segmentos_SubSetorId",
                table: "Segmentos",
                column: "SubSetorId");

            migrationBuilder.CreateIndex(
                name: "IX_SubSetores_SetorId",
                table: "SubSetores",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickers_SegmentoId",
                table: "Tickers",
                column: "SegmentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricosArquivoImportacao");

            migrationBuilder.DropTable(
                name: "ArquivosImportacao");

            migrationBuilder.DropTable(
                name: "Tickers");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Segmentos");

            migrationBuilder.DropTable(
                name: "SubSetores");

            migrationBuilder.DropTable(
                name: "Setores");
        }
    }
}
