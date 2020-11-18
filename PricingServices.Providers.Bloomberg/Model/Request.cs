using System;
using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class Request
    {
        [JsonPropertyNameAttribute("@type")]
        public string Type { get; set; }

        [JsonPropertyNameAttribute("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyNameAttribute("title")]
        public string Title { get; set; }

        [JsonPropertyNameAttribute("description")]
        public string Description { get; set; }

        [JsonPropertyNameAttribute("universe")]
        public Uri Universe { get; set; }

        [JsonPropertyNameAttribute("fieldList")]
        public Uri FieldList { get; set; }

        [JsonPropertyNameAttribute("trigger")]
        public Uri Trigger { get; set; }

        [JsonPropertyNameAttribute("formatting")]
        public RequestFormatting Formatting { get; set; }

        [JsonPropertyNameAttribute("pricingSourceOptions")]
        public PricingSourceOptions PricingSourceOptions { get; set; }
    }

}
