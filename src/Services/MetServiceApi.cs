using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SurfReport.Models;

namespace SurfReport.Services
{
    public class MetServiceApi
    {
        private readonly HttpClient _httpClient;

        public MetServiceApi()
        {
            _httpClient = new HttpClient
            {
                    BaseAddress = new Uri("https://www.metservice.com/")
            };
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        }
        public async Task<IndividualSurfReport> GetSurfReport(string region, string beach)
        {
            var response = await _httpClient.GetAsync($"publicData/webdata/marine/regions/{region}/surf/locations/{beach}");
            var json = await response.Content.ReadAsStringAsync();
            var metServiceReport = JsonSerializer.Deserialize<MetServiceSurfReportResponse>(json);

            var dailyData = metServiceReport.Layout.Primary.Slots.Main.Modules.First().Days;
            var surfReport = new IndividualSurfReport
            {
                Beach = metServiceReport.SearchLabel,
                Data = dailyData.Select(dayData => new DailySurfData
                {
                    Date = dayData.Date,
                    WaveHeight = dayData.Rows
                        .Single(group => group.Label.Contains("Set face")).Data
                        .Where(wave => wave.SetFace != null)
                        .Average(wave => double.Parse(wave.SetFace))
                })
            };
            return surfReport;
        }
    }
}