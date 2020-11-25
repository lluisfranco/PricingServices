using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingServices.Client.Win
{
    public class SecurityDTO
    {
        public int AssetId { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string Ticker { get; set; }
        public int AssetTypeId { get; set; }
        public string AssetTypeName { get; set; }
        public bool IsActive { get; set; }

        public string pxLast { get; set; }
        public string name { get; set; }
        public string crncy { get; set; }
        public string cntryOfIncorporation { get; set; }
        public string industrySector { get; set; }
        public string mifidIiComplexInstrIndicator { get; set; }
        public string legalEntityIdentifier { get; set; }
        public string leiUltimateParentCompany { get; set; }
    }
}
