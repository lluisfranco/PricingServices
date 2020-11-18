using PricingServices.Core;
using PricingServices.Providers.Bloomberg;

namespace PricingServices.Factory
{
    public static class Builder
    {
        public static IPricingAPIService GetPricingService() => 
            new BloombergPricingAPIService();

    }
}
