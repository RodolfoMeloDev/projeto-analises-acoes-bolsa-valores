using App.Domain.Dtos.BaseTicker;
using App.Domain.Interfaces.Services.BaseTicker;
using App.Domain.Repository;
using AutoMapper;

namespace App.Service.Services
{
    public class BaseTickerService : IBaseTickerService
    {
        private IBaseTickerRepository _repository;
        private readonly IMapper _mapper;

        public BaseTickerService(IBaseTickerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BaseTickerDto>> GetAllBaseTickers()
        {
            var listEntity = await _repository.SelectAllAsync();

            return _mapper.Map<IEnumerable<BaseTickerDto>>(listEntity);
        }
    }
}