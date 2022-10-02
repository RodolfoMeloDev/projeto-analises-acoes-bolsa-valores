using System.Reflection;
using System.Linq;
using App.Data.Context;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using App.Domain.Models.Seed;
using System.Transactions;
using App.Domain.Entities;
using System;

namespace App.Data.Seed
{
    public class DatasInitial
    {
        private static int IncludeSector(StockAnalysisContext context, FileInitial line)
        {
            var _sector =  context.Sectors.Where(obj => obj.Name.Equals(line.Sector.ToUpper()))
                                      .FirstOrDefault();
            var _sectorId = (_sector == null ? 0 : _sector.Id);

            if (_sector == null)
            {                            
                context.Sectors.Add(new SectorEntity {
                    Name = line.Sector.ToUpper(),
                    Active = true,
                    DateCreated = DateTime.UtcNow
                });

                context.SaveChanges();

                _sectorId = context.Sectors.Where(obj => obj.Name.Equals(line.Sector.ToUpper()))
                                           .FirstOrDefault().Id;
            }
            
            return _sectorId;
        }

        private static int IncludeSubSector(StockAnalysisContext context, FileInitial line, int sectorId)
        {
            var _subSector = context.SubSectors.Where(obj => obj.Name.Equals(line.SubSector.ToUpper()))
                                               .FirstOrDefault();
            var _subSectorId = (_subSector == null ? 0 : _subSector.Id);

            if (_subSector == null)
            {
                context.SubSectors.Add(new SubSectorEntity{
                    Name = line.SubSector.ToUpper(),
                    SectorId = sectorId,
                    Active = true,
                    DateCreated = DateTime.UtcNow
                });

                context.SaveChanges();

                _subSectorId = context.SubSectors.Where(obj => obj.Name.Equals(line.SubSector.ToUpper()))
                                                 .FirstOrDefault().Id;
            }

            return _subSectorId;
        }

        private static int IncludeSegments(StockAnalysisContext context, FileInitial line, int subSectorId)
        {
            var _segments = context.Segments.Where(obj => obj.Name.Equals(line.Segments.ToUpper()))
                                            .FirstOrDefault();

            var _segmentsId = (_segments == null ? 0: _segments.Id);

            if (_segments == null)
            {
                context.Segments.Add(new SegmentEntity{
                    Name = line.Segments.ToUpper(),
                    SubSectorId = subSectorId,
                    Active = true,
                    DateCreated = DateTime.UtcNow
                });

                context.SaveChanges();

                _segmentsId = context.Segments.Where(obj => obj.Name.Equals(line.Segments.ToUpper()))
                                              .FirstOrDefault().Id;
            }

            return _segmentsId;
        }

        private static void IncludeBaseTicker(StockAnalysisContext context, FileInitial line, int segmentsId)
        {
            context.BaseTickers.Add(
                new BaseTickerEntity{
                    BaseTicker = line.BaseTicker.ToUpper(),
                    Company = line.Company.ToUpper(),
                    SegmentId = segmentsId,
                    Active = true,
                    DateCreated = DateTime.UtcNow
                }
            );

            context.SaveChanges();
        }

        public static void InsertDatasInitial(StockAnalysisContext context)
        {
            var _file = Assembly.GetExecutingAssembly().GetManifestResourceStream("App.Data.Seed.StockList_B3.csv");

            var _config = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim,
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };

            using (var reader = new StreamReader(_file, Encoding.Latin1))
            using (var csv = new CsvReader(reader, _config))
            {
                var fileInitialRecords = csv.GetRecords<FileInitial>().ToList();

                using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                                        new TransactionOptions
                                                        {
                                                            IsolationLevel = IsolationLevel.ReadCommitted
                                                        }
                                                       ))
                {

                    string _lastSector = string.Empty;
                    string _lastSubSector = string.Empty;
                    string _lastSegments = string.Empty;

                    int _sectorId = 0;
                    int _subSectorId = 0;
                    int _segmentsId = 0;

                    foreach (var line in fileInitialRecords)
                    {
                        if (!_lastSector.Equals(line.Sector))
                        {
                            _sectorId = IncludeSector(context, line);
                        }                                            

                        if (!_lastSubSector.Equals(line.SubSector))
                        {
                            _subSectorId = IncludeSubSector(context, line, _sectorId);                            
                        }

                        if (!_lastSegments.Equals(line.Segments))
                        {
                            _segmentsId = IncludeSegments(context, line, _subSectorId);
                        }

                        IncludeBaseTicker(context, line, _segmentsId);

                        _lastSector = line.Sector;
                        _lastSubSector = line.SubSector;
                        _lastSegments = line.Segments;
                    }

                    scope.Complete();
                }
            }            
        }
    }
}