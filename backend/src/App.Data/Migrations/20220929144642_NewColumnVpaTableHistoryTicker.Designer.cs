﻿// <auto-generated />
using System;
using App.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.Data.Migrations
{
    [DbContext(typeof(AnaliseDeAcoesContext))]
    [Migration("20220929144642_NewColumnVpaTableHistoryTicker")]
    partial class NewColumnVpaTableHistoryTicker
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("App.Domain.Entities.FileImportEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataArquivo")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DataCadastro")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("NomeArquivo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("TipoArquivo")
                        .HasColumnType("integer");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ArquivosImportacao");
                });

            modelBuilder.Entity("App.Domain.Entities.HistoryTickerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ArquivoImportacaoId")
                        .HasColumnType("integer");

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("CAGRLucro")
                        .HasColumnType("numeric");

                    b.Property<decimal>("CrescimentoEsperado")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DataCadastro")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal?>("DividendYield")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Dpa")
                        .HasColumnType("numeric");

                    b.Property<decimal>("EvEbit")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("LiquidezMediaDiaria")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Lpa")
                        .HasColumnType("numeric");

                    b.Property<decimal>("MargemEbit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("MediaCrescimento")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Payout")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PrecoLucro")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PrecoUnitario")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("PrecoValorPatrimonial")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Roe")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Roic")
                        .HasColumnType("numeric");

                    b.Property<int>("TickerId")
                        .HasColumnType("integer");

                    b.Property<decimal?>("ValorMercado")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Vpa")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ArquivoImportacaoId");

                    b.HasIndex("TickerId");

                    b.ToTable("HistoricoTickers");
                });

            modelBuilder.Entity("App.Domain.Entities.SectorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DataCadastro")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Setores");
                });

            modelBuilder.Entity("App.Domain.Entities.SegmentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DataCadastro")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("SubSetorId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SubSetorId");

                    b.ToTable("Segmentos");
                });

            modelBuilder.Entity("App.Domain.Entities.SubSectorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DataCadastro")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("SetorId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SetorId");

                    b.ToTable("SubSetores");
                });

            modelBuilder.Entity("App.Domain.Entities.TickerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<string>("BaseTicker")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("CNPJ")
                        .HasMaxLength(18)
                        .HasColumnType("character varying(18)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DataCadastro")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Empresa")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("RecuperacaoJudicial")
                        .HasColumnType("boolean");

                    b.Property<int?>("SegmentoId")
                        .HasColumnType("integer");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<int>("Tipo")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SegmentoId");

                    b.ToTable("Tickers");
                });

            modelBuilder.Entity("App.Domain.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DataCadastro")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("App.Domain.Entities.FileImportEntity", b =>
                {
                    b.HasOne("App.Domain.Entities.UserEntity", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("App.Domain.Entities.HistoryTickerEntity", b =>
                {
                    b.HasOne("App.Domain.Entities.FileImportEntity", "ArquivoImportacao")
                        .WithMany()
                        .HasForeignKey("ArquivoImportacaoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("App.Domain.Entities.TickerEntity", "Ticker")
                        .WithMany()
                        .HasForeignKey("TickerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ArquivoImportacao");

                    b.Navigation("Ticker");
                });

            modelBuilder.Entity("App.Domain.Entities.SegmentEntity", b =>
                {
                    b.HasOne("App.Domain.Entities.SubSectorEntity", "SubSetor")
                        .WithMany("Segmentos")
                        .HasForeignKey("SubSetorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("SubSetor");
                });

            modelBuilder.Entity("App.Domain.Entities.SubSectorEntity", b =>
                {
                    b.HasOne("App.Domain.Entities.SectorEntity", "Setor")
                        .WithMany("SubSetores")
                        .HasForeignKey("SetorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Setor");
                });

            modelBuilder.Entity("App.Domain.Entities.TickerEntity", b =>
                {
                    b.HasOne("App.Domain.Entities.SegmentEntity", "Segmento")
                        .WithMany()
                        .HasForeignKey("SegmentoId");

                    b.Navigation("Segmento");
                });

            modelBuilder.Entity("App.Domain.Entities.SectorEntity", b =>
                {
                    b.Navigation("SubSetores");
                });

            modelBuilder.Entity("App.Domain.Entities.SubSectorEntity", b =>
                {
                    b.Navigation("Segmentos");
                });
#pragma warning restore 612, 618
        }
    }
}
