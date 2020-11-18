﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace PricingServices.Core
{
    public interface IPricingAPIService
    {
        List<string> SecuritiesList { get; }
        List<string> FieldsList { get; }
        IPricingAPIService SetSecuritiesList(List<string> securitiesList);
        IPricingAPIService SetFieldsList(List<string> fieldsList);
        IPricingAPIService SetCredentials(ServiceCredential serviceCredential);
        IPricingAPIService InitializeSession();
        Task RequestDataAsync();
    }
}
