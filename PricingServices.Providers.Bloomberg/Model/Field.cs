using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class Field
    {
        [JsonPropertyNameAttribute("cleanName")]
        public string CleanName { get; set; }
    }

}
