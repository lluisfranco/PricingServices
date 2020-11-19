using PricingServices.Core;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class FieldValue : IFieldValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
