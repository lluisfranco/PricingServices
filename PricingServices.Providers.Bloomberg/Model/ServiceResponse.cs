using PricingServices.Core;
using System;
using System.Collections.Generic;

namespace PricingServices.Providers.Bloomberg.Model
{
    public class ServiceResponse : IServiceResponse
    {
        public  string RequestId { get; set; }
        public  DateTime RequestDateTime { get; set; }
        public  TimeSpan ElapsedTime { get; set; }
        public  string ResponseZippedFilePath { get; set; }
        public  string ResponseUnzippedFilePath { get; set; }
        public  List<ISecurityValues> SecuritiesValues { get; set; }
    }
}
