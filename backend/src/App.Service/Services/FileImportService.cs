using App.Domain.Dtos.FileImport;
using App.Domain.Dtos.Ticker;
using App.Domain.Entities;
using App.Domain.Enums;
using App.Domain.Interfaces.Services.DataTicker;
using App.Domain.Interfaces.Services.FileImport;
using App.Domain.Interfaces.Services.Segment;
using App.Domain.Interfaces.Services.Ticker;
using App.Domain.Models;
using App.Domain.Models.DataTicker;
using App.Domain.Models.FilesImport;
using App.Domain.Repository;
using App.Service.Services.Exceptions;
using AutoMapper;

namespace App.Service.Services
{
    public class FileImportService : IFileImportService
    {
        private IFileImportRepository _repository;
        private ITickerService _tickerService;
        private IDataTickerService _dataTickerService;
        private ISegmentService _segmentService;
        private readonly IMapper _mapper;

        public FileImportService(IFileImportRepository repository, ITickerService tickerService, IDataTickerService dataTickerService,
            ISegmentService segmentService, IMapper mapper)
        {
            _repository = repository;
            _tickerService = tickerService;
            _dataTickerService = dataTickerService;
            _segmentService = segmentService;
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

                        await InsertHistoryTicker(linesFiles);
                    }
                    else
                    {
                        var fileUpload = new FileUploadFundamentusService(fileImport.File, fileImport.UsuarioId.ToString());
                        var linesFiles = fileUpload.GetLinesFile();

                        await InsertHistoryTicker(linesFiles);
                    }
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

        private async Task<bool> InsertHistoryTicker(IEnumerable<FileStatusInvest> lines)
        {
            var listTickerWebData = await _dataTickerService.GetDataAllTicker();

            await _dataTickerService.ImportSegmentsSubSectorsAndSectors(listTickerWebData);

            var listSegment = await _segmentService.GetAllComplete();

            foreach (var line in lines)
            {
                var ticker = await _tickerService.GetByTicker(line.Ticker);

                if (ticker == null)
                {
                    TickerDtoCreate tickerCreate = new TickerDtoCreate();
                    tickerCreate.Ticker = line.Ticker;
                    tickerCreate.Tipo = TypeTicker.ACAO;
                    tickerCreate.RecuperacaoJudicial = false;

                    DataTickerModel? tickerWebData = listTickerWebData.Where(t => t.cd_acao.Contains(line.Ticker)).FirstOrDefault();

                    if (tickerWebData != null)
                    {
                        var segment = listSegment.Where(sg => sg.Nome.Equals(tickerWebData.segmento) &&
                                                        sg.SubSetor.Nome.Equals(tickerWebData.subsetor) &&
                                                        sg.SubSetor.Setor.Nome.Equals(tickerWebData.setor_economico))
                                                 .FirstOrDefault();

                        tickerCreate.BaseTicker = tickerWebData.cd_acao_rdz;
                        tickerCreate.Empresa = tickerWebData.nm_empresa;
                        tickerCreate.CNPJ = tickerWebData.tx_cnpj;
                        tickerCreate.SegmentoId = (segment != null ? segment.Id : null);
                    }

                    await _tickerService.Insert(tickerCreate);
                }
            }

            return true;
        }

        private async Task<bool> InsertHistoryTicker(IEnumerable<FileFundamentus> lines)
        {
            var listTickerWebData = await _dataTickerService.GetDataAllTicker();

            await _dataTickerService.ImportSegmentsSubSectorsAndSectors(listTickerWebData);

            var listSegment = await _segmentService.GetAllComplete();

            foreach (var line in lines)
            {
                var ticker = await _tickerService.GetByTicker(line.Ticker);

                if (ticker == null)
                {
                    TickerDtoCreate tickerCreate = new TickerDtoCreate();
                    tickerCreate.Ticker = line.Ticker;
                    tickerCreate.Tipo = TypeTicker.ACAO;
                    tickerCreate.RecuperacaoJudicial = false;

                    DataTickerModel? tickerWebData = listTickerWebData.Where(t => t.cd_acao.Contains(line.Ticker)).FirstOrDefault();

                    if (tickerWebData != null)
                    {
                        var segment = listSegment.Where(sg => sg.Nome.Equals(tickerWebData.segmento) &&
                                                        sg.SubSetor.Nome.Equals(tickerWebData.subsetor) &&
                                                        sg.SubSetor.Setor.Nome.Equals(tickerWebData.setor_economico))
                                                 .FirstOrDefault();

                        tickerCreate.BaseTicker = tickerWebData.cd_acao_rdz;
                        tickerCreate.Empresa = tickerWebData.nm_empresa;
                        tickerCreate.CNPJ = tickerWebData.tx_cnpj;
                        tickerCreate.SegmentoId = (segment != null ? segment.Id : null);
                    }

                    await _tickerService.Insert(tickerCreate);
                }
            }

            return true;
        }
    }
}
