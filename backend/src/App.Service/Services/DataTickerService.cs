using System.Net;
using System.Text.Json;
using App.Domain.Dtos.Sector;
using App.Domain.Dtos.Segment;
using App.Domain.Dtos.SubSector;
using App.Domain.Interfaces.Services.DataTicker;
using App.Domain.Interfaces.Services.Sector;
using App.Domain.Interfaces.Services.Segment;
using App.Domain.Interfaces.Services.SubSector;
using App.Domain.Models.DataTicker;

namespace App.Service
{
    public class DataTickerService : IDataTickerService
    {
        private const string END_POINT_ALL_TICKER = "https://api-cotacao-b3.labdo.it/api/empresa";

        private ISectorService _sectorService;
        private ISubSectorService _subSectorService;
        private ISegmentService _segmentService;

        public DataTickerService(ISectorService sectorService, ISubSectorService subSectorService, ISegmentService segmentService)
        {
            _sectorService = sectorService;
            _subSectorService = subSectorService;
            _segmentService = segmentService;
        }

        public async Task<IEnumerable<DataTickerModel>> GetDataAllTicker()
        {
            HttpClient _client = new HttpClient();
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            _client.BaseAddress = new Uri(END_POINT_ALL_TICKER);

            HttpResponseMessage _response = await _client.GetAsync("");

            if (_response.IsSuccessStatusCode)
            {
                string response = await _response.Content.ReadAsStringAsync();

                var jsonDeserialize = JsonSerializer.Deserialize<IEnumerable<DataTickerModel>>(response);

                if (jsonDeserialize != null)
                    return jsonDeserialize;

                throw new HttpRequestException("[{\"ErrorStatusCode\":\"" + (int)HttpStatusCode.InternalServerError + "\", {\"Message\": \"N�o foi poss�vel ler o arquivo informado.\" }]");
            }
            else
            {
                throw new HttpRequestException("[{\"ErrorStatusCode\":\"" + (int)_response.StatusCode + "\", {\"Message\": \"" + _response.ReasonPhrase + "\" }]");
            }
        }

        public async Task<bool> ImportSegmentsSubSectorsAndSectors(IEnumerable<DataTickerModel> tickres)
        {
            var listSector = await _sectorService.GetAllSectors();
            var listSubSector = await _subSectorService.GetAllComplete();
            var listSegment = await _segmentService.GetAllComplete();

            foreach (var ticker in tickres)
            {
                var resultSector = new SectorDtoCreateResult();
                var resultSubSector = new SubSectorDtoCreateResult();

                var sector = listSector.Where(s => s.Name.Equals(ticker.setor_economico))
                                       .FirstOrDefault();

                if (sector == null)
                {
                    var sectorNew = new SectorDtoCreate();
                    sectorNew.Name = ticker.setor_economico;

                    resultSector = await _sectorService.InsertSector(sectorNew);
                    // update list sectors
                    listSector = await _sectorService.GetAllSectors();
                }

                var subSector = listSubSector.Where(ss => ss.Name.Equals(ticker.subsetor) && 
                                                          ss.Sector.Name.Equals(ticker.setor_economico))
                                             .FirstOrDefault();

                if (subSector == null)
                {
                    var subSectorNew = new SubSectorDtoCreate();
                    subSectorNew.Name = ticker.subsetor;
                    subSectorNew.SectorId = (sector != null ? sector.Id : resultSector.Id);

                    resultSubSector = await _subSectorService.Insert(subSectorNew);
                    // update list subsectores
                    listSubSector = await _subSectorService.GetAllComplete();
                }

                var segment = listSegment.Where(sg => sg.Name.Equals(ticker.segmento) &&
                                                      sg.SubSector.Name.Equals(ticker.subsetor) &&
                                                      sg.SubSector.Sector.Name.Equals(ticker.setor_economico))
                                         .FirstOrDefault();

                if (segment == null)
                {
                    var segmentNew = new SegmentDtoCreate();
                    segmentNew.Name = ticker.segmento;
                    segmentNew.SubSectorId = (subSector != null ? subSector.Id : resultSubSector.Id);

                    await _segmentService.Insert(segmentNew);
                    // update list segments
                    listSegment = await _segmentService.GetAllComplete();
                }
            }

            return true;
        }
    }
}
