using App.Domain.Dtos.HistoryTicker;
using App.Domain.Entities;
using App.Domain.Interfaces.Services.HistoryTicker;
using App.Domain.Models;
using App.Domain.Repository;
using AutoMapper;

namespace App.Service.Services
{
    public class HistoryTickerService : IHistoryTickerService
    {       
        private IHistoryTickerRepository _repository;
        private readonly IMapper _mapper;

        public HistoryTickerService(IHistoryTickerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HistoryTickerDto>> GetAllHistoryTicker()
        {
            var listEntity = await _repository.SelectAllAsync();

            return _mapper.Map<IEnumerable<HistoryTickerDto>>(listEntity);
        }

        public async Task<HistoryTickerDtoCreateResult> InsertHistoryTicker(HistoryTickerDtoCreate historyTicker)
        {
            var model = _mapper.Map<HistoryTickerModel>(historyTicker);
            var entity = _mapper.Map<HistoryTickerEntity>(model);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<HistoryTickerDtoCreateResult>(result);
        }

        public async Task<bool> DeleteHistoryTickerByFileImport(int fileImportId)
        {
            return await _repository.DeleteByFileImport(fileImportId);
        }

        public async Task<bool> DeleteHistoryTickertById(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}