namespace PricingServices.Core
{
    public interface IFieldValue
    {
        IField Field { get; set; }
        string Value { get; set; }
    }
}
