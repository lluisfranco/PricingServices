using PricingServices.Core;
using PricingServices.Factory;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PricingServices.Core.SecurityInfo;

namespace PricingServices.Client.Console
{
    class Program
    {
        static async Task Main()
        {
            var bloombergService = Builder.GetPricingService().
                SetCredentials(new ServiceCredentials()
                {
                    ClientId = "1e6e5c12b273505793bff2f9df21107f",
                    ClientSecret = "a1cd3e8a0453cee37635f60f16d2a95a3fe1c807b0dcaf07bdc2ee057638c621"
                }).
                SetSecuritiesList(new List<SecurityInfo> {
                    new SecurityInfo() { Name = "AAPL US Equity" },
                    new SecurityInfo() { Name = "AS5533318 Corp" },
                    new SecurityInfo() { Name = "EI5630724 Govt" },
                    new SecurityInfo() { Name = "CAC Index" },
                    new SecurityInfo() { Name = "EUR", Type = SecurityInfoTypeEnum.Currency },
                    new SecurityInfo() { Name = "USD" , Type = SecurityInfoTypeEnum.Currency},
                    new SecurityInfo() { Name = "GBP" , Type = SecurityInfoTypeEnum.Currency},
                    new SecurityInfo() { Name = "AMZN US Equity" },
                    new SecurityInfo() { Name = "GOOG US Equity" },
                    new SecurityInfo() { Name = "GOOGL US Equity" },
                }).
                SetFieldsList(new List<string>
                {
                    "pxLast",
                    "name",
                    "crncy",
                    "cntryOfIncorporation",
                    "industrySector",
                    "mifidIiComplexInstrIndicator",
                    "legalEntityIdentifier",
                    "leiUltimateParentCompany"
                }).
                InitializeSession();
            var response = await bloombergService.RequestDataAsync();
            Dump(response);
        }

        private static void Dump(IServiceResponse data)
        {
            System.Console.WriteLine(
                $"RESPONSE: {data.RequestId} ({data.RequestDateTime}) Elapsed = {data.ElapsedTime}");
            foreach (var security in data.SecuritiesValues)
            {
                System.Console.WriteLine(
                    $"  SECURITY: {security.SecurityName} - (ERROR = {security.ErrorCode})");
                foreach (var value in security.FieldValues)
                {
                    System.Console.WriteLine(
                        $"    FIELD: {value.Name} = {value.Value}");
                }
            }
        }
    }
}
