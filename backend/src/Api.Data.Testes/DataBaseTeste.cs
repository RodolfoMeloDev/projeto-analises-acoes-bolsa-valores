using App.Data.Context;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Testes
{
    public class DataBaseTeste
    {
        public StockAnalysisContext ContextoBD { get; private set; }

        public DataBaseTeste()
        {
            var optionsBuilder = new DbContextOptionsBuilder<StockAnalysisContext>();

            optionsBuilder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

            ContextoBD = new StockAnalysisContext(optionsBuilder.Options);
        }

        public static void InsertData(StockAnalysisContext context)
        {
            context.Users.Add(new UserEntity
            {
                Id = 1,
                Login = "TESTE",
                Name = "Rodolfo",
                NickName = "Rodolfo",
                Password = "teste",
                Active = true,
                DateCreated = DateTime.Now
            });

            context.Sectors.Add(new SectorEntity
            {
                Id = 1,
                Name = "Teste",
                Active = true,
                DateCreated = DateTime.Now
            });

            context.SubSectors.Add(new SubSectorEntity
            {
                Id = 1,
                Name = "Teste",
                SectorId = 1,
                Active = true,
                DateCreated = DateTime.Now
            });

            context.Segments.Add(new SegmentEntity
            {
                Id = 1,
                Name = "Teste",
                SubSectorId = 1,
                Active = true,
                DateCreated = DateTime.Now
            });

            context.BaseTickers.Add(new BaseTickerEntity
            {
                Id = 1,
                BaseTicker = "A",
                Company = "Teste",
                SegmentId = 1,
                Active = true,
                DateCreated = DateTime.Now
            });

            context.SaveChanges();
        }
    }
}
