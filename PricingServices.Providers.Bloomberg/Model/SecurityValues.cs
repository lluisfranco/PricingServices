using PricingServices.Core;
using System.Collections.Generic;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class SecurityValues : ISecurityValues
    {
        public string SecurityName { get; set; }
        public string OriginalSecurityName { get; set; }
        public List<IFieldValue> FieldValues { get; set; } = new List<IFieldValue>();
        public string ErrorCode { get; set; }
        public string RawValue { get; set; }
    }
}
