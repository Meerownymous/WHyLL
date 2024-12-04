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
        private readonly IText firstLine;
        private readonly Stream body;
        private readonly Func<HttpRequestMessage, Task<HttpResponseMessage>> convert;
        private readonly bool allowBodyReplay;

        /// <summary>
        /// Renders a response message using Asp.Net Core HttpClient.
        /// </summary>
        public HttpWire(HttpClient client, bool allowBodyReplay = true) : this(
            new AsText(""),
            new List<IPair<string, string>>(),
            new MemoryStream(),
            async msg => await client.SendAsync(msg, HttpCompletionOption.ResponseHeadersRead),
            allowBodyReplay
        )
        { }

        /// <summary>
        /// Renders a response message using Asp.Net Core HttpClient.
        /// </summary>
        private HttpWire(HttpResponseMessage result, bool allowBodyReplay = true) : this(
            new AsText(""),
            new List<IPair<string, string>>(),
            new MemoryStream(),
            _ => Task.FromResult(result),
            allowBodyReplay
        )
        { }

        /// <summary>
        /// Renders a response message using Asp.Net Core HttpClient.
        /// </summary>
        public HttpWire(Func<HttpRequestMessage, Task<HttpResponseMessage>> convert, bool allowBodyReplay = true) : this(
            new AsText(""),
            new List<IPair<string, string>>(),
            new MemoryStream(),
            convert.Invoke,
            allowBodyReplay
        )
        { }

        private HttpWire(
            IText firstLine,
            IList<IPair<string,string>> headers,
            Stream body,
            Func<HttpRequestMessage, Task<HttpResponseMessage>> convert,
            bool allowBodyReplay
            
        )
        {
            this.firstLine = firstLine;
            this.headers = headers;
            this.convert = convert;
            this.body = body;
            this.allowBodyReplay = allowBodyReplay;
        }

        public IWarp<IMessage> Refine(string newFirstLine)
        {
            return new HttpWire(
                new AsText(newFirstLine), this.headers, this.body, this.convert, this.allowBodyReplay
            );
        }

        public IWarp<IMessage> Refine(IEnumerable<IPair<string, string>> newParts) =>
            this.Refine(newParts.ToArray());

        public IWarp<IMessage> Refine(params IPair<string, string>[] parts)
        {
            foreach (var part in parts)
                this.headers.Add(part);
            
            return new HttpWire(this.firstLine, this.headers,this.body,  this.convert, this.allowBodyReplay);
        }

        public IWarp<IMessage> Refine(Stream newBody)
        {
            return new HttpWire(this.firstLine, this.headers, newBody, convert, this.allowBodyReplay);
        }

        public async Task<IMessage> Render()
        {
            var request = RequestMessage(this.firstLine.AsString());
            request.Content = new StreamContent(this.body);
            foreach (var header in headers)
            {
                if(IsContentHeader(header.Key()) && request.Content != null)
                    request.Content.Headers.Add(header.Key(), header.Value());
                else
                    request.Headers.Add(header.Key(), header.Value());
            }
             
            var aspResponse = await convert(request);
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
                    allowBodyReplay 
                        ? new BufferingReadStream(await aspResponse.Content.ReadAsStreamAsync())
                        : await aspResponse.Content.ReadAsStreamAsync()
                );
        }

        private static HttpRequestMessage RequestMessage(string requestLine)
        {
            var result = new HttpRequestMessage();
            var pieces = RequestLineParts(requestLine);
            result.Method = new HttpMethod(pieces[0]);
            result.RequestUri = new Uri(pieces[1]);
            result.Version =
                new Version(
                    new TrimmedLeft(
                        new TrimmedRight(pieces[2], "\r\n"),
                        "HTTP/"
                    ).AsString()
                );
            return result;
        }

        private static string[] RequestLineParts(string requestLine) =>
            requestLine.Split(" ");

        private static bool IsContentHeader(string header) =>
            contentHeaders.Contains(header, StringComparer.OrdinalIgnoreCase);
    }
}