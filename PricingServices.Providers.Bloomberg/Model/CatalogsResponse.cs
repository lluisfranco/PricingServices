using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class CatalogsResponse
    {
        [JsonPropertyNameAttribute("contains")]
        public Catalog[] Contains { get; set; }
    }

}
