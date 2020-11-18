using System;
using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class DistributionModel
    {
        [JsonPropertyNameAttribute("@type")]
        public string Type { get; set; }

        [JsonPropertyNameAttribute("@id")]
        public Uri Id { get; set; }

        [JsonPropertyNameAttribute("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyNameAttribute("contentType")]
        public string ContentType { get; set; }

        [JsonPropertyNameAttribute("digest")]
        public DigestModel Digest { get; set; }

        [JsonPropertyNameAttribute("snapshot")]
        public SnapshotModel Snapshot { get; set; }
    }

}
