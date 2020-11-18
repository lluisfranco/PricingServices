using Serilog;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PricingServices.Providers.Bloomberg
{
    public class BEAPHandler : DelegatingHandler
    {
        const HttpStatusCode PermanentRedirect = (HttpStatusCode)308;
        private readonly string d_apiVersion;
        private readonly Credential d_tokenMaker;
        private const uint d_maxRedirects = 3;
        private const string d_MediaTypeSseEvents = "text/event-stream";
        private readonly ILogger d_logger;

        /// <summary>
        /// Constructs new JWT signing handler and initializes default HttpClientHandler inner HTTP handler 
        /// which is used to delegate the rest of standard HTTP messaging behaviour to the regular
        /// component. 
        /// Note: The default "HttpClientHandler" redirecitons handling is disabled because in case of
        /// HTTP redirection default "HttpClientHandler" behaviour is to follow redirection without
        /// generating new JWT token for each redirected request while actually each request should have
        /// its own unique JWT token.
        ///
        /// This constructor will enable all logging of this unit to console. The following code shows how to
        /// customize this behaviour:
        /// var logger = new LoggerConfiguration()
        ///     .MinimumLevel.Information()
        ///     .WriteTo.Console()
        ///     .CreateLogger();
        /// var httpHandler = new BEAPHandler(logger: logger);
        /// </summary>
        /// <param name="apiVersion"> The Hypermedia API version. </param>
        /// <param name="credential"> JWT token maker instance responsible for JWT generation. </param>
        /// <param name="logger"> An object to be used to log messages. </param>
        public BEAPHandler(string apiVersion = "2", Credential credential = null, ILogger logger = null)
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var defaultHandler = new HttpClientHandler();
            // Important: the following line disables default redirection 
            // hadler because the default handler will not update JWT on 
            // each redirection, JWTSigningHandler will do it in 
            // 'SendAsync' method.
            defaultHandler.AllowAutoRedirect = false;
            InnerHandler = defaultHandler;
            d_apiVersion = apiVersion;
            d_logger = logger ?? DefaultLogger();
            d_tokenMaker = credential ?? Credential.LoadCredential(d_logger);
        }

        /// <summary>
        /// Return default logger in case if it is not provided by application.
        /// </summary>
        /// <returns> Default logger with enabled logging all messages to console. </returns>
        static ILogger DefaultLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
        }

        /// <summary>
        /// Determine whether provided response has one of the redirection status code.
        /// </summary>
        /// <param name="response"> HTTP response to determine redirection status for. </param>
        /// <returns> True if this is redirect response, false otherwise </returns>
        private static bool IsRedirect(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.MultipleChoices:
                case HttpStatusCode.Moved:
                case HttpStatusCode.Found:
                case HttpStatusCode.SeeOther:
                case HttpStatusCode.TemporaryRedirect:
                case PermanentRedirect:
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// Convert HTTP method of redirected response in accord with HTTP standard.
        /// </summary>
        /// <param name="response"> Response containing redirection status. </param>
        /// <param name="request"> Original HTTP request. </param>
        private static void ConvertForRedirect(HttpResponseMessage response, HttpRequestMessage request)
        {
            if (request.Method == HttpMethod.Post)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.MultipleChoices:
                    case HttpStatusCode.Moved:
                    case HttpStatusCode.Found:
                    case HttpStatusCode.SeeOther:
                        request.Content = null;
                        request.Headers.TransferEncodingChunked = false;
                        request.Method = HttpMethod.Get;
                        break;
                }
            }
        }
        /// <summary>
        /// Set new JWT token to outgoing request.
        /// </summary>
        /// <param name="request"> The HTTP request message to send to the server. </param>
        private void AssignBeapToken(HttpRequestMessage request)
        {
            var accessToken = d_tokenMaker.CreateToken(
                request.RequestUri.Host,
                request.RequestUri.LocalPath,
                request.Method.ToString()
                );

            request.Headers.Remove("JWT");
            request.Headers.Add("JWT", accessToken);
        }
        /// <summary>
        /// Add JWT token to request and reflect HTTP redirection responses, if any.
        /// </summary>
        /// <param name="request"> The HTTP request message to send to the server. </param>
        /// <param name="cancellationToken"> A cancellation token to cancel operation. </param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            System.Threading.CancellationToken cancellationToken)
        {
            AssignBeapToken(request);
            request.Headers.Add("api-version", d_apiVersion);

            d_logger.Debug("Request being sent to HTTP server:\n{request}\n", request);

            var response = await base.SendAsync(request, cancellationToken);
            uint redirectCount = 0;
            try
            {
                while (IsRedirect(response) && redirectCount++ < d_maxRedirects)
                {
                    Uri redirectUri;
                    redirectUri = response.Headers.Location;
                    if (!redirectUri.IsAbsoluteUri)
                    {
                        redirectUri = new Uri(request.RequestUri, response.Headers.Location);
                    }

                    d_logger.Information("Redirecting to {redirectUri}.", redirectUri);

                    request.RequestUri = redirectUri;
                    ConvertForRedirect(response, request);
                    response.Dispose();

                    AssignBeapToken(request);
                    response = await base.SendAsync(request, cancellationToken);
                }
            }
            catch (Exception)
            {
                response.Dispose();
                throw;
            }

            switch (response.StatusCode)
            {
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.Unauthorized:
                    d_logger.Fatal("Either supplied credentials are invalid or expired, " +
                        "or the requesting IP is address is not on the allowlist");
                    break;
            }

            d_logger.Debug(
                "{method} {requestUri} [{statusCode}]",
                request.Method,
                request.RequestUri,
                response.StatusCode);
            // If it's not a file download request - print the response contents.
            if (response.Content.Headers.ContentDisposition == null &&
                response.Content.Headers.ContentType.MediaType != d_MediaTypeSseEvents)
            {
                var content = await response.Content.ReadAsStringAsync();
                d_logger.Debug("Response content: {content}", content);
            }
            return response;
        }
    }

}
