using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.Azure.Mobile.Server;


namespace COVIDAPI
{
    public partial class CovidIndia : EntityData
    {
        [JsonProperty("cases_time_series")]
        public List<CasesTimeSery> CasesTimeSeries { get; set; }

        [JsonProperty("statewise")]
        public List<Statewise> Statewise { get; set; }

        [JsonProperty("tested")]
        public List<Tested> Tested { get; set; }
    }

    public partial class CasesTimeSery : EntityData
    {
        [JsonProperty("dailyconfirmed")]
        public string Dailyconfirmed { get; set; }

        [JsonProperty("dailydeceased")]
        public string Dailydeceased { get; set; }

        [JsonProperty("dailyrecovered")]
        public string Dailyrecovered { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("totalconfirmed")]
        public string Totalconfirmed { get; set; }

        [JsonProperty("totaldeceased")]
        public string Totaldeceased { get; set; }

        [JsonProperty("totalrecovered")]
        public string Totalrecovered { get; set; }
    }

    public partial class Statewise : EntityData
    {
        [JsonProperty("active")]
        public string Active { get; set; }

        [JsonProperty("confirmed")]
        public string Confirmed { get; set; }

        [JsonProperty("deaths")]
        public string Deaths { get; set; }

        [JsonProperty("deltaconfirmed")]
        public string Deltaconfirmed { get; set; }

        [JsonProperty("deltadeaths")]
        public string Deltadeaths { get; set; }

        [JsonProperty("deltarecovered")]
        public string Deltarecovered { get; set; }

        [JsonProperty("lastupdatedtime")]
        public string Lastupdatedtime { get; set; }

        [JsonProperty("recovered")]
        public string Recovered { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("statecode")]
        public string Statecode { get; set; }

        [JsonProperty("statenotes")]
        public string Statenotes { get; set; }
    }

    public partial class Tested : EntityData
    {
        [JsonProperty("positivecasesfromsamplesreported")]
        public string Positivecasesfromsamplesreported { get; set; }

        [JsonProperty("samplereportedtoday")]
        public string Samplereportedtoday { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("testsconductedbyprivatelabs")]
        public string Testsconductedbyprivatelabs { get; set; }

        [JsonProperty("totalindividualstested")]
        public string Totalindividualstested { get; set; }

        [JsonProperty("totalpositivecases")]
        public string Totalpositivecases { get; set; }

        [JsonProperty("totalsamplestested")]
        public string Totalsamplestested { get; set; }

        [JsonProperty("updatetimestamp")]
        public string Updatetimestamp { get; set; }
    }

    public partial class CovidIndia : EntityData
    {
        public static CovidIndia FromJson(string json) => JsonConvert.DeserializeObject<CovidIndia>(json, COVIDAPI.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CovidIndia self) => JsonConvert.SerializeObject(self, COVIDAPI.Converter.Settings);
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
