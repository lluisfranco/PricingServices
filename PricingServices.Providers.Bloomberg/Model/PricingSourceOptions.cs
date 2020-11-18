using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class PricingSourceOptions
    {
        [JsonPropertyNameAttribute("@type")]
        public string Type { get; set; }

        [JsonPropertyNameAttribute("prefer")]
        public PricingSourcePreference Prefer { get; set; }
    }

}
