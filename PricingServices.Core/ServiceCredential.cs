namespace PricingServices.Core
{
    public class ServiceCredentials
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public long ExpirationDate { get; set; }
    }
}
