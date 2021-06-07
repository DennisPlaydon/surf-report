using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SurfReport.Models;

namespace SurfReport.Services
{
    public class PushNotificationService
    {
        private readonly HttpClient _httpClient;

        public PushNotificationService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.pushbullet.com/")
            };
            _httpClient.DefaultRequestHeaders.Add("Access-Token", "REDACTED");
        }
        
        public async Task Push(string title, string pushType, string body)
        {
            var notification = new PushNotification
            {
                Title = title,
                Type = pushType,
                Body = body
            };
            var json = JsonSerializer.Serialize(notification, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("/v2/pushes", content);
        }

        public string FormatReportBody(IEnumerable<IndividualSurfReport> idealSurfReports)
        {
            string body = null;
            foreach (var report in idealSurfReports)
            {
                string dailyEntries = null;
                foreach (var data in report.Data)
                {
                    var dayEntry = $"\n  - {data.Date:ddd dd/MM} ({Math.Round(data.WaveHeight, 2)}m)";
                    dailyEntries += dayEntry;
                }
                var beachData = $"{report.Beach}:{dailyEntries}\n";
                body += beachData;
            }

            return body;
        }
    }
}