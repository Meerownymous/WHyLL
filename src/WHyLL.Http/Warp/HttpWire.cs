using Tonga;
using Tonga.Enumerable;
using Tonga.Text;
using WHyLL.Message;

namespace WHyLL.Http.Warp
{
    /// <summary>
    /// Renders a response message using Asp.Net Core HttpClient.
    /// </summary>
    public sealed class HttpWire : IWarp<IMessage>
    {
        private static readonly string[] contentHeaders =
        [
            "Content-Type",
            "Content-Length",
            "Content-Disposition",
            "Content-Encoding",
            "Content-Language",
            "Content-Location",
            "Content-Range", 
            "Content-Security-Policy",
            "Content-Security-Policy-Report-Only",
            "Content-Script-Type",
            "Content-Style-Type"
        ];

        private readonly IList<IPair<string, string>> headers;
        private readonly HttpRequestMessage message;
        private readonly Func<HttpRequestMessage, Task<HttpResponseMessage>> convert;

        /// <summary>
        /// Renders a response message using Asp.Net Core HttpClient.
        /// </summary>
        public HttpWire(HttpClient client) : this(
            new HttpRequestMessage(),
            new List<IPair<string, string>>(),
            msg => client.SendAsync(msg, HttpCompletionOption.ResponseHeadersRead)
        )
        { }

        /// <summary>
        /// Renders a response message using Asp.Net Core HttpClient.
        /// </summary>
        public HttpWire(HttpResponseMessage result) : this(
            new HttpRequestMessage(),
            new List<IPair<string, string>>(),
            _ => Task.FromResult(result)
        )
        { }

        /// <summary>
        /// Renders a response message using Asp.Net Core HttpClient.
        /// </summary>
        public HttpWire(Func<HttpRequestMessage, Task<HttpResponseMessage>> convert) : this(
            new HttpRequestMessage(),
            new List<IPair<string, string>>(),
            convert.Invoke
        )
        { }

        private HttpWire(
            HttpRequestMessage message,
            IList<IPair<string,string>> headers,
            Func<HttpRequestMessage, Task<HttpResponseMessage>> convert
            
        )
        {
            this.message = message;
            this.headers = headers;
            this.convert = convert;
        }

        public IWarp<IMessage> Refine(string requestLine)
        {
            var pieces = RequestLineParts(requestLine);
            message.Method = new HttpMethod(pieces[0]);
            message.RequestUri = new Uri(pieces[1]);
            message.Version =
                new Version(
                    new TrimmedLeft(
                        new TrimmedRight(pieces[2], "\r\n"),
                        "HTTP/"
                    ).AsString()
                );
            return new HttpWire(this.message, this.headers, convert);
        }

        public IWarp<IMessage> Refine(IEnumerable<IPair<string, string>> parts) =>
            this.Refine(parts.ToArray());

        public IWarp<IMessage> Refine(params IPair<string, string>[] parts)
        {
            foreach (var part in parts)
                this.headers.Add(part);
            
            return new HttpWire(message, headers, convert);
        }

        public IWarp<IMessage> Refine(Stream body)
        {
            message.Content = new StreamContent(body);
            return new HttpWire(message, headers, convert);
        }

        public async Task<IMessage> Render()
        {
            foreach (var header in headers)
            {
                if(IsContentHeader(header.Key()) && message.Content != null)
                    message.Content.Headers.Add(header.Key(), header.Value());
                else
                    message.Headers.Add(header.Key(), header.Value());
            }
            var aspResponse = await convert(message);
            return
                new SimpleMessage(
                    $"HTTP/{aspResponse.Version} {(int)aspResponse.StatusCode} {aspResponse.ReasonPhrase}\r\n",
                    Tonga.Enumerable.Joined._(
                        Mapped._(
                            header => 
                            Mapped._(
                                value => Tonga.Map.AsPair._(header.Key, value),
                                header.Value
                            ),
                            aspResponse.Headers
                        )
                    ),
                    await aspResponse.Content.ReadAsStreamAsync()
                );
        }

        private static string[] RequestLineParts(string requestLine) =>
            requestLine.Split(" ");

        private static bool IsContentHeader(string header) =>
            contentHeaders.Contains(header, StringComparer.OrdinalIgnoreCase);
    }
}