using System;
using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class SnapshotModel
    {
        [JsonPropertyNameAttribute("@type")]
        public string Type { get; set; }

        [JsonPropertyNameAttribute("@id")]
        public Uri Id { get; set; }

        [JsonPropertyNameAttribute("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyNameAttribute("issued")]
        public string Issued { get; set; }

        [JsonPropertyNameAttribute("dataset")]
        public DatasetModel Dataset { get; set; }
    }

}
