using System;
using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class CatalogModel
    {
        [JsonPropertyNameAttribute("@type")]
        public string Type { get; set; }

        [JsonPropertyNameAttribute("@id")]
        public Uri Id { get; set; }

        [JsonPropertyNameAttribute("identifier")]
        public string Identifier { get; set; }
    }

}
