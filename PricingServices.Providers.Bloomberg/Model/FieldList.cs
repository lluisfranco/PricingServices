using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class FieldList
    {
        [JsonPropertyNameAttribute("@type")]
        public string Type { get; set; }

        [JsonPropertyNameAttribute("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyNameAttribute("title")]
        public string Title { get; set; }

        [JsonPropertyNameAttribute("description")]
        public string Description { get; set; }

        [JsonPropertyNameAttribute("contains")]
        public Field[] Contains { get; set; }
    }

}
