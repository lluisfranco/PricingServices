namespace PricingServices.Core
{
    public class SecurityInfo
    {
        public string Name { get; set; }
        public SecurityInfoTypeEnum Type { get; set; }
        public enum SecurityInfoTypeEnum
        {
            Asset, Currency
        }
    }
}
