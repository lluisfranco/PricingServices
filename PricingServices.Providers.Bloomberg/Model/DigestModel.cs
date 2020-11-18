using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class DigestModel
    {
        [JsonPropertyNameAttribute("@type")]
        public string Type { get; set; }

        [JsonPropertyNameAttribute("digestValue")]
        public string DigestValue { get; set; }

        [JsonPropertyNameAttribute("digestAlgorithm")]
        public string DigestAlgorithm { get; set; }
    }

}
