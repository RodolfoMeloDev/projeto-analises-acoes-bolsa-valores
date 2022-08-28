using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace App.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<AnaliseDeAcoesContext>
    {
        public AnaliseDeAcoesContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost;Port=5432;Database=AnaliseDeAcoes;User Id=postgres;Password=admin;Timeout=15;";
            var optionsBuilder = new DbContextOptionsBuilder<AnaliseDeAcoesContext>();

            optionsBuilder.UseNpgsql(connectionString);

            return new AnaliseDeAcoesContext(optionsBuilder.Options);
        }
    }
}