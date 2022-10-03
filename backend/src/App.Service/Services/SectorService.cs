using App.Domain.Dtos.Sector;
using App.Domain.Entities;
using App.Domain.Interfaces.Services.Sector;
using App.Domain.Models;
using App.Domain.Repository;
using App.Service.Services.Exceptions;
using AutoMapper;

namespace App.Service.Services
{
    public class SectorService : ISectorService
    {
        private ISectorRepository _repository;
        private readonly IMapper _mapper;

        public SectorService(ISectorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SectorDto> GetByName(string name)
        {
            var entity = await _repository.GetByName(name);

            return _mapper.Map<SectorDto>(entity);
        }

        public async Task<SectorDto> GetById(int id)
        {
            var entity = await _repository.SelectAsync(id);

            return _mapper.Map<SectorDto>(entity);
        }

        public async Task<IEnumerable<SectorDto>> GetAll()
        {
            var listEntity = await _repository.SelectAllAsync();

            return _mapper.Map<IEnumerable<SectorDto>>(listEntity);
        }

        public async Task<SectorDtoCreateResult> Insert(SectorDtoCreate sector)
        {
            var existSector = await _repository.GetByName(sector.Name);

            if (existSector == null)
            {
                var model = _mapper.Map<SectorModel>(sector);
                var entity = _mapper.Map<SectorEntity>(model);
                var result = await _repository.InsertAsync(entity);

                return _mapper.Map<SectorDtoCreateResult>(result);
            }
            else
            {
                throw new IntegrityException("O Setor informado já está cadastrado");
            }
        }

        public async Task<SectorDtoUpdateResult> Update(SectorDtoUpdate sector)
        {
            var model = _mapper.Map<SectorModel>(sector);
            var entity = _mapper.Map<SectorEntity>(model);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<SectorDtoUpdateResult>(result);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
