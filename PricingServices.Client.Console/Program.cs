using PricingServices.Core;
using PricingServices.Factory;
using System.Collections.Generic;
using System.Threading.Tasks;
using static PricingServices.Core.SecurityInfo;

namespace PricingServices.Client.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var outputFilePath = System.IO.Path.Combine("Tempfiles", $"myReq20201119115321.bbg");
            var data = Builder.GetPricingService().ProcessFile(outputFilePath);
            Dump(data);

            //System.Console.WriteLine("Hello Bloomberg API!");
            //var bloombergService = Builder.GetPricingService().
            //    SetCredentials(new ServiceCredentials()
            //    {
            //        ClientId = "1e6e5c12b273505793bff2f9df21107f",
            //        ClientSecret = "a1cd3e8a0453cee37635f60f16d2a95a3fe1c807b0dcaf07bdc2ee057638c621",
            //        ExpirationDate = 1652364986947
            //    }).
            //    SetSecuritiesList(new List<SecurityInfo> {
            //        new SecurityInfo() { Name = "CAC Index" },
            //        new SecurityInfo() { Name = "EUR", Type = SecurityInfoTypeEnum.Currency },
            //        new SecurityInfo() { Name = "USD" , Type = SecurityInfoTypeEnum.Currency},
            //        new SecurityInfo() { Name = "GBP" , Type = SecurityInfoTypeEnum.Currency},
            //        new SecurityInfo() { Name = "AAPL US Equity" },
            //        new SecurityInfo() { Name = "AS5533318 Corp" },
            //        new SecurityInfo() { Name = "AN4198411 Corp" },
            //        new SecurityInfo() { Name = "EI5630724 Govt" },
            //        new SecurityInfo() { Name = "AD NA Equity" },
            //        new SecurityInfo() { Name = "AMZN US Equity" },
            //        new SecurityInfo() { Name = "GOOG US Equity" },
            //        new SecurityInfo() { Name = "GOOGL US Equity" },
            //        new SecurityInfo() { Name = "MERNEWA LX Equity" },
            //        new SecurityInfo() { Name = "MORGBZH LX Equity" },
            //        new SecurityInfo() { Name = "TAGCI SW Equity" },
            //        new SecurityInfo() { Name = "ED630505     Corp" },
            //        new SecurityInfo() { Name = "EP0492991 Pfd" },
            //        new SecurityInfo() { Name = "ECU9 COMB Curncy" },
            //        new SecurityInfo() { Name = "ECZ9 COMB Curncy" },
            //        new SecurityInfo() { Name = "1720692D Fp" }
            //    }).
            //    SetFieldsList(new List<string>
            //    {
            //        "pxLast",
            //        "name",
            //        "crncy",
            //        "cntryOfIncorporation",
            //        "industrySector",
            //        "mifidIiComplexInstrIndicator",
            //        "legalEntityIdentifier",
            //        "leiUltimateParentCompany"
            //    }).
            //    InitializeSession();
            //var data = await bloombergService.RequestDataAsync();
            //Dump(data);
        }

        private static void Dump(List<ISecurityValues> data)
        {
            foreach (var sec in data)
            {
                System.Console.WriteLine($"SECURITY: {sec.SecurityName} - {sec.ErrorCode}");
                foreach (var value in sec.FieldValues)
                {
                    System.Console.WriteLine($"    FIELD: {value.Name} = {value.Value}");
                }
            }
        }
    }
}
