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
    public class FileFundamentusService : IFilesService<FileFundamentus>
    {
        private string? _pathFile;

        private ISegmentService _segmentService;
        private ITickerService _tickerService;
        private IHistoryTickerService _historyTickerService;

        public FileFundamentusService(ISegmentService segmentService, ITickerService tickerService, IHistoryTickerService historyTickerService)
        {
            _segmentService = segmentService;
            _tickerService = tickerService;
            _historyTickerService = historyTickerService;
        }

        public IEnumerable<FileFundamentus> GetLinesFile(IFormFile file, string directoryUser)
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
                        return csv.GetRecords<FileFundamentus>().ToList();
                    }
                }
                else
                    throw new FileUploadFundamentusException("Não foi possível definir um local para descarregar o arquivo no servidor.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> InsertListTickers(IEnumerable<FileFundamentus> lines, IEnumerable<DataTickerModel> listTickerWeb)
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
                        tickerCreate.BaseTicker = line.Ticker.Substring(0, 4);
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

        public async Task<bool> InsertHistoryTickers(IEnumerable<FileFundamentus> lines, int fileImportId)
        {
            try
            {
                var tickers = await _tickerService.GetAllComplete();

                foreach (var line in lines)
                {
                    if (line.Preco.Equals(0))
                    {
                        continue;
                    }

                    var historyTicker = new HistoryTickerDtoCreate();

                    bool success;
                    decimal value = 0;

                    historyTicker.ArquivoImportacaoId = fileImportId;
                    historyTicker.TickerId = tickers.Where(t => t.Ticker.Equals(line.Ticker))
                                                    .FirstOrDefault().Id;
                    historyTicker.PrecoUnitario = line.Preco;

                    historyTicker.PrecoLucro = (line.PrecoLucro == null ? 0 : (decimal)line.PrecoLucro);

                    success = decimal.TryParse(line.Roic, out value);
                    historyTicker.Roic = (success ? value : 0);

                    historyTicker.EvEbit = (line.EvEbit == null ? 0 : (decimal)line.EvEbit);

                    success = decimal.TryParse(line.MargemEbit, out value);
                    historyTicker.MargemEbit = (success ? value : 0);

                    success = decimal.TryParse(line.DividendYeild, out value);
                    historyTicker.DividendYield = (success ? value : 0);
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

        public async Task<bool> DeleteHistoryTickers(int fileImportId)
        {
            return await _historyTickerService.DeleteHistoryTickerByFileImport(fileImportId);
        }
    }
}
