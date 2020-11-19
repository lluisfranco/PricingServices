using PricingServices.Core;
using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class Field : IField
    {
        [JsonPropertyNameAttribute("cleanName")]
        public string CleanName { get; set; }
    }
}
