using System.Linq;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Context
{
    public class StockAnalysisContext : DbContext
    {
        public StockAnalysisContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<SectorEntity> Sectors { get; set; }
        public DbSet<SubSectorEntity> SubSectors { get; set; }
        public DbSet<SegmentEntity> Segments { get; set; }
        public DbSet<TickerEntity> Tickers { get; set; }
        public DbSet<FileImportEntity> FileImports { get; set; }
        public DbSet<HistoryTickerEntity> HistoryTickers { get; set; }

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
