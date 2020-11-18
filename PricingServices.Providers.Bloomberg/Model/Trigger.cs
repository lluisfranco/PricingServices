using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class Trigger
    {
        [JsonPropertyNameAttribute("@type")]
        public string Type { get; set; }

        [JsonPropertyNameAttribute("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyNameAttribute("title")]
        public string Title { get; set; }

        [JsonPropertyNameAttribute("description")]
        public string Description { get; set; }

        [JsonPropertyNameAttribute("frequency")]
        public string Frequency { get; set; }

        [JsonPropertyNameAttribute("startDate")]
        public string StartDate { get; set; }

        [JsonPropertyNameAttribute("startTime")]
        public string StartTime { get; set; }
    }

}
