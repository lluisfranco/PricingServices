using PricingServices.Core;
using PricingServices.Factory;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PricingServices.Client.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Hello Bloomberg API!");
            var bloombergService = Builder.GetPricingService().
                SetCredentials(new ServiceCredentials()
                {
                    ClientId = "1e6e5c12b273505793bff2f9df21107f",
                    ClientSecret = "a1cd3e8a0453cee37635f60f16d2a95a3fe1c807b0dcaf07bdc2ee057638c621",
                    ExpirationDate = 1652364986947
                }).
                SetSecuritiesList(new List<string> {
                    "EUR Curncy",
                    "USD Curncy",
                    "GBP Curncy",
                    "AAPL US Equity",
                    "AS5533318 Corp",
                    "AN4198411 Corp",
                    "EI5630724 Govt",
                    "AD NA Equity",
                    "AMZN US Equity",
                    "GOOG US Equity",
                    "GOOGL US Equity",
                    "MERNEWA LX Equity",
                    "MORGBZH LX Equity",
                    "TAGCI SW Equity"
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
            var data = await bloombergService.RequestDataAsync();
            Dump(data);
        }

        private static void Dump(List<ISecurityValues> data)
        {
            foreach (var sec in data)
            {
                System.Console.WriteLine($"SECURITY: {sec.SecurityName}");
                foreach (var value in sec.FieldValues)
                {
                    System.Console.WriteLine($"    FIELD: {value.Name} = {value.Value}");
                }
            }
        }
    }
}
