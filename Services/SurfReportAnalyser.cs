using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SurfReport.Models;

namespace SurfReport.Services
{
    public class SurfReportAnalyser
    {
        private readonly MetServiceApi _metServiceApi;
        private readonly double _minIdealWaveHeight;
        private readonly double _maxIdealWaveHeight;

        public SurfReportAnalyser()
        {
            _metServiceApi = new MetServiceApi();
            _minIdealWaveHeight = 1.25;
            _maxIdealWaveHeight = 1.9;
        }

        public async Task<IEnumerable<IndividualSurfReport>> Analyse()
        {
            // West coast beaches
            var omaha = await _metServiceApi.GetSurfReport("great-barrier","omaha");
            var muriwai = await _metServiceApi.GetSurfReport("piha","muriwai-beach");
            var piha = await _metServiceApi.GetSurfReport("piha","piha");
            
            // East coast beaches
            var portWaikato = await _metServiceApi.GetSurfReport("west-auckland","port-waikato");
            var waihi = await _metServiceApi.GetSurfReport("coromandel","waihi-beach");

            var combinedReports = new List<IndividualSurfReport> {omaha, muriwai, piha, portWaikato, waihi};
            var idealReports = combinedReports.Select(FilterReports).Where(x => x.Data.Any());

            return idealReports;
        }

        private IndividualSurfReport FilterReports(IndividualSurfReport report)
        {
            var filteredReport = new IndividualSurfReport
            {
                Beach = report.Beach,
                Data = report.Data.Where(x =>
                    x.WaveHeight > _minIdealWaveHeight & x.WaveHeight < _maxIdealWaveHeight)
            };

            return filteredReport;
        }
    }
}