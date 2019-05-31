using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LTF.Services
{
    public partial class DeserializeService
    {
        [JsonProperty("question")]
        public List<string> Question { get; set; }

        [JsonProperty("ans1")]
        public List<string> Ans1 { get; set; }

        [JsonProperty("ans2")]
        public List<string> Ans2 { get; set; }

        [JsonProperty("ans3")]
        public List<string> Ans3 { get; set; }

        [JsonProperty("ans4")]
        public List<string> Ans4 { get; set; }

        [JsonProperty("ans5")]
        public List<string> Ans5 { get; set; }
    }

    public partial class DeserializeService
    {
        public static DeserializeService FromJson(string json) => JsonConvert.DeserializeObject<DeserializeService>(File.ReadAllText(json), Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this DeserializeService self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
