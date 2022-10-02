using App.Data.Context;
using App.Data.Repository;
using App.Domain.Entities;
using App.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Implementations
{
    public class BaseTickerImplementation : BaseRepository<BaseTickerEntity>, IBaseTickerRepository
    {
        private DbSet<BaseTickerEntity> _dataSet;

        public BaseTickerImplementation(StockAnalysisContext context) : base(context)
        {
            _dataSet = context.Set<BaseTickerEntity>();
        }
    }
}