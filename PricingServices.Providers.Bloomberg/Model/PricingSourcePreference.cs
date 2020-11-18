using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class PricingSourcePreference
    {
        [JsonPropertyNameAttribute("mnemonic")]
        public string Mnemonic { get; set; }
    }

}
