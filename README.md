# PricingServices
Financial Pricing API Service (Bloomberg Provider)

Welcome to Pricing API Service sample solution.
This solution allows to connect to a different pricing providers and request securities pricing info. You can request different fields like pxLast (security last market price) or company or instrument info. Depending on your subscription additional charges may be applied.

This solution includes a Net core 5 console project and some netstandard class libraries, including a Bloomberg pricing provider

In a short future I'd like to implement other financial providers like Refinitiv, SIX and others.
You can also create your own provider for other pricing services implementing interface IPricingAPIService

## Important Note 
A BEAP (Bloomberg Enterprise Access Point) license is required in order to obtain a valid set of credentials to connect.
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

When creating a new SecurityInfo object you can specify the instrument type: Asset (default) or Curreny. This is because in the most providers currencies must add some prefix/suffix appart of the currency ISO code (USD).

You can also specify the sedcurity's type of identifier from a list of different types: Bloomberg TICKER (default), ISIN or, BB_GLOBAL.

## Response type
Response includes some metadata (id, time, elapsed time) and the list of securities and requested values.

```json
{
    "$type": "PricingServices.Providers.Bloomberg.Model.ServiceResponse",
    "RequestId": "20201120113447",
    "RequestDateTime": "2020-11-20T12:34:47.4988258+01:00",
    "ElapsedTime": "00:00:32.2943616",
    "ResponseZippedFilePath": "TempFiles\\myReq20201120113447.bbg.gz",
    "ResponseUnzippedFilePath": "----\\bin\\Debug\\net5.0\\TempFiles\\myReq20201120113447.bbg",
    "SecuritiesValues": {
        "$type": "System.Collections.Generic.List<PricingServices.Core.ISecurityValues>",
        "$values": [
            {
                "$type": "PricingServices.Providers.Bloomberg.Model.SecurityValues",
                "SecurityName": "AAPL US Equity",
                "FieldValues": {
                    "$type": "System.Collections.Generic.List<PricingServices.Core.IFieldValue>",
                    "$values": [
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "PX_LAST",
                            "Value": "118.640000"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "NAME",
                            "Value": "APPLE INC"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CRNCY",
                            "Value": "USD"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CNTRY_OF_INCORPORATION",
                            "Value": "US"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "INDUSTRY_SECTOR",
                            "Value": "Technology"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "MIFID_II_COMPLEX_INSTR_INDICATOR",
                            "Value": "N"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEGAL_ENTITY_IDENTIFIER",
                            "Value": "HWUPKR0MPOU8FGXBT394"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEI_ULTIMATE_PARENT_COMPANY",
                            "Value": "HWUPKR0MPOU8FGXBT394"
                        }
                    ]
                },
                "ErrorCode": "0",
                "RawValue": "AAPL US Equity|0|8|118.640000|APPLE INC|USD|US|Technology|N|HWUPKR0MPOU8FGXBT394|HWUPKR0MPOU8FGXBT394|"
            },
            {
                "$type": "PricingServices.Providers.Bloomberg.Model.SecurityValues",
                "SecurityName": "AS5533318 Corp",
                "FieldValues": {
                    "$type": "System.Collections.Generic.List<PricingServices.Core.IFieldValue>",
                    "$values": [
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "PX_LAST",
                            "Value": "N.S."
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "NAME",
                            "Value": "N.S."
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CRNCY",
                            "Value": "N.S."
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CNTRY_OF_INCORPORATION",
                            "Value": "N.S."
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "INDUSTRY_SECTOR",
                            "Value": "N.S."
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "MIFID_II_COMPLEX_INSTR_INDICATOR",
                            "Value": "N.S."
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEGAL_ENTITY_IDENTIFIER",
                            "Value": "ZXTILKJKG63JELOEG630"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEI_ULTIMATE_PARENT_COMPANY",
                            "Value": "ZXTILKJKG63JELOEG630"
                        }
                    ]
                },
                "ErrorCode": "0",
                "RawValue": "AS5533318 Corp|0|8|N.S.|N.S.|N.S.|N.S.|N.S.|N.S.|ZXTILKJKG63JELOEG630|ZXTILKJKG63JELOEG630|"
            },
            {
                "$type": "PricingServices.Providers.Bloomberg.Model.SecurityValues",
                "SecurityName": "EI5630724 Govt",
                "FieldValues": {
                    "$type": "System.Collections.Generic.List<PricingServices.Core.IFieldValue>",
                    "$values": [
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "PX_LAST",
                            "Value": "N.S."
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "NAME",
                            "Value": "N.S."
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CRNCY",
                            "Value": "N.S."
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CNTRY_OF_INCORPORATION",
                            "Value": "N.S."
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "INDUSTRY_SECTOR",
                            "Value": "N.S."
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "MIFID_II_COMPLEX_INSTR_INDICATOR",
                            "Value": "N.S."
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEGAL_ENTITY_IDENTIFIER",
                            "Value": "ERE94C0BSULG2RM19605"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEI_ULTIMATE_PARENT_COMPANY",
                            "Value": "ERE94C0BSULG2RM19605"
                        }
                    ]
                },
                "ErrorCode": "0",
                "RawValue": "EI5630724 Govt|0|8|N.S.|N.S.|N.S.|N.S.|N.S.|N.S.|ERE94C0BSULG2RM19605|ERE94C0BSULG2RM19605|"
            },
            {
                "$type": "PricingServices.Providers.Bloomberg.Model.SecurityValues",
                "SecurityName": "CAC Index",
                "FieldValues": {
                    "$type": "System.Collections.Generic.List<PricingServices.Core.IFieldValue>",
                    "$values": [
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "PX_LAST",
                            "Value": "5509.700000"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "NAME",
                            "Value": "CAC 40 INDEX"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CRNCY",
                            "Value": "EUR"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CNTRY_OF_INCORPORATION",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "INDUSTRY_SECTOR",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "MIFID_II_COMPLEX_INSTR_INDICATOR",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEGAL_ENTITY_IDENTIFIER",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEI_ULTIMATE_PARENT_COMPANY",
                            "Value": " "
                        }
                    ]
                },
                "ErrorCode": "0",
                "RawValue": "CAC Index|0|8|5509.700000|CAC 40 INDEX|EUR| | | | | |"
            },
            {
                "$type": "PricingServices.Providers.Bloomberg.Model.SecurityValues",
                "SecurityName": "EUR Curncy",
                "FieldValues": {
                    "$type": "System.Collections.Generic.List<PricingServices.Core.IFieldValue>",
                    "$values": [
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "PX_LAST",
                            "Value": "1.186200"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "NAME",
                            "Value": "Euro Spot"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CRNCY",
                            "Value": "EUR"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CNTRY_OF_INCORPORATION",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "INDUSTRY_SECTOR",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "MIFID_II_COMPLEX_INSTR_INDICATOR",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEGAL_ENTITY_IDENTIFIER",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEI_ULTIMATE_PARENT_COMPANY",
                            "Value": " "
                        }
                    ]
                },
                "ErrorCode": "0",
                "RawValue": "EUR Curncy|0|8|1.186200|Euro Spot|EUR| | | | | |"
            },
            {
                "$type": "PricingServices.Providers.Bloomberg.Model.SecurityValues",
                "SecurityName": "USD Curncy",
                "FieldValues": {
                    "$type": "System.Collections.Generic.List<PricingServices.Core.IFieldValue>",
                    "$values": [
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "PX_LAST",
                            "Value": "1.000000"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "NAME",
                            "Value": "US Dollar Spot"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CRNCY",
                            "Value": "USD"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CNTRY_OF_INCORPORATION",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "INDUSTRY_SECTOR",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "MIFID_II_COMPLEX_INSTR_INDICATOR",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEGAL_ENTITY_IDENTIFIER",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEI_ULTIMATE_PARENT_COMPANY",
                            "Value": " "
                        }
                    ]
                },
                "ErrorCode": "0",
                "RawValue": "USD Curncy|0|8|1.000000|US Dollar Spot|USD| | | | | |"
            },
            {
                "$type": "PricingServices.Providers.Bloomberg.Model.SecurityValues",
                "SecurityName": "GBP Curncy",
                "FieldValues": {
                    "$type": "System.Collections.Generic.List<PricingServices.Core.IFieldValue>",
                    "$values": [
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "PX_LAST",
                            "Value": "1.327800"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "NAME",
                            "Value": "British Pound Spot"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CRNCY",
                            "Value": "GBP"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CNTRY_OF_INCORPORATION",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "INDUSTRY_SECTOR",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "MIFID_II_COMPLEX_INSTR_INDICATOR",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEGAL_ENTITY_IDENTIFIER",
                            "Value": " "
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEI_ULTIMATE_PARENT_COMPANY",
                            "Value": " "
                        }
                    ]
                },
                "ErrorCode": "0",
                "RawValue": "GBP Curncy|0|8|1.327800|British Pound Spot|GBP| | | | | |"
            },
            {
                "$type": "PricingServices.Providers.Bloomberg.Model.SecurityValues",
                "SecurityName": "AMZN US Equity",
                "FieldValues": {
                    "$type": "System.Collections.Generic.List<PricingServices.Core.IFieldValue>",
                    "$values": [
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "PX_LAST",
                            "Value": "3117.020000"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "NAME",
                            "Value": "AMAZON.COM INC"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CRNCY",
                            "Value": "USD"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CNTRY_OF_INCORPORATION",
                            "Value": "US"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "INDUSTRY_SECTOR",
                            "Value": "Communications"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "MIFID_II_COMPLEX_INSTR_INDICATOR",
                            "Value": "N"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEGAL_ENTITY_IDENTIFIER",
                            "Value": "ZXTILKJKG63JELOEG630"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEI_ULTIMATE_PARENT_COMPANY",
                            "Value": "ZXTILKJKG63JELOEG630"
                        }
                    ]
                },
                "ErrorCode": "0",
                "RawValue": "AMZN US Equity|0|8|3117.020000|AMAZON.COM INC|USD|US|Communications|N|ZXTILKJKG63JELOEG630|ZXTILKJKG63JELOEG630|"
            },
            {
                "$type": "PricingServices.Providers.Bloomberg.Model.SecurityValues",
                "SecurityName": "GOOG US Equity",
                "FieldValues": {
                    "$type": "System.Collections.Generic.List<PricingServices.Core.IFieldValue>",
                    "$values": [
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "PX_LAST",
                            "Value": "1763.920000"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "NAME",
                            "Value": "ALPHABET INC-CL C"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CRNCY",
                            "Value": "USD"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CNTRY_OF_INCORPORATION",
                            "Value": "US"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "INDUSTRY_SECTOR",
                            "Value": "Communications"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "MIFID_II_COMPLEX_INSTR_INDICATOR",
                            "Value": "N"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEGAL_ENTITY_IDENTIFIER",
                            "Value": "5493006MHB84DD0ZWV18"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEI_ULTIMATE_PARENT_COMPANY",
                            "Value": "5493006MHB84DD0ZWV18"
                        }
                    ]
                },
                "ErrorCode": "0",
                "RawValue": "GOOG US Equity|0|8|1763.920000|ALPHABET INC-CL C|USD|US|Communications|N|5493006MHB84DD0ZWV18|5493006MHB84DD0ZWV18|"
            },
            {
                "$type": "PricingServices.Providers.Bloomberg.Model.SecurityValues",
                "SecurityName": "GOOGL US Equity",
                "FieldValues": {
                    "$type": "System.Collections.Generic.List<PricingServices.Core.IFieldValue>",
                    "$values": [
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "PX_LAST",
                            "Value": "1758.570000"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "NAME",
                            "Value": "ALPHABET INC-CL A"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CRNCY",
                            "Value": "USD"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "CNTRY_OF_INCORPORATION",
                            "Value": "US"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "INDUSTRY_SECTOR",
                            "Value": "Communications"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "MIFID_II_COMPLEX_INSTR_INDICATOR",
                            "Value": "N"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEGAL_ENTITY_IDENTIFIER",
                            "Value": "5493006MHB84DD0ZWV18"
                        },
                        {
                            "$type": "PricingServices.Providers.Bloomberg.Model.FieldValue",
                            "Name": "LEI_ULTIMATE_PARENT_COMPANY",
                            "Value": "5493006MHB84DD0ZWV18"
                        }
                    ]
                },
                "ErrorCode": "0",
                "RawValue": "GOOGL US Equity|0|8|1758.570000|ALPHABET INC-CL A|USD|US|Communications|N|5493006MHB84DD0ZWV18|5493006MHB84DD0ZWV18|"
            }
        ]
    }
}
```

Hope this helps you ;)
