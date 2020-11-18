using System.Collections.Generic;

namespace PricingServices.Core
{
    public interface ISecurityValues
    {
        ISecurity Security { get; set; }
        List<IFieldValue> FieldValues { get; set; }
        string ErrorCode { get; set; }
        string RawValue { get; set; }
    }
}
