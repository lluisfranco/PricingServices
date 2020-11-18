using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sse
{
    /// <summary>
    /// This class represents a single parsed SSE event.
    /// </summary>
    class SseEvent
    {
        public string Origin { get; }
        public string Type { get; }
        public string Id { get; }
        public string Data { get; }
        public string Comments { get; }
        public int? Retry { get; }
        /// <summary>
        /// Indicates whether or not the event is a BEAP hearbeat.
        /// </summary>
        /// <returns>true for heartbeats, false otherwise.</returns>
        public bool IsHeartbeat { get { return Data == null; } }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="origin">A URI the event came from.</param>
        /// <param name="type">Event type.</param>
        /// <param name="id">Event identifier.</param>
        /// <param name="data">All the event data lines.</param>
        /// <param name="comments">All the event comment lines.</param>
        /// <param name="retry">Reconnection timeout value, if any, or null.</param>
        public SseEvent(string origin, string type, string id, string data, string comments, int? retry)
        {
            Origin = origin;
            Type = type;
            Id = id;
            Data = data;
            Comments = comments;
            Retry = retry;
        }

    }

    /// <summary>
    /// This class constructs a new event from event stream lines.
    /// </summary>
    class SseEventBuilder
    {
        private const string d_defaultType = "message";
        private const string d_fieldNameData = "data";
        private const string d_fieldNameId = "id";
        private const string d_fieldNameRetry = "retry";
        private const string d_fieldNameType = "event";
        private const string d_fieldPattern = "(?<field>event|id|data|retry):?( ?(?<value>.*))";
        private const string d_lineSeparator = "\n";
        private const string d_valueMark = ":";

        private static readonly Regex d_fieldParser = new Regex(d_fieldPattern);

        private readonly string d_origin;
        private string d_type = d_defaultType;
        private string d_id = null;
        private readonly List<string> d_data = new List<string>();
        private readonly List<string> d_comments = new List<string>();
        private int? d_retry = null;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="origin">A URI the stream came from.</param>
        public SseEventBuilder(string origin)
        {
            d_origin = origin;
        }

        /// <summary>
        /// Parses the specified line according to the SSE standard rules
        /// and makes the line a part of the event being constructed.
        /// </summary>
        /// <param name="line">Single event stream line.</param>
        public void AddLine(string line)
        {
            if (line.StartsWith(d_valueMark))
            {
                d_comments.Add(line.Substring(1));
                return;
            }

            var match = d_fieldParser.Match(line);
            if (!match.Success)
            {
                // Unknown field names are ignored according to the standard.
                Console.Error.WriteLine($"Invalid SSE line: {line}");
                return;
            }

            var field = match.Groups["field"].Value;
            var valueGroup = match.Groups["value"];
            var value = valueGroup.Success ? valueGroup.Value : "";
            switch (field)
            {
                case d_fieldNameType:
                    d_type = value;
                    break;
                case d_fieldNameId:
                    d_id = value;
                    break;
                case d_fieldNameData:
                    d_data.Add(value);
                    break;
                case d_fieldNameRetry:
                    if (int.TryParse(value, out int retry))
                    {
                        d_retry = retry;
                    }
                    break;
            }
        }

        /// <summary>
        /// Makes a new event using the fields collected so far.
        /// </summary>
        /// <returns>A new event.</returns>
        public SseEvent MakeEvent()
        {
            return new SseEvent(
                d_origin,
                d_type,
                d_id,
                d_data.Any() ? string.Join(d_lineSeparator, d_data) : null,
                d_comments.Any() ? string.Join(d_lineSeparator, d_comments) : null,
                d_retry);
        }
    }

    /// <summary>
    /// This class is a SSE event stream parser.
    /// </summary>
    class SseStreamParser : IDisposable
    {
        private readonly StreamReader d_data;
        private readonly string d_origin;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="data">The stream to be read.</param>
        /// <param name="origin">A URI the stream came from.</param>
        public SseStreamParser(StreamReader data, string origin)
        {
            d_data = data;
            d_origin = origin;
        }

        /// <summary>
        /// Reads an event from the event stream.
        /// </summary>
        /// <returns>The next event.</returns>
        public async Task<SseEvent> ReadEvent()
        {
            var builder = new SseEventBuilder(d_origin);
            while (true)
            {
                var line = await d_data.ReadLineAsync();
                if (line == null)
                {
                    // Discard the event being constructed if the stream ends before the final new line.
                    throw new EndOfStreamException("event stream is over");
                }

                if (line == string.Empty)
                {
                    // Dispatch the event if a blank line is encountered.
                    return builder.MakeEvent();
                }

                // Keep collecting the event lines.
                builder.AddLine(line);
            }
        }

        /// <summary>
        /// Closes the underlying stream.
        /// </summary>
        public void Dispose()
        {
            d_data.Dispose();
        }
    }

    /// <summary>
    /// This class serves as a HTTP client capable of receiving events from a server.
    /// </summary>
    class SseClient : IDisposable
    {
        private const string d_headerNameAccept = "Accept";
        private const string d_headerNameCacheControl = "Cache-Control";
        private const string d_headerNameLastEventId = "Last-Event-ID";
        private const string d_headerValueCacheControl = "no-cache";
        private const string d_headerValueContentType = "text/event-stream";
        private const int d_maxConnectAttempts = 3;

        private readonly Uri d_uri;
        private readonly HttpClient d_session;
        private int d_retryTimeoutMs = 3000;
        private HttpResponseMessage d_response;
        private SseStreamParser d_parser;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="uri">A URI to send the initial request.</param>
        /// <param name="session">An HTTP client session.</param>
        public SseClient(Uri uri, HttpClient session)
        {
            d_uri = uri;
            d_session = session;

            d_session.DefaultRequestHeaders.Add(d_headerNameAccept, d_headerValueContentType);
            d_session.DefaultRequestHeaders.Add(d_headerNameCacheControl, d_headerValueCacheControl);
        }

        /// <summary>
        /// Tries to establish a connection by sending a HTTP GET request.
        /// </summary>
        public async Task Connect()
        {
            for (var i = 0; i < d_maxConnectAttempts; ++i)
            {
                try
                {
                    d_response?.Dispose();
                    d_response = await d_session.GetAsync(d_uri, HttpCompletionOption.ResponseHeadersRead);
                    d_response.EnsureSuccessStatusCode();
                    var data = await d_response.Content.ReadAsStreamAsync();
                    var reader = new StreamReader(data);
                    d_parser?.Dispose();
                    d_parser = new SseStreamParser(reader, d_uri.ToString());
                    return;
                }
                catch (HttpRequestException error)
                {
                    if (i + 1 == d_maxConnectAttempts)
                    {
                        throw;
                    }

                    Console.Error.WriteLine(error.Message);
                }
            }
        }

        /// <summary>
        /// Reads an event from the event stream.
        /// </summary>
        /// <returns>The next event.</returns>
        public async Task<SseEvent> ReadEvent()
        {
            for (var i = 0; i < d_maxConnectAttempts; ++i)
            {
                try
                {
                    var nextEvent = await d_parser.ReadEvent();
                    if (!string.IsNullOrEmpty(nextEvent.Id))
                    {
                        d_session.DefaultRequestHeaders.Remove(d_headerNameLastEventId);
                        d_session.DefaultRequestHeaders.Add(d_headerNameLastEventId, nextEvent.Id);
                    }
                    d_retryTimeoutMs = nextEvent.Retry ?? d_retryTimeoutMs;
                    return nextEvent;
                }
                catch (Exception error)
                {
                    if (i + 1 == d_maxConnectAttempts)
                    {
                        throw;
                    }

                    Console.Error.WriteLine($"Error when reading event from the SSE server: {error.Message}");
                    d_session.CancelPendingRequests();
                    await Task.Delay(d_retryTimeoutMs);
                    await Connect();
                }
            }
            throw new Exception("too many failed attempts to read an event");
        }

        /// <summary>
        /// Releases all the underlying resources.
        /// </summary>
        public void Dispose()
        {
            d_parser?.Dispose();
            d_response?.Dispose();
            d_session.Dispose();
        }
    }
}
