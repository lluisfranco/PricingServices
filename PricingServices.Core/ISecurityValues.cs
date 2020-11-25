using System.Collections.Generic;

namespace PricingServices.Core
{

    public interface ISecurityValues
    {
        string SecurityName { get; set; }
        string OriginalSecurityName { get; set; }
        List<IFieldValue> FieldValues { get; set; }
        string ErrorCode { get; set; }
        string RawValue { get; set; }
    }
}
