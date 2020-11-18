using PricingServices.Core;
using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class Security : ISecurity
    {
        [JsonPropertyNameAttribute("@type")]
        public string Type { get; set; }

        [JsonPropertyNameAttribute("identifierType")]
        public string IdentifierType { get; set; }

        [JsonPropertyNameAttribute("identifierValue")]
        public string IdentifierValue { get; set; }
    }

}
