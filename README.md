# PricingServices
Financial Pricing API Service (Bloomberg Provider)

Welcome to Pricing API Service sample solution.
This solution allows to connect to a different pricing providers and request securities pricing info. You can request different fields like pxLast (security last market price) or company or instrument info. Depending on your subscription additional charges may be applied.

This solution includes a Net core 5 console project and some netstandard class libraries, including a Bloomberg pricing provider

In a short future I'd like to implement other financial providers like Refinitiv, SIX and others.
You can also create your own provider for other pricing services implementing interface IPricingAPIService

## Important Note 
A BEAP (Bloomberg Enterprise Access Point) license is required in order to obtain valid credentials to connect.
You can request a test license contacting [Enterprise Access Point portal](https://www.bloomberg.com/professional/product/enterprise-access-point/)

## Samples
Connect to the service using your BEAP credentials, requesting some secuirities and some extra fields.

``` csharp
static async Task Main()
{
    var bloombergService = Builder.GetPricingService().
        SetCredentials(new ServiceCredentials()
        {
            ClientId = "Your client Id here",
            ClientSecret = "Your client Id here"
        }).
        SetSecuritiesList(new List<SecurityInfo> {
            new SecurityInfo() { Name = "AAPL US EQUITY" },
            new SecurityInfo() { Name = "AS5533318 Corp" },
            new SecurityInfo() { Name = "EI5630724 Govt" },
            new SecurityInfo() { Name = "CAC Index" },
            new SecurityInfo() { Name = "EUR", Type = SecurityInfoTypeEnum.Currency },
            new SecurityInfo() { Name = "USD" , Type = SecurityInfoTypeEnum.Currency},
            new SecurityInfo() { Name = "GBP" , Type = SecurityInfoTypeEnum.Currency},
            new SecurityInfo() { Name = "AMZN US Equity" },
            new SecurityInfo() { Name = "GOOG US Equity" },
            new SecurityInfo() { Name = "GOOGL US Equity" }
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
```
