using DevExpress.XtraGrid;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PricingServices.Client.Win
{
    public static class DxExt
    {
        public enum ExportGridControlFormatEnum
        {
            pdf,
            xlsx
        }

        public static void ExportToDisk(this GridControl control,
                ExportGridControlFormatEnum format, bool showMessageOnError = false)
        {
            try
            {
                var file = System.IO.Path.GetTempFileName().Replace("tmp", format.ToString());
                switch (format)
                {
                    case ExportGridControlFormatEnum.xlsx:
                        control.ExportToXlsx(file);
                        break;
                    case ExportGridControlFormatEnum.pdf:
                        control.ExportToPdf(file);
                        break;
                    default:
                        break;
                }
                Process.Start(file);
            }
            catch (Exception ex)
            {
                if (showMessageOnError)
                    MessageBox.Show(ex.Message, Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}