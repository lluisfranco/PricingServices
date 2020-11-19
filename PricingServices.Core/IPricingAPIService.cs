using System.Collections.Generic;
using System.Threading.Tasks;

namespace PricingServices.Core
{
    public interface IPricingAPIService
    {
        List<SecurityInfo> SecuritiesList { get; }
        List<string> FieldsList { get; }
        IPricingAPIService SetSecuritiesList(List<SecurityInfo> securitiesList);
        IPricingAPIService SetFieldsList(List<string> fieldsList);
        IPricingAPIService SetCredentials(ServiceCredentials serviceCredential);
        IPricingAPIService InitializeSession();
        Task<List<ISecurityValues>> RequestDataAsync();
        List<ISecurityValues> ProcessFile(string responseFileName);
    }
}
