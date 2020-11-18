using Microsoft.Extensions.DependencyInjection;
using PricingServices.Core;
using PricingServices.Providers.Bloomberg;

namespace PricingServices.Factory
{
    public static class Builder
    {
        public static IPricingAPIService GetPricingService()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IPricingAPIService, BloombergPricingAPIService>()
                .BuildServiceProvider();
            return serviceProvider.GetService<IPricingAPIService>();
        }
    }
}
