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

        public async Task<bool> InsertListTickers(IEnumerable<FileFundamentus> lines, IEnumerable<DataTickerModel> listTickerWeb)
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

        public async Task<bool> InsertHistoryTickers(IEnumerable<FileFundamentus> lines, int fileImportId)
        {
            var tickers = await _tickerService.GetAllComplete();
            var _priceRemove = lines.Where(obj => obj.Preco.Equals(0));

            lines = lines.Except(_priceRemove)
                            .ToList();

            foreach (var line in lines)
            {
                bool success;
                decimal value = 0;
                HistoryTickerDtoCreate historyTicker = new HistoryTickerDtoCreate();

                historyTicker.ArquivoImportacaoId = fileImportId;
                historyTicker.TickerId = tickers.Where(t => t.Ticker.Equals(line.Ticker))
                                                .Select(x => x.Id)
                                                .FirstOrDefault();
                historyTicker.PrecoUnitario = line.Preco;

                historyTicker.PrecoLucro = Convert.ToDecimal(line.PrecoLucro);
                historyTicker.EvEbit = Convert.ToDecimal(line.EvEbit);
                historyTicker.PrecoValorPatrimonial = line.PrecoValorPatrimonial;
                historyTicker.LiquidezMediaDiaria = line.LiquidezMediaDiaria;
                historyTicker.ValorMercado = line.ValorMercado;

                success = decimal.TryParse(line.Roic, out value);
                historyTicker.Roic = (success ? value : 0);

                success = decimal.TryParse(line.MargemEbit, out value);
                historyTicker.MargemEbit = (success ? value : 0);

                success = decimal.TryParse(line.DividendYeild, out value);
                historyTicker.DividendYield = (success ? value : 0);

                success = decimal.TryParse(line.Roe, out value);
                historyTicker.Roe = (success ? value : 0);

                success = decimal.TryParse(line.CAGRLucro, out value);
                historyTicker.CAGRLucro = (success ? (value == 0 ? null : value) : null);

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
