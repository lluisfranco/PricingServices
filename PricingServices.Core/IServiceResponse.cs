using System;
using System.Collections.Generic;

namespace PricingServices.Core
{
    public interface IServiceResponse
    {
        string RequestId { get; set; }
        DateTime RequestDateTime { get; set; }
        TimeSpan ElapsedTime { get; set; } 
        string ResponseZippedFilePath { get; set; }
        string ResponseUnzippedFilePath { get; set; }
        List<ISecurityValues> SecuritiesValues { get; set; }
    }
}
