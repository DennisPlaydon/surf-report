using System.Collections.Generic;

namespace SurfReport.Models
{
    public class IndividualSurfReport
    {
        public string Beach { get; set; }
        public IEnumerable<DailySurfData> Data { get; set; }
    }
}