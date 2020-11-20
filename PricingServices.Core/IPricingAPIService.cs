using System.Collections.Generic;
using System.Threading.Tasks;

namespace PricingServices.Core
{
    public interface IPricingAPIService
    {
        List<SecurityInfo> SecuritiesList { get; }
        List<string> FieldsList { get; }
        IPricingAPIService SetCredentials(ServiceCredentials serviceCredential);
        IPricingAPIService SetOptions(ServiceOptions serviceOptions);
        IPricingAPIService SetSecuritiesList(List<SecurityInfo> securitiesList);
        IPricingAPIService SetFieldsList(List<string> fieldsList);
        IPricingAPIService InitializeSession();
        Task<IServiceResponse> RequestDataAsync();
        List<ISecurityValues> ProcessFile(string responseFileName);
    }
}
