using System.Linq;
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
        public DbSet<HistoryTickerEntity> HistoricoTickers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.NoAction;
        }

    }
}
