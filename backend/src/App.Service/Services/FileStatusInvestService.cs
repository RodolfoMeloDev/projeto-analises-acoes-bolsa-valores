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

        public async Task<bool> InsertListTickers(IEnumerable<FileStatusInvest> lines, IEnumerable<DataTickerModel> listTickerWeb)
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

        public async Task<bool> InsertHistoryTickers(IEnumerable<FileStatusInvest> lines, int fileImportId)
        {
            var tickers = await _tickerService.GetAllComplete();
            var _priceRemove = lines.Where(obj => obj.Preco.Equals(0));
            var _averageDailyLiquidityRemove = lines.Where(obj => obj.LiquidezMediaDiaria == null);

            lines = lines.Except(_priceRemove)
                            .Except(_averageDailyLiquidityRemove)
                            .ToList();

            foreach (var line in lines)
            {
                var historyTicker = new HistoryTickerDtoCreate();

                historyTicker.TickerId = tickers.Where(t => t.Ticker.Equals(line.Ticker))
                                                .Select(t => t.Id)
                                                .FirstOrDefault();
                historyTicker.ArquivoImportacaoId = fileImportId;
                historyTicker.PrecoUnitario = line.Preco;
                historyTicker.PrecoLucro = Convert.ToDecimal(line.PrecoLucro);
                historyTicker.Roic = Convert.ToDecimal(line.Roic);
                historyTicker.EvEbit = Convert.ToDecimal(line.EvEbit);
                historyTicker.MargemEbit = Convert.ToDecimal(line.MargemEbit);
                historyTicker.Lpa = Convert.ToDecimal(line.Lpa);
                historyTicker.Vpa = Convert.ToDecimal(line.Vpa);
                historyTicker.Roe = Convert.ToDecimal(line.Roe);
                historyTicker.CAGRLucro = line.CAGRLucro;
                historyTicker.DividendYield = line.DividendYeild;
                historyTicker.PrecoValorPatrimonial = line.PrecoValorPatrimonial;
                historyTicker.LiquidezMediaDiaria = line.LiquidezMediaDiaria;
                historyTicker.ValorMercado = line.ValorMercado;

                await _historyTickerService.InsertHistoryTicker(historyTicker);
            }

            return true;
        }

        public async Task<bool> DeleteHistoryTickers(int fileImportId)
        {
            return await _historyTickerService.DeleteHistoryTickerByFileImport(fileImportId);
        }
    }
}
