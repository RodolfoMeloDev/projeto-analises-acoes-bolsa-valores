using App.Domain.Dtos.SubSector;
using App.Domain.Entities;
using App.Domain.Interfaces.Services.SubSector;
using App.Domain.Models;
using App.Domain.Repository;
using App.Service.Services.Exceptions;
using AutoMapper;

namespace App.Service.Services
{
    public class SubSectorService : ISubSectorService
    {
        private ISubSectorRepository _repository;
        private IMapper _mapper;

        public SubSectorService(ISubSectorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubSectorDto>> GetAll()
        {
            var listEntity = await _repository.SelectAllAsync();

            return _mapper.Map<IEnumerable<SubSectorDto>>(listEntity);
        }

        public async Task<IEnumerable<SubSectorDtoComplete>> GetAllComplete()
        {
            var listEntity = await _repository.GetAllComplete();

            return _mapper.Map<IEnumerable<SubSectorDtoComplete>>(listEntity);
        }

        public async Task<SubSectorDtoComplete> GetByIdComplete(int id)
        {
            var entity = await _repository.GetByIdComplete(id);

            return _mapper.Map<SubSectorDtoComplete>(entity);
        }

        public async Task<SubSectorDto> GetById(int id)
        {
            var entity = await _repository.SelectAsync(id);

            return _mapper.Map<SubSectorDto>(entity);
        }

        public async Task<IEnumerable<SubSectorDto>> GetBySectorId(int sectorId)
        {
            var listEntity = await _repository.GetBySectorId(sectorId);

            return _mapper.Map<IEnumerable<SubSectorDto>>(listEntity);
        }

        public async Task<SubSectorDtoCreateResult> Insert(SubSectorDtoCreate subSector)
        {
            var existSubSector = await _repository.ExistSubSector(subSector.Name, subSector.SectorId);

            if (existSubSector != null)
                throw new IntegrityException("O SubSetor já cadastrado.");

            var model = _mapper.Map<SubSectorModel>(subSector);
            var entity = _mapper.Map<SubSectorEntity>(model);
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<SubSectorDtoCreateResult>(result);
        }

        public async Task<SubSectorDtoUpdateResult> Update(SubSectorDtoUpdate subSector)
        {
            var existSubSector = await _repository.ExistSubSector(subSector.Name, subSector.SectorId);

            if (existSubSector != null &&
                existSubSector.Name.Equals(subSector.Name) &&
                !existSubSector.Id.Equals(subSector.Id))
            {
                throw new IntegrityException("O SubSetor já cadastrado.");
            }

            var model = _mapper.Map<SubSectorModel>(subSector);
            var entity = _mapper.Map<SubSectorEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<SubSectorDtoUpdateResult>(result);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
