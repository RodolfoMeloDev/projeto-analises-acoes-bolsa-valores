using System.Transactions;
using App.Domain.Dtos.FileImport;
using App.Domain.Entities;
using App.Domain.Enums;
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
        private IFilesService<FileStatusInvest> _statusInvestService;
        private IFilesService<FileFundamentus> _fundamentusService;
        private readonly IMapper _mapper;

        public FileImportService(IFileImportRepository repository,
               IFilesService<FileStatusInvest> fileStatusInvestService,
               IFilesService<FileFundamentus> fileFundamentusService,
               IMapper mapper)
        {
            _repository = repository;
            _statusInvestService = fileStatusInvestService;
            _fundamentusService = fileFundamentusService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FileImportDto>> GetAll(int UserId)
        {
            var listEntity = await _repository.GetAll(UserId);

            return _mapper.Map<IEnumerable<FileImportDto>>(listEntity);
        }

        public async Task<FileImportDto> GetById(int id)
        {
            var entity = await _repository.SelectAsync(id);

            return _mapper.Map<FileImportDto>(entity);
        }

        public async Task<FileImportDto> GetByDate(int UserId, DateTime date)
        {
            if (date.Kind != DateTimeKind.Utc)
                date = TimeZoneInfo.ConvertTimeToUtc(date);

            var entity = await _repository.GetByDate(UserId, date);

            return _mapper.Map<FileImportDto>(entity);
        }

        public async Task<FileImportDtoCreateResult> Insert(FileImportDtoCreate fileImport)
        {
            if (fileImport.DateFile.Kind != DateTimeKind.Utc)
                fileImport.DateFile = TimeZoneInfo.ConvertTimeToUtc(fileImport.DateFile);

            var existFileImport = await _repository.GetByDate(fileImport.UserId, fileImport.DateFile);

            if (existFileImport == null)
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                                        new TransactionOptions
                                                        {
                                                            IsolationLevel = IsolationLevel.ReadCommitted
                                                        },
                                                        TransactionScopeAsyncFlowOption.Enabled
                                                       ))
                {
                    var model = _mapper.Map<FileImportModel>(fileImport);
                    model.FileName = fileImport.File.FileName;

                    var entity = _mapper.Map<FileImportEntity>(model);
                    var result = await _repository.InsertAsync(entity);

                    if (result != null)
                    {
                        if (fileImport.TypeFile == TypeFileImport.STATUS_INVEST)
                        {
                            var linesFiles = _statusInvestService.GetLines(fileImport.File, fileImport.UserId.ToString());

                            if (await _statusInvestService.InsertListTickers(linesFiles))
                            {
                                await _statusInvestService.InsertHistoryTickers(linesFiles, result.Id);
                            }
                        }
                        else
                        {
                            var linesFiles = _fundamentusService.GetLines(fileImport.File, fileImport.UserId.ToString());

                            if (await _fundamentusService.InsertListTickers(linesFiles))
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

        public async Task<bool> Delete(int id)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                                        new TransactionOptions
                                                        {
                                                            IsolationLevel = IsolationLevel.ReadCommitted
                                                        },
                                                        TransactionScopeAsyncFlowOption.Enabled
                                                       ))
            {
                var fileImport = await _repository.SelectAsync(id);
                bool itensRemoved = false;

                if (fileImport != null)
                {
                    if (fileImport.TypeFile == TypeFileImport.STATUS_INVEST)
                        itensRemoved = await _statusInvestService.DeleteHistoryTickers(id);
                    else
                        itensRemoved = await _fundamentusService.DeleteHistoryTickers(id);

                    if (itensRemoved)
                        itensRemoved = await _repository.DeleteAsync(id);
                }

                scope.Complete();

                return itensRemoved;
            }
        }
    }
}
