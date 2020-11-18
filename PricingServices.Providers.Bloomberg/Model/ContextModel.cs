using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class ContextModel
    {
        [JsonPropertyNameAttribute("@vocab")]
        public string Vocab { get; set; }
    }

}
