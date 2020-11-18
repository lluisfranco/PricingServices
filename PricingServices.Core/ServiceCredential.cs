namespace PricingServices.Core
{
    public class ServiceCredential
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public long ExpirationDate { get; set; }
    }
}
