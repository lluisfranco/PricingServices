using DevExpress.XtraBars;
using PricingServices.Core;
using PricingServices.Factory;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static PricingServices.Core.SecurityInfo;

namespace PricingServices.Client.Win
{
    public partial class RibbonForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public RibbonForm1()
        {
            InitializeComponent();
        }

        public List<SecurityDTO> Securities { get; private set; }

        private async void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.UseWaitCursor = true;
            var repo = new AssetRepo();
            Securities = await repo.GetSecuritiesAsync();
            securityDTOBindingSource.DataSource = Securities;
            Application.UseWaitCursor = false;
        }

        private async void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            var securities = GetSelectedSecurities();
            if (securities.Count == 0) return;
            Application.UseWaitCursor = true;
            var bloombergService = Builder.GetPricingService().
            SetCredentials(new ServiceCredentials()
            {
                ClientId = "1e6e5c12b273505793bff2f9df21107f",
                ClientSecret = "a1cd3e8a0453cee37635f60f16d2a95a3fe1c807b0dcaf07bdc2ee057638c621"
            }).
            SetSecuritiesList(
                securities
            ).
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
            foreach (var sv in response.SecuritiesValues)
            {
                var security = Securities.FirstOrDefault(
                    p => p.Ticker == sv.SecurityName);
                if(security != null)
                {
                    security.ProviderInternalName = sv.ProviderInternalSecurityName;
                    security.ErrorCode = sv.ErrorCode;
                    if (sv.ErrorCode == "0" && sv.FieldValues.Count > 0)
                    {
                        security.pxLast = sv.FieldValues[0].Value;
                        security.name = sv.FieldValues[1].Value;
                        security.crncy = sv.FieldValues[2].Value;
                        security.cntryOfIncorporation = sv.FieldValues[3].Value;
                        security.industrySector = sv.FieldValues[4].Value;
                        security.mifidIiComplexInstrIndicator = sv.FieldValues[5].Value;
                        security.legalEntityIdentifier = sv.FieldValues[6].Value;
                        security.leiUltimateParentCompany = sv.FieldValues[7].Value;
                    }
                }
            }
            gridView1.RefreshData();
            Application.UseWaitCursor = false;
        }

        private List<SecurityInfo> GetSelectedSecurities()
        {
            var data = new List<SecurityInfo>();
            foreach (var rowindex in gridView1.GetSelectedRows())
            {
                var row = gridView1.GetRow(rowindex) as SecurityDTO;
                var s = new SecurityInfo()
                {
                    Name = row.Ticker
                };
                if (row.AssetTypeId == 0) s.Type = SecurityInfoTypeEnum.Currency;
                data.Add(s);
            }
            return data;
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl1.ExportToDisk(DxExt.ExportGridControlFormatEnum.xlsx, true);
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridView1.SelectAll();
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridView1.ClearSelection();
        }
    }
}