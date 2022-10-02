using CsvHelper.Configuration.Attributes;

namespace App.Domain.Models.Seed
{
    public class FileInitial
    {
        [Name("setor")]
        public string Sector { get; set; }

        [Name("SubSetor")]
        public string SubSector { get; set; }

        [Name("seguimento")]
        public string Segments { get; set; }

        [Name("base_ticker")]
        public string BaseTicker { get; set; }

        [Name("empresa")]
        public string Company { get; set; }
    }
}