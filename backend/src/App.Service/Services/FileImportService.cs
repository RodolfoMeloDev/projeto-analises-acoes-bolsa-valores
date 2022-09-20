using App.Domain.Dtos.FileImport;
using App.Domain.Entities;
using App.Domain.Interfaces.Services.FileImport;
using App.Domain.Models;
using App.Domain.Repository;
using App.Service.Services.Exceptions;
using AutoMapper;

namespace App.Service.Services
{
    public class FileImportService : IFileImportService
    {
        private IFileImportRepository _service;
        private readonly IMapper _mapper;

        public FileImportService(IFileImportRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FileImportDto>> GetAllFileImport()
        {
            var listEntity = await _service.SelectAllAsync();

            return _mapper.Map<IEnumerable<FileImportDto>>(listEntity);
        }

        public async Task<FileImportDto> GetFileImportById(int id)
        {
            var entity = await _service.SelectAsync(id);

            return _mapper.Map<FileImportDto>(entity);
        }

        public async Task<FileImportDto> GetFileImportByDate(DateTime date)
        {
            var entity = await _service.GetByDate(date);

            return _mapper.Map<FileImportDto>(entity);
        }

        public async Task<IEnumerable<FileImportDto>> GetFileImportPerPeriod(DateTime dateInitial, DateTime dateFinal)
        {
            var listEntity = await _service.GetPerPeriod(dateInitial, dateFinal);

            return _mapper.Map<IEnumerable<FileImportDto>>(listEntity);
        }

        public async Task<FileImportDtoCreateResult> InsertFileImport(FileImportDtoCreate fileImport)
        {
            var existFileImport = await _service.GetByDate(fileImport.DataArquivo);

            if (existFileImport == null)
            {
                var model = _mapper.Map<FileImportModel>(fileImport);
                var entity = _mapper.Map<FileImportEntity>(model);
                var result = await _service.InsertAsync(entity);
                
                return _mapper.Map<FileImportDtoCreateResult>(result);
            }
            else
            {
                throw new IntegrityException("Já existe uma importação para está Data");
            }
        }

        public async Task<bool> DeleteFileImport(int id)
        {
            return await _service.DeleteAsync(id);
        }        
    }
}