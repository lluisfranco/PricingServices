
namespace PricingServices.Client.Win
{
    partial class RibbonForm1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RibbonForm1));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.securityDTOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAssetId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colpxLast = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcrncy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcntryOfIncorporation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colindustrySector = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colmifidIiComplexInstrIndicator = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collegalEntityIdentifier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colleiUltimateParentCompany = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProviderInternalName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colErrorCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colISIN = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityDTOBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 6;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1296, 158);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Load Assets";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Get From Bloomberg";
            this.barButtonItem2.Id = 2;
            this.barButtonItem2.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem2.ImageOptions.SvgImage")));
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Export...";
            this.barButtonItem3.Id = 3;
            this.barButtonItem3.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem3.ImageOptions.SvgImage")));
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Select All";
            this.barButtonItem4.Id = 4;
            this.barButtonItem4.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem4.ImageOptions.SvgImage")));
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "Select none";
            this.barButtonItem5.Id = 5;
            this.barButtonItem5.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem5.ImageOptions.SvgImage")));
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem2);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem3);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem4, true);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem5);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Options";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.securityDTOBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 158);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1296, 605);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // securityDTOBindingSource
            // 
            this.securityDTOBindingSource.DataSource = typeof(PricingServices.Client.Win.SecurityDTO);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAssetId,
            this.colAssetCode,
            this.colAssetName,
            this.colTicker,
            this.colAssetTypeId,
            this.colAssetTypeName,
            this.colIsActive,
            this.colpxLast,
            this.colname,
            this.colcrncy,
            this.colcntryOfIncorporation,
            this.colindustrySector,
            this.colmifidIiComplexInstrIndicator,
            this.collegalEntityIdentifier,
            this.colleiUltimateParentCompany,
            this.colProviderInternalName,
            this.colErrorCode,
            this.colISIN});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            // 
            // colAssetId
            // 
            this.colAssetId.AppearanceHeader.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.colAssetId.AppearanceHeader.Options.UseBackColor = true;
            this.colAssetId.FieldName = "AssetId";
            this.colAssetId.Name = "colAssetId";
            // 
            // colAssetCode
            // 
            this.colAssetCode.AppearanceHeader.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.colAssetCode.AppearanceHeader.Options.UseBackColor = true;
            this.colAssetCode.FieldName = "AssetCode";
            this.colAssetCode.Name = "colAssetCode";
            this.colAssetCode.Visible = true;
            this.colAssetCode.VisibleIndex = 3;
            // 
            // colAssetName
            // 
            this.colAssetName.AppearanceHeader.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.colAssetName.AppearanceHeader.Options.UseBackColor = true;
            this.colAssetName.FieldName = "AssetName";
            this.colAssetName.Name = "colAssetName";
            this.colAssetName.Visible = true;
            this.colAssetName.VisibleIndex = 4;
            // 
            // colTicker
            // 
            this.colTicker.AppearanceHeader.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.colTicker.AppearanceHeader.Options.UseBackColor = true;
            this.colTicker.FieldName = "Ticker";
            this.colTicker.Name = "colTicker";
            this.colTicker.Visible = true;
            this.colTicker.VisibleIndex = 6;
            // 
            // colAssetTypeId
            // 
            this.colAssetTypeId.AppearanceHeader.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.colAssetTypeId.AppearanceHeader.Options.UseBackColor = true;
            this.colAssetTypeId.FieldName = "AssetTypeId";
            this.colAssetTypeId.Name = "colAssetTypeId";
            // 
            // colAssetTypeName
            // 
            this.colAssetTypeName.AppearanceHeader.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.colAssetTypeName.AppearanceHeader.Options.UseBackColor = true;
            this.colAssetTypeName.FieldName = "AssetTypeName";
            this.colAssetTypeName.Name = "colAssetTypeName";
            this.colAssetTypeName.Visible = true;
            this.colAssetTypeName.VisibleIndex = 1;
            // 
            // colIsActive
            // 
            this.colIsActive.AppearanceHeader.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.colIsActive.AppearanceHeader.Options.UseBackColor = true;
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.Visible = true;
            this.colIsActive.VisibleIndex = 2;
            // 
            // colpxLast
            // 
            this.colpxLast.FieldName = "pxLast";
            this.colpxLast.Name = "colpxLast";
            this.colpxLast.Visible = true;
            this.colpxLast.VisibleIndex = 9;
            // 
            // colname
            // 
            this.colname.FieldName = "name";
            this.colname.Name = "colname";
            this.colname.Visible = true;
            this.colname.VisibleIndex = 10;
            // 
            // colcrncy
            // 
            this.colcrncy.FieldName = "crncy";
            this.colcrncy.Name = "colcrncy";
            this.colcrncy.Visible = true;
            this.colcrncy.VisibleIndex = 11;
            // 
            // colcntryOfIncorporation
            // 
            this.colcntryOfIncorporation.FieldName = "cntryOfIncorporation";
            this.colcntryOfIncorporation.Name = "colcntryOfIncorporation";
            this.colcntryOfIncorporation.Visible = true;
            this.colcntryOfIncorporation.VisibleIndex = 12;
            // 
            // colindustrySector
            // 
            this.colindustrySector.FieldName = "industrySector";
            this.colindustrySector.Name = "colindustrySector";
            this.colindustrySector.Visible = true;
            this.colindustrySector.VisibleIndex = 13;
            // 
            // colmifidIiComplexInstrIndicator
            // 
            this.colmifidIiComplexInstrIndicator.FieldName = "mifidIiComplexInstrIndicator";
            this.colmifidIiComplexInstrIndicator.Name = "colmifidIiComplexInstrIndicator";
            this.colmifidIiComplexInstrIndicator.Visible = true;
            this.colmifidIiComplexInstrIndicator.VisibleIndex = 14;
            // 
            // collegalEntityIdentifier
            // 
            this.collegalEntityIdentifier.FieldName = "legalEntityIdentifier";
            this.collegalEntityIdentifier.Name = "collegalEntityIdentifier";
            this.collegalEntityIdentifier.Visible = true;
            this.collegalEntityIdentifier.VisibleIndex = 15;
            // 
            // colleiUltimateParentCompany
            // 
            this.colleiUltimateParentCompany.FieldName = "leiUltimateParentCompany";
            this.colleiUltimateParentCompany.Name = "colleiUltimateParentCompany";
            this.colleiUltimateParentCompany.Visible = true;
            this.colleiUltimateParentCompany.VisibleIndex = 16;
            // 
            // colProviderInternalName
            // 
            this.colProviderInternalName.AppearanceHeader.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.colProviderInternalName.AppearanceHeader.Options.UseBackColor = true;
            this.colProviderInternalName.FieldName = "ProviderInternalName";
            this.colProviderInternalName.Name = "colProviderInternalName";
            this.colProviderInternalName.Visible = true;
            this.colProviderInternalName.VisibleIndex = 7;
            // 
            // colErrorCode
            // 
            this.colErrorCode.AppearanceHeader.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.colErrorCode.AppearanceHeader.Options.UseBackColor = true;
            this.colErrorCode.FieldName = "ErrorCode";
            this.colErrorCode.Name = "colErrorCode";
            this.colErrorCode.Visible = true;
            this.colErrorCode.VisibleIndex = 8;
            // 
            // colISIN
            // 
            this.colISIN.AppearanceHeader.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.colISIN.AppearanceHeader.Options.UseBackColor = true;
            this.colISIN.FieldName = "ISIN";
            this.colISIN.Name = "colISIN";
            this.colISIN.Visible = true;
            this.colISIN.VisibleIndex = 5;
            // 
            // RibbonForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 763);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.ribbon);
            this.Name = "RibbonForm1";
            this.Ribbon = this.ribbon;
            this.Text = "RibbonForm1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityDTOBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private System.Windows.Forms.BindingSource securityDTOBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetId;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetCode;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetName;
        private DevExpress.XtraGrid.Columns.GridColumn colTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
        private DevExpress.XtraGrid.Columns.GridColumn colpxLast;
        private DevExpress.XtraGrid.Columns.GridColumn colname;
        private DevExpress.XtraGrid.Columns.GridColumn colcrncy;
        private DevExpress.XtraGrid.Columns.GridColumn colcntryOfIncorporation;
        private DevExpress.XtraGrid.Columns.GridColumn colindustrySector;
        private DevExpress.XtraGrid.Columns.GridColumn colmifidIiComplexInstrIndicator;
        private DevExpress.XtraGrid.Columns.GridColumn collegalEntityIdentifier;
        private DevExpress.XtraGrid.Columns.GridColumn colleiUltimateParentCompany;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraGrid.Columns.GridColumn colProviderInternalName;
        private DevExpress.XtraGrid.Columns.GridColumn colErrorCode;
        private DevExpress.XtraGrid.Columns.GridColumn colISIN;
    }
}