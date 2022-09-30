using App.Domain.Dtos.Ticker;
using App.Domain.Entities;
using App.Domain.Interfaces.Services.Ticker;
using App.Domain.Models;
using App.Domain.Repository;
using App.Service.Services.Exceptions;
using AutoMapper;

namespace App.Service.Services
{
    public class TickerService : ITickerService
    {
        private ITickerRepository _repository;
        private ISegmentRepository _segmentRepository;
        private readonly IMapper _mapper;

        public TickerService(ITickerRepository repository, ISegmentRepository segmentRepository, IMapper mapper)
        {
            _repository = repository;
            _segmentRepository = segmentRepository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TickerDto>> GetAll()
        {
            var listEntity = await _repository.SelectAllAsync();

            return _mapper.Map<IEnumerable<TickerDto>>(listEntity);
        }

        public async Task<IEnumerable<TickerDtoComplete>> GetAllComplete()
        {
            var listEntity = await _repository.GetAllComplete();

            return _mapper.Map<IEnumerable<TickerDtoComplete>>(listEntity);
        }

        public async Task<TickerDto> GetById(int id)
        {
            var entity = await _repository.SelectAsync(id);

            return _mapper.Map<TickerDto>(entity);
        }

        public async Task<TickerDtoComplete> GetByTicker(string ticker)
        {
            var entity = await _repository.GetByTickerComplete(ticker);

            return _mapper.Map<TickerDtoComplete>(entity);
        }

        public async Task<TickerDtoComplete> GetByIdComplete(int id)
        {
            var entity = await _repository.GetByIdComplete(id);

            return _mapper.Map<TickerDtoComplete>(entity);
        }

        public async Task<IEnumerable<TickerDto>> GetBySectorId(int sectorId)
        {
            var listEntity = await _repository.GetBySectorId(sectorId);

            return _mapper.Map<IEnumerable<TickerDto>>(listEntity);
        }

        public async Task<IEnumerable<TickerDto>> GetBySegmentId(int segmentId)
        {
            var listEntity = await _repository.GetBySegmentoId(segmentId);

            return _mapper.Map<IEnumerable<TickerDto>>(listEntity);
        }

        public async Task<IEnumerable<TickerDto>> GetBySubSectorId(int subSectorId)
        {
            var listEntity = await _repository.GetBySubSectorId(subSectorId);

            return _mapper.Map<IEnumerable<TickerDto>>(listEntity);
        }

        public async Task<TickerDtoCreateResult> Insert(TickerDtoCreate ticker)
        {
            var existTicker = await _repository.ExistTicker(ticker.Ticker);

            if (existTicker != null)
                throw new IntegrityException("O Ticker já está cadastrado.");

            var model = _mapper.Map<TickerModel>(ticker);
            var entity = _mapper.Map<TickerEntity>(model);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<TickerDtoCreateResult>(result);
        }

        public async Task<TickerDtoUpdateResult> Update(TickerDtoUpdate ticker)
        {
            var existTicker = await _repository.ExistTicker(ticker.Ticker);

            if (existTicker != null &&
                ticker.Company.Equals(existTicker.Company) &&
                !ticker.Id.Equals(existTicker.Id))
            {
                throw new IntegrityException("O Ticker já está cadastrado.");
            }

            var model = _mapper.Map<TickerModel>(ticker);
            var entity = _mapper.Map<TickerEntity>(model);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<TickerDtoUpdateResult>(result);
        }
    }
}
