using PricingServices.Core;
using PricingServices.Providers.Bloomberg.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PricingServices.Providers.Bloomberg
{
    public class BloombergPricingAPIService : IPricingAPIService
    {
        private static HttpClient BeapSession;

        private readonly string UniverseId;
        private readonly string FieldListId;
        private readonly string TriggerId;
        private readonly string RequestId;

        public List<SecurityInfo> SecuritiesList { get; private set; }
        public List<string> FieldsList { get; private set; }
        public Credential Credential { get; private set; } = null;
        public ServiceOptions Options { get; private set; }
        public IServiceResponse ServiceResponse { get; set; }
        public List<Security> CurrentSecuritiesList { get; set; }

        readonly Stopwatch clock = new Stopwatch();
        public BloombergPricingAPIService()
        {
            // Generate unique resource indentifiers.
            var resourceIdPostfix = DateTime.UtcNow.ToString("yyyyMMddhhmmss");
            UniverseId = $"myUniverse{resourceIdPostfix}";
            FieldListId = $"myFieldList{resourceIdPostfix}";
            TriggerId = $"myTrigger{resourceIdPostfix}";
            RequestId = $"myReq{resourceIdPostfix}";
            Options = new ServiceOptions();
            ServiceResponse = new ServiceResponse()
            {
                RequestId = resourceIdPostfix,
                RequestDateTime = DateTime.Now
            };
        }

        public IPricingAPIService SetCredentials(ServiceCredentials serviceCredential)
        {
            Credential = new Credential()
            {
                ClientId = serviceCredential.ClientId,
                ClientSecret = serviceCredential.ClientSecret,
                ExpirationDate = serviceCredential.ExpirationDate
            };
            return this;
        }

        public IPricingAPIService SetOptions(ServiceOptions serviceOptions)
        {
            Options = serviceOptions;
            return this;
        }

        public IPricingAPIService SetSecuritiesList(List<SecurityInfo> securitiesList)
        {
            SecuritiesList = securitiesList;
            return this;
        }

        public IPricingAPIService SetFieldsList(List<string> fieldsList)
        {
            FieldsList = fieldsList;
            return this;
        }

        public IPricingAPIService InitializeSession()
        {
            BeapSession = new HttpClient(new BEAPHandler(credential: Credential));
            return this;
        }

        private static Uri MakeUri(string path)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = "api.bloomberg.com",
                Path = path,
            };
            return uriBuilder.Uri;
        }

        private async Task<string> HttpPost<TPayload>(Uri uri, TPayload payload)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            var body = JsonSerializer.Serialize(payload, options);

            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await BeapSession.PostAsync(uri, content);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception($"Unexpected response status: {response.ReasonPhrase}");
            }

            return response.Headers.Location.ToString();
        }

        private async Task<string> GetScheduledCatalog()
        {
            var uri = MakeUri("/eap/catalogs/");
            var response = await BeapSession.GetAsync(uri);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Unexpected response status: {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var catalogs = JsonSerializer.Deserialize<CatalogsResponse>(content);
            var scheduledCatalog = Array.Find(catalogs.Contains, catalog => catalog.SubscriptionType == "scheduled");
            if (scheduledCatalog == null)
            {
                throw new Exception("Account number not found");
            }

            return scheduledCatalog.Identifier;
        }
        private async Task<string> CreateUniverse(string catalog)
        {
            var uri = MakeUri($"/eap/catalogs/{catalog}/universes/");
            var CurrentSecuritiesList = GetSecuritiesList().ToList();
            var payload = new Universe
            {
                Type = "Universe",
                Identifier = UniverseId,
                Title = "My Universe",
                Description = "Some description",
                Contains = CurrentSecuritiesList.ToArray()
            };
            return await HttpPost(uri, payload);
        }

        private IEnumerable<Security> GetSecuritiesList()
        {
            foreach (var security in SecuritiesList)
            {
                security.ProviderInternalName = security.Name;
                security.Name = FixSecurityName(security);
            }
            return SecuritiesList.Select(security =>
            new Security()
            {
                Type = "Identifier",
                IdentifierType = security.IdentifierType.ToString(),
                IdentifierValue = security.Name 
            });
        }

        //Bloomberg API is case sentitive
        //CORP, GOVT, EQUITY must be Corp, Govt, Equity
        //In currencies must be added Curncy sufix 
        private string FixSecurityName(SecurityInfo security)
        {
            if (security.Type == SecurityInfo.SecurityInfoTypeEnum.Currency)
            {
                return !security.ProviderInternalName.Contains("Curncy") ? 
                    $"{security.ProviderInternalName} Curncy" : 
                    security.Name;
            }
            if (security.Type == SecurityInfo.SecurityInfoTypeEnum.Asset)
            {
                security.Name = FixSecurityNameCase(security.ProviderInternalName, "Corp");
                security.Name = FixSecurityNameCase(security.ProviderInternalName, "Govt");
                security.Name = FixSecurityNameCase(security.ProviderInternalName, "Equity");
                return security.Name;
            }
            return security.Name;
        }

        private string FixSecurityNameCase(string securityName, string containsText)
        {
            var contains = securityName.IndexOf(
                containsText, StringComparison.CurrentCultureIgnoreCase) > 0;
            if (contains)
            {
                var regex = new Regex(containsText, RegexOptions.IgnoreCase);
                return regex.Replace(securityName, containsText);
            }
            else
                return securityName;
        }

        private async Task<string> CreateFieldList(string catalog)
        {
            var uri = MakeUri($"/eap/catalogs/{catalog}/fieldLists/");
            var payload = new FieldList
            {
                Type = "DataFieldList",
                Identifier = FieldListId,
                Title = "My FieldList",
                Description = "Some description",
                Contains = GetFieldsList().ToArray()
            };
            return await HttpPost(uri, payload);
        }

        private IEnumerable<Field> GetFieldsList()
        {
            return FieldsList.Select(field =>
            new Field()
            {
                CleanName = field
            });
        }

        private async Task<string> CreateTrigger(string catalog)
        {
            var uri = MakeUri($"/eap/catalogs/{catalog}/triggers/");

            // NOTE: The "SubmitTrigger" value is used as the trigger's type.
            //       We choose an event-based trigger, in this case, specifying that the
            //       job should run as soon as possible after POSTing the request component
            var payload = new Trigger
            {
                Type = "SubmitTrigger",
                Identifier = TriggerId,
                Title = "My Trigger",
                Description = "Some description"
            };

            // An alternative value, "ScheduledTrigger", can be used to specify a
            // recurring job schedule.
            // Using a 5 minute margin of safety for the job:
            // If the current time is less than 5 minutes before midnight in the account timezone,
            // Ensure the job will be processed the following day.
            //var utcNowBiased = DateTime.UtcNow.AddMinutes(5);
            //var accountTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");  // America/New_York
            //var zonedStartDate = TimeZoneInfo.ConvertTime(utcNowBiased, accountTimeZone);
            //var payload = new Trigger {
            //    Type = "ScheduledTrigger",
            //    Identifier = TriggerId,
            //    Title = "My Trigger",
            //    Frequency = "daily",
            //    StartDate = zonedStartDate.ToString("yyyyMMdd"),
            //    StartTime = "12:00:00"
            //};

            return await HttpPost(uri, payload);
        }

        private async Task<string> CreateRequest(string catalog, string universePath, string fieldListPath, string triggerPath)
        {
            var uri = MakeUri($"/eap/catalogs/{catalog}/requests/");
            var payload = new Request
            {
                Type = "DataRequest",
                Identifier = RequestId,
                Title = "My Request",
                Description = "Some description",
                Universe = MakeUri(universePath),
                FieldList = MakeUri(fieldListPath),
                Trigger = MakeUri(triggerPath),
                Formatting = new RequestFormatting
                {
                    Type = "DataFormat",
                    ColumnHeader = true,
                    DateFormat = "yyyymmdd",
                    Delimiter = Options.RequestFileDelimiter,
                    FileType = "unixFileType",
                    OutputFormat = "variableOutputFormat"
                },
                PricingSourceOptions = new PricingSourceOptions
                {
                    Type = "DataPricingSourceOptions",
                    Prefer = new PricingSourcePreference
                    {
                        Mnemonic = "BGN"
                    }
                }
            };
            return await HttpPost(uri, payload);
        }

        private async Task DownloadDistribution(Uri distributionUrl, string outputFilePath)
        {
            // Create a HTTP request.
            // Set up compression to reduce download time.
            // Note that some of dataset files can exceed 100MB in size,
            // so compression will speed up downloading significantly.
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = distributionUrl
            };
            request.Headers.Add("Accept-Encoding", "gzip");

            var response = await BeapSession.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var content = response.Content;
            // Read the response data into a file.
            // Use streams to optimize memory footprint.
            using (var contentStream = await content.ReadAsStreamAsync())
            using (var outputFile = File.Create(outputFilePath))
            {
                await contentStream.CopyToAsync(outputFile);
            }

            Console.WriteLine($"Status: {response.StatusCode}");
            Console.WriteLine($"Content-Encoding: {content.Headers.ContentEncoding}");
            Console.WriteLine($"Content-Length: {content.Headers.ContentLength} bytes");
            Console.WriteLine($"Downloaded {distributionUrl} to {outputFilePath}");
            Console.WriteLine();
        }

        public async Task<IServiceResponse> RequestDataAsync()
        {
            clock.Start();
            //  Create an SSE session to receive notification when reply is delivered
            using (var sseClient = new Sse.SseClient(MakeUri("/eap/notifications/sse"), BeapSession))
            {
                await sseClient.Connect();

                // Fetch our account number.
                var catalog = await GetScheduledCatalog();

                // Create a universe component.
                var universePath = await CreateUniverse(catalog);
                // Fetch the newly-created universe component.
                await BeapSession.GetAsync(MakeUri(universePath));

                // Create a field list component.
                var fieldListPath = await CreateFieldList(catalog);
                // Fetch the newly-created field list component.
                await BeapSession.GetAsync(MakeUri(fieldListPath));

                // Create a trigger component.
                var triggerPath = await CreateTrigger(catalog);
                // Fetch the newly-created trigger component.
                await BeapSession.GetAsync(MakeUri(triggerPath));

                // Create a request component.
                var requestPath = await CreateRequest(catalog, universePath, fieldListPath, triggerPath);
                // Fetch the newly-created request component.
                await BeapSession.GetAsync(MakeUri(requestPath));

                // Wait for notification that our output is ready for download. We allow a reasonable amount of time for
                // the request to be processed and avoid waiting forever for the purposes of the sample code -- a timeout
                // may not apply to your actual business workflow. For larger requests or requests made during periods of
                // high load, you may need to increase the timeout.
                var replyTimeout = TimeSpan.FromMinutes(15);
                var expirationTimestamp = DateTimeOffset.UtcNow.Add(replyTimeout);
                while (DateTimeOffset.UtcNow < expirationTimestamp)
                {
                    var sseEvent = await sseClient.ReadEvent();

                    if (sseEvent.IsHeartbeat)
                    {
                        Console.WriteLine("Received heartbeat event, keep waiting for events");
                        continue;
                    }

                    DeliveryNotification notification;
                    try
                    {
                        notification = JsonSerializer.Deserialize<DeliveryNotification>(sseEvent.Data);
                    }
                    catch (JsonException)
                    {
                        Console.WriteLine("Received other event type, continue waiting");
                        continue;
                    }

                    Console.WriteLine($"Received reply delivery notification event: {sseEvent.Data}");

                    var deliveryDistributionId = notification.Generated.Identifier;
                    var replyUrl = notification.Generated.Id;
                    var deliveryCatalogId = notification.Generated.Snapshot.Dataset.Catalog.Identifier;

                    if (deliveryCatalogId != catalog || deliveryDistributionId != $"{RequestId}.bbg")
                    {
                        Console.WriteLine("Some other delivery occurred - continue waiting");
                        continue;
                    }
                    // Prepare for storing reply file.
                    Directory.CreateDirectory(Options.ResponseOutputFolderName);
                    var zippedFilePath = Path.Combine(Options.ResponseOutputFolderName, $"{deliveryDistributionId}.gz");
                    // Download reply file from server.
                    await DownloadDistribution(replyUrl, zippedFilePath);
                    Console.WriteLine($"Reply file was downloaded: {zippedFilePath}");
                    var unzippedFilePath = UnzipFile(zippedFilePath);
                    Console.WriteLine($"File was unzipped: {unzippedFilePath}");
                    var securitiesValues = ProcessFile(unzippedFilePath);
                    // Clean files if nedded.
                    if (Options.RemoveFilesAfterResponse == ServiceOptions.ResponseRemoveFilesEnum.RemoveAll ||
                        Options.RemoveFilesAfterResponse == ServiceOptions.ResponseRemoveFilesEnum.RemoveZippedFile)
                        File.Delete(zippedFilePath);
                    if (Options.RemoveFilesAfterResponse == ServiceOptions.ResponseRemoveFilesEnum.RemoveAll ||
                       Options.RemoveFilesAfterResponse == ServiceOptions.ResponseRemoveFilesEnum.RemoveUnzippedFile)
                        File.Delete(unzippedFilePath);
                    clock.Stop();
                    ServiceResponse.ElapsedTime = clock.Elapsed;
                    ServiceResponse.ResponseZippedFilePath = zippedFilePath;
                    ServiceResponse.ResponseUnzippedFilePath = unzippedFilePath;
                    ServiceResponse.SecuritiesValues = securitiesValues;
                    return ServiceResponse;
                }
            }
            Console.WriteLine("Reply NOT delivered, try to increase waiter loop timeout");
            return null;
        }

        public static string UnzipFile(string zippedFilePath)
        {
            var unzippedFileName = string.Empty;
            var fileToUnzip = new FileInfo(zippedFilePath);
            using (FileStream originalFileStream = fileToUnzip.OpenRead())
            {
                string currentFileName = fileToUnzip.FullName;
                unzippedFileName = currentFileName.Remove(currentFileName.Length - fileToUnzip.Extension.Length);
                using (FileStream decompressedFileStream = File.Create(unzippedFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                }
            }
            return unzippedFileName;
        }

        private string GetOriginalName(string securityName)
        {
            return SecuritiesList.FirstOrDefault(
                p => p.Name == securityName)?.ProviderInternalName;
        }

        public List<ISecurityValues> ProcessFile(string responseFileName)
        {
            var SecuritiesValues = new List<ISecurityValues>();
            var startReadingData = false;
            var lineNumber = 0;
            var lines = File.ReadLines(responseFileName);
            var columnNames = new List<string>();
            foreach (var line in lines)
            {
                if (line == "START-OF-DATA") startReadingData = true;
                if (line == "END-OF-DATA") startReadingData = false;
                if (startReadingData)
                {                    
                    var securityValues = new SecurityValues() { RawValue = line };
                    lineNumber++;
                    if (lineNumber == 1) continue;
                    if (lineNumber == 2) columnNames = line.Split(
                        Options.RequestFileDelimiter.ToCharArray()).ToList();
                    if (lineNumber > 2)
                    {
                        var columnValues = line.Split(Options.RequestFileDelimiter.ToCharArray()).ToList();
                        securityValues.SecurityName = columnValues[0];
                        securityValues.OriginalSecurityName = GetOriginalName(securityValues.SecurityName);
                        securityValues.ErrorCode = columnValues[1];
                        if (securityValues.ErrorCode == "0")
                        {
                            for (int colindex = 3; colindex < columnValues.Count; colindex++)
                            {
                                var fieldValue = new FieldValue()
                                {
                                    Name = columnNames[colindex],
                                    Value = columnValues[colindex]
                                };
                                if (!string.IsNullOrWhiteSpace(fieldValue.Name))
                                    securityValues.FieldValues.Add(fieldValue);
                            }
                        }                         
                        SecuritiesValues.Add(securityValues);
                    }
                }
            }
            Console.WriteLine("File was processed");
            return SecuritiesValues;
        }
    }
}
