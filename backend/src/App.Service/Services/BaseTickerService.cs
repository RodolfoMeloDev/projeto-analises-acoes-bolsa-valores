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

        public async Task<IEnumerable<BaseTickerDto>> GetAll()
        {
            var listEntity = await _repository.SelectAllAsync();

            return _mapper.Map<IEnumerable<BaseTickerDto>>(listEntity);
        }

        public async Task<IEnumerable<BaseTickerDto>> GetAllBySegment(int segment){
            var listEntity = await _repository.GetAllBySegment(segment);

            return _mapper.Map<IEnumerable<BaseTickerDto>>(listEntity);
        }

        public async Task<IEnumerable<BaseTickerDto>> GetAllBySubSector(int subSector){
            var listEntity = await _repository.GetAllBySubSector(subSector);

            return _mapper.Map<IEnumerable<BaseTickerDto>>(listEntity);
        }

        public async Task<IEnumerable<BaseTickerDto>> GetAllBySector(int sector){
            var listEntity = await _repository.GetAllBySector(sector);

            return _mapper.Map<IEnumerable<BaseTickerDto>>(listEntity);
        }
    }
}
