using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Context
{
    public class AnaliseDeAcoesContext : DbContext
    {
        public AnaliseDeAcoesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserEntity> Usuarios { get; set; }
        public DbSet<SectorEntity> Setores { get; set; }
        public DbSet<SubSectorEntity> SubSetores { get; set; }
        public DbSet<SegmentEntity> Segmentos { get; set; }
        public DbSet<TickerEntity> Tickers { get; set; }
        public DbSet<FileImportEntity> ArquivosImportacao { get; set; }
        public DbSet<HistoryFileImportEntity> HistoricosArquivoImportacao { get; set; }

    }
}