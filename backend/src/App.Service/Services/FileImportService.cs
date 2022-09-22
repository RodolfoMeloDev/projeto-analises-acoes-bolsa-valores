using App.Domain.Dtos.FileImport;
using App.Domain.Entities;
using App.Domain.Enums;
using App.Domain.Interfaces.Services.FileImport;
using App.Domain.Models;
using App.Domain.Repository;
using App.Service.Services.Exceptions;
using AutoMapper;

namespace App.Service.Services
{
    public class FileImportService : IFileImportService
    {
        private IFileImportRepository _repository;
        private ITickerRepository _tickerRepository;
        private readonly IMapper _mapper;

        public FileImportService(IFileImportRepository repository, ITickerRepository tickerRepository, IMapper mapper)
        {
            _repository = repository;
            _tickerRepository = tickerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FileImportDto>> GetAllFileImport()
        {
            var listEntity = await _repository.SelectAllAsync();

            return _mapper.Map<IEnumerable<FileImportDto>>(listEntity);
        }

        public async Task<FileImportDto> GetFileImportById(int id)
        {
            var entity = await _repository.SelectAsync(id);

            return _mapper.Map<FileImportDto>(entity);
        }

        public async Task<FileImportDto> GetFileImportByDate(DateTime date)
        {
            var entity = await _repository.GetByDate(date);

            return _mapper.Map<FileImportDto>(entity);
        }

        public async Task<IEnumerable<FileImportDto>> GetFileImportPerPeriod(DateTime dateInitial, DateTime dateFinal)
        {
            var listEntity = await _repository.GetPerPeriod(dateInitial, dateFinal);

            return _mapper.Map<IEnumerable<FileImportDto>>(listEntity);
        }

        public async Task<FileImportDtoCreateResult> InsertFileImport(FileImportDtoCreate fileImport)
        {
            if (fileImport.DataArquivo.Kind != DateTimeKind.Utc)
                fileImport.DataArquivo = TimeZoneInfo.ConvertTimeToUtc(fileImport.DataArquivo);

            var existFileImport = await _repository.GetByDate(fileImport.DataArquivo);

            if (existFileImport == null)
            {
                var model = _mapper.Map<FileImportModel>(fileImport);
                model.NomeArquivo = fileImport.File.FileName;

                var entity = _mapper.Map<FileImportEntity>(model);
                var result = await _repository.InsertAsync(entity);

                if (result != null)
                {
                    if (fileImport.TipoImportacao == TypeFileImport.STATUS_INVEST)
                    {
                        var fileUpload = new FileUploadStatusInvestService(fileImport.File, fileImport.UsuarioId.ToString());
                        var linesFiles = fileUpload.GetLinesFile();
                    }
                    else
                    {
                        var fileUpload = new FileUploadFundamentusService(fileImport.File, fileImport.UsuarioId.ToString());
                        var linesFiles = fileUpload.GetLinesFile();
                    }

                    var ticker = new TickerEntity();

                    //_tickerRepository.InsertAsync()
                }

                return _mapper.Map<FileImportDtoCreateResult>(result);
            }
            else
                throw new IntegrityException("Já existe uma importação para está Data");
        }

        public async Task<bool> DeleteFileImport(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
