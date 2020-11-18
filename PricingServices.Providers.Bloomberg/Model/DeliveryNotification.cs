using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class DeliveryNotification
    {
        [JsonPropertyNameAttribute("@context")]
        public ContextModel Context { get; set; }

        [JsonPropertyNameAttribute("@type")]
        public string Type { get; set; }

        [JsonPropertyNameAttribute("endedAtTime")]
        public string EndedAtTime { get; set; }

        [JsonPropertyNameAttribute("generated")]
        public DistributionModel Generated { get; set; }
    }

}
