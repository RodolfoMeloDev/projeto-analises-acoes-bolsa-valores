using System.Globalization;
using System.Text;
using App.Domain.Dtos.HistoryTicker;
using App.Domain.Dtos.Ticker;
using App.Domain.Enums;
using App.Domain.Interfaces.Services.FileImport;
using App.Domain.Interfaces.Services.HistoryTicker;
using App.Domain.Interfaces.Services.Segment;
using App.Domain.Interfaces.Services.Ticker;
using App.Domain.Models.DataTicker;
using App.Domain.Models.FilesImport;
using App.Service.Services.Exceptions;
using App.Service.Services.Functions;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;

namespace App.Service.Services
{
    public class FileStatusInvestService : IFilesService<FileStatusInvest>
    {
        private string? _pathFile;
        
        private ISegmentService _segmentService;
        private ITickerService _tickerService;
        private IHistoryTickerService _historyTickerService;

        public FileStatusInvestService(ISegmentService segmentService, ITickerService tickerService, IHistoryTickerService historyTickerService)
        {
            _segmentService = segmentService;
            _tickerService = tickerService;
            _historyTickerService = historyTickerService;            
        }

        public IEnumerable<FileStatusInvest> GetLinesFile(IFormFile file, string directoryUser)
        {
            try
            {
                _pathFile = Utils.CopyFileToServer(file, directoryUser);

                if (!String.IsNullOrEmpty(_pathFile))
                {
                    var config = new CsvConfiguration(CultureInfo.CurrentCulture)
                    {
                        HasHeaderRecord = true,
                        TrimOptions = TrimOptions.Trim,
                        PrepareHeaderForMatch = args => args.Header.ToLower(),
                    };

                    using (var reader = new StreamReader(_pathFile, Encoding.Latin1))
                    using (var csv = new CsvReader(reader, config))
                    {
                        return csv.GetRecords<FileStatusInvest>().ToList();
                    }
                }
                else
                    throw new FileUploadStatusInvestException("Não foi possível definir um local para descarregar o arquivo no servidor.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> InsertListTickers(IEnumerable<FileStatusInvest> lines, IEnumerable<DataTickerModel> listTickerWeb)
        {            
            try
            {
                var listSegment = await _segmentService.GetAllComplete();

                foreach (var line in lines)
                {
                    var ticker = await _tickerService.GetByTicker(line.Ticker);

                    if (ticker == null)
                    {
                        DataTickerModel? tickerWebData = listTickerWeb.Where(t => t.cd_acao.Contains(line.Ticker))
                                                                      .FirstOrDefault();

                        TickerDtoCreate tickerCreate = new TickerDtoCreate();
                        
                        tickerCreate.Ticker = line.Ticker;
                        tickerCreate.Tipo = TypeTicker.ACAO;
                        tickerCreate.RecuperacaoJudicial = false;                    

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
            catch (Exception e)
            {                
                throw e;
            }            
        }

        public async Task<bool> InsertHistoryTickers(IEnumerable<FileStatusInvest> lines, int fileImportId)
        {
            try
            {
                var tickers = await _tickerService.GetAllComplete();

                foreach (var line in lines)
                {
                    var historyTicker = new HistoryTickerDtoCreate();

                    historyTicker.TickerId = tickers.Where(t => t.Ticker.Equals(line.Ticker))
                                                    .FirstOrDefault().Id;
                    historyTicker.ArquivoImportacaoId = fileImportId;
                    historyTicker.PrecoUnitario = line.Preco;                    
                    historyTicker.PrecoLucro = (line.PrecoLucro == null ? 0 : (decimal)line.PrecoLucro);
                    historyTicker.Roic = (line.Roic == null ? 0 : (decimal)line.Roic);
                    historyTicker.EvEbit = (line.EvEbit == null ? 0 : (decimal)line.EvEbit);
                    historyTicker.MargemEbit = (line.MargemEbit == null ? 0 : (decimal)line.MargemEbit);
                    historyTicker.DividendYield = line.DividendYeild;
                    historyTicker.PrecoValorPatrimonial = line.PrecoValorPatrimonial;
                    historyTicker.LiquidezMediaDiaria = line.LiquidezMediaDiaria;
                    historyTicker.ValorMercado = line.ValorMercado;

                    await _historyTickerService.InsertHistoryTicker(historyTicker);
                }

                return true;
            }
            catch (Exception e)
            {                
                throw e;
            }
        }
    }
}
