using System.Text.Json.Serialization;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class RequestFormatting
    {
        [JsonPropertyNameAttribute("@type")]
        public string Type { get; set; }

        [JsonPropertyNameAttribute("columnHeader")]
        public bool ColumnHeader { get; set; }

        [JsonPropertyNameAttribute("dateFormat")]
        public string DateFormat { get; set; }

        [JsonPropertyNameAttribute("delimiter")]
        public string Delimiter { get; set; }

        [JsonPropertyNameAttribute("fileType")]
        public string FileType { get; set; }

        [JsonPropertyNameAttribute("outputFormat")]
        public string OutputFormat { get; set; }
    }

}
