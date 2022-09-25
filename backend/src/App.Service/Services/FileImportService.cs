using System.Transactions;
using App.Domain.Dtos.FileImport;
using App.Domain.Entities;
using App.Domain.Enums;
using App.Domain.Interfaces.Services.DataTicker;
using App.Domain.Interfaces.Services.FileImport;
using App.Domain.Models;
using App.Domain.Models.FilesImport;
using App.Domain.Repository;
using App.Service.Services.Exceptions;
using AutoMapper;

namespace App.Service.Services
{
    public class FileImportService : IFileImportService
    {
        private IFileImportRepository _repository;
        private IDataTickerService _dataTickerService;
        private IFilesService<FileStatusInvest> _statusInvestService;
        private IFilesService<FileFundamentus> _fundamentusService;
        private readonly IMapper _mapper;

        public FileImportService(IFileImportRepository repository, IDataTickerService dataTickerService,
            IFilesService<FileStatusInvest> fileStatusInvestService, IFilesService<FileFundamentus> fileFundamentusService,
            IMapper mapper)
        {
            _repository = repository;
            _dataTickerService = dataTickerService;
            _statusInvestService = fileStatusInvestService;
            _fundamentusService = fileFundamentusService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FileImportDto>> GetAllFileImport(int UsuarioId)
        {
            var listEntity = await _repository.GetAllFileImport(UsuarioId);

            return _mapper.Map<IEnumerable<FileImportDto>>(listEntity);
        }

        public async Task<FileImportDto> GetFileImportById(int id)
        {
            var entity = await _repository.SelectAsync(id);

            return _mapper.Map<FileImportDto>(entity);
        }

        public async Task<FileImportDto> GetFileImportByDate(int UsuarioId, DateTime date)
        {
            if (date.Kind != DateTimeKind.Utc)
                date = TimeZoneInfo.ConvertTimeToUtc(date);

            var entity = await _repository.GetByDate(UsuarioId, date);

            return _mapper.Map<FileImportDto>(entity);
        }

        public async Task<FileImportDtoCreateResult> InsertFileImport(FileImportDtoCreate fileImport)
        {
            if (fileImport.DataArquivo.Kind != DateTimeKind.Utc)
                fileImport.DataArquivo = TimeZoneInfo.ConvertTimeToUtc(fileImport.DataArquivo);

            var existFileImport = await _repository.GetByDate(fileImport.UsuarioId, fileImport.DataArquivo);

            if (existFileImport == null)
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                                        new TransactionOptions { 
                                                            IsolationLevel = IsolationLevel.ReadCommitted
                                                            },
                                                        TransactionScopeAsyncFlowOption.Enabled
                                                       ))
                {
                    var model = _mapper.Map<FileImportModel>(fileImport);
                    model.NomeArquivo = fileImport.File.FileName;

                    var entity = _mapper.Map<FileImportEntity>(model);
                    var result = await _repository.InsertAsync(entity);

                    if (result != null)
                    {
                        // get date api
                        var listTickerWebData = await _dataTickerService.GetDataAllTicker();
                        // relation ticker with sector/subsector and segments
                        await _dataTickerService.ImportSegmentsSubSectorsAndSectors(listTickerWebData);
                            
                        if (fileImport.TipoImportacao == TypeFileImport.STATUS_INVEST)
                        {
                            var linesFiles = _statusInvestService.GetLinesFile(fileImport.File, fileImport.UsuarioId.ToString());

                            if (await _statusInvestService.InsertListTickers(linesFiles, listTickerWebData))
                            {
                                await _statusInvestService.InsertHistoryTickers(linesFiles, result.Id);
                            }
                        }
                        else
                        {
                            var linesFiles = _fundamentusService.GetLinesFile(fileImport.File, fileImport.UsuarioId.ToString());

                            if (await _fundamentusService.InsertListTickers(linesFiles, listTickerWebData))
                            {
                                await _fundamentusService.InsertHistoryTickers(linesFiles, result.Id);
                            }
                        }
                    }

                    scope.Complete();

                    return _mapper.Map<FileImportDtoCreateResult>(result);
                }                
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
