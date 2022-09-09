using App.Domain.Dtos.Segment;
using App.Domain.Entities;
using App.Domain.Interfaces.Services.Segment;
using App.Domain.Models;
using App.Domain.Repository;
using App.Service.Services.Exceptions;
using AutoMapper;

namespace App.Service.Services
{
    public class SegmentService : ISegmentService
    {
        private ISegmentRepository _repository;
        private IMapper _mapper;

        public SegmentService(ISegmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SegmentDto>> GetAll()
        {
            var listEntity = await _repository.SelectAllAsync();

            return _mapper.Map<IEnumerable<SegmentDto>>(listEntity);
        }

        public async Task<IEnumerable<SegmentDtoComplete>> GetAllComplete()
        {
            var listEntity = await _repository.GetAllComplete();

            return _mapper.Map<IEnumerable<SegmentDtoComplete>>(listEntity);
        }

        public async Task<SegmentDto> GetById(int id)
        {
            var entity = await _repository.SelectAsync(id);

            return _mapper.Map<SegmentDto>(entity);
        }

        public async Task<SegmentDtoComplete> GetByIdComplete(int id)
        {
            var entity = await _repository.GetByIdComplete(id);

            return _mapper.Map<SegmentDtoComplete>(entity);
        }

        public async Task<IEnumerable<SegmentDto>> GetBySectorId(int sectorId)
        {
            var listEntity = await _repository.GetBySectorId(sectorId);

            return _mapper.Map<IEnumerable<SegmentDto>>(listEntity);
        }

        public async Task<IEnumerable<SegmentDto>> GetBySubSectorId(int subSectorId)
        {
            var listEntity = await _repository.GetBySubSectorId(subSectorId);

            return _mapper.Map<IEnumerable<SegmentDto>>(listEntity);
        }

        public async Task<SegmentDtoCreateResult> Insert(SegmentDtoCreate segment)
        {
            var existSegment = await _repository.ExistSegment(segment.Nome, segment.SubSetorId);

            if (existSegment != null)
                throw new IntegrityException("O Segmento já está cadastrado.");

            var model = _mapper.Map<SegmentModel>(segment);
            var entity = _mapper.Map<SegmentEntity>(model);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<SegmentDtoCreateResult>(result);
        }

        public async Task<SegmentDtoUpdateResult> Update(SegmentDtoUpdate segment)
        {
            var existSegment = await _repository.ExistSegment(segment.Nome, segment.SubSetorId);

            if (existSegment != null &&
                existSegment.Nome.Equals(segment.Nome) &&
                !existSegment.Id.Equals(segment.Id))
            {
                throw new IntegrityException("O Segmento já está cadastrado.");
            }

            var model = _mapper.Map<SegmentModel>(segment);
            var entity = _mapper.Map<SegmentEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<SegmentDtoUpdateResult>(result);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
