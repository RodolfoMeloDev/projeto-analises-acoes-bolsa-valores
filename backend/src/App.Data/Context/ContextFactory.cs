using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace App.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<StockAnalysisContext>
    {
        public StockAnalysisContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost;Port=5432;Database=StockAnalysis;User Id=postgres;Password=admin;Timeout=15;";
            var optionsBuilder = new DbContextOptionsBuilder<StockAnalysisContext>();

            optionsBuilder.UseNpgsql(connectionString);

            return new StockAnalysisContext(optionsBuilder.Options);
        }
    }
}