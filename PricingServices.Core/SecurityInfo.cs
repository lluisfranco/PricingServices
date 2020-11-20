namespace PricingServices.Core
{
    public class SecurityInfo
    {
        public string Name { get; set; }
        public SecurityIdentifierTypeEnum IdentifierType { get; set; }
        public SecurityInfoTypeEnum Type { get; set; }
        public enum SecurityInfoTypeEnum
        {
            Asset, Currency
        }

        public enum SecurityIdentifierTypeEnum
        {
            TICKER,
            AUSTRIAN,
            BB_COMPANY,
            BB_GLOBAL,
            BB_UNIQUE,
            BELGIUM,
            CATS,
            CEDEL,
            CINS,
            COMMON_NUMBER,
            CUSIP,
            CZECH,
            DUTCH,
            EUROCLEAR,
            FRENCH,
            IRISH,
            ISIN,
            ISRAELI,
            ITALY,
            JAPAN,
            LUXEMBOURG,
            SEDOL,
            SPAIN,
            VALOREN,
            WPK
        }
    }
}
