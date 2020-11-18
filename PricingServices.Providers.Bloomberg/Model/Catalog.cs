using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class Catalog
    {
        [JsonPropertyNameAttribute("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyNameAttribute("subscriptionType")]
        public string SubscriptionType { get; set; }
    }
}
