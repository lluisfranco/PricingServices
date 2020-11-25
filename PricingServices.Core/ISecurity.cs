namespace PricingServices.Core
{
    public interface ISecurity
    {
        string Type { get; set; }

        string IdentifierType { get; set; }

        string IdentifierValue { get; set; }
        //string OriginalIdentifierValue { get; set; }
    }
}
