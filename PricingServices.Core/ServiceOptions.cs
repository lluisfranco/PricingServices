namespace PricingServices.Core
{
    public class ServiceOptions
    {
        public string CustomerName { get; set; } = "FIM";
        public string ResponseOutputFolderName { get; set; } = "TempFiles";
        public string RequestFileDelimiter { get; } = "|";
        public ResponseRemoveFilesEnum RemoveFilesAfterResponse { get; set; }
        public enum ResponseRemoveFilesEnum
        {
            None, RemoveZippedFile, RemoveUnzippedFile, RemoveAll
        }
    }
}
