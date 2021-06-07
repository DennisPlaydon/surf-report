using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SurfReport.Models
{
    public class MetServiceSurfReportResponse
    {
        [JsonPropertyName("layout")]
        public Layout Layout { get; set; }
        
        [JsonPropertyName("searchLabel")]
        public string SearchLabel { get; set; }
    }

    public class Layout
    {
        [JsonPropertyName("primary")]
        public Primary Primary { get; set; }
    }

    public class Primary
    {
        [JsonPropertyName("slots")]
        public Slots Slots { get; set; }
    }

    public class Slots
    {
        [JsonPropertyName("main")]
        public Main Main { get; set; }
    }

    public class Main
    {
        [JsonPropertyName("modules")]
        public List<Module> Modules { get; set; }
    }

    public class Module
    {
        [JsonPropertyName("days")]
        public List<Day> Days { get; set; }
    }

    public class Day
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        
        [JsonPropertyName("rows")]
        public List<Row> Rows { get; set; }
    }

    public class Row
    {
        [JsonPropertyName("data")]
        public List<Data> Data { get; set; }
        
        [JsonPropertyName("label")]
        public string Label { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("setFace")]
        public string SetFace { get; set; }
    }
}