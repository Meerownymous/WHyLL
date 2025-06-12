using Tonga;
using Tonga.Enumerable;
using Tonga.Map;
using Tonga.Text;
using WHyLL.Message;
using WHyLL.Prologue;

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
        private readonly IPrologue prologue;
        private readonly Stream body;
        private readonly Func<HttpRequestMessage, Task<HttpResponseMessage>> convert;
        private readonly bool allowBodyReplay;

        /// <summary>
        /// Renders a response message using Asp.Net Core HttpClient.
        /// </summary>
        public HttpWire(HttpClient client, bool allowBodyReplay = true) : this(
            new Prologue.Blank(),
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
            new Prologue.Blank(),
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
            new Prologue.Blank(),
            new List<IPair<string, string>>(),
            new MemoryStream(),
            convert.Invoke,
            allowBodyReplay
        )
        { }

        private HttpWire(
            IPrologue prologue,
            IList<IPair<string,string>> headers,
            Stream body,
            Func<HttpRequestMessage, Task<HttpResponseMessage>> convert,
            bool allowBodyReplay
            
        )
        {
            this.prologue = prologue;
            this.headers = headers;
            this.convert = convert;
            this.body = body;
            this.allowBodyReplay = allowBodyReplay;
        }

        public IWarp<IMessage> Refine(IPrologue newPrologue)
        {
            return new HttpWire(
                newPrologue, this.headers, this.body, this.convert, this.allowBodyReplay
            );
        }

        public IWarp<IMessage> Refine(IEnumerable<IPair<string, string>> newParts) =>
            this.Refine(newParts.ToArray());

        public IWarp<IMessage> Refine(params IPair<string, string>[] parts)
        {
            foreach (var part in parts)
                this.headers.Add(part);
            
            return new HttpWire(this.prologue, this.headers,this.body,  this.convert, this.allowBodyReplay);
        }

        public IWarp<IMessage> Refine(Stream newBody)
        {
            return new HttpWire(this.prologue, this.headers, newBody, convert, this.allowBodyReplay);
        }

        public async Task<IMessage> Render()
        {
            var request = RequestMessage(this.prologue);
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
                    new AsPrologue(
                        [
                            $"HTTP/{aspResponse.Version}",
                            aspResponse.StatusCode.ToString(),
                            aspResponse.ReasonPhrase
                        ]
                    ),
                    aspResponse
                        .Headers
                        .AsMapped(header => 
                                header.Value.AsMapped(value => (header.Key, value).AsPair())
                        ).AsJoined(),
                    allowBodyReplay 
                        ? new BufferingReadStream(await aspResponse.Content.ReadAsStreamAsync())
                        : await aspResponse.Content.ReadAsStreamAsync()
                );
        }

        private static HttpRequestMessage RequestMessage(IPrologue prologue)
        {
            var result = new HttpRequestMessage();
            var pieces = prologue.Sequence();
            result.Method = new HttpMethod(pieces[0]);
            result.RequestUri = new Uri(pieces[1]);
            result.Version =
                new Version(
                    new TrimmedLeft(
                        new TrimmedRight(pieces[2], "\r\n"),
                        "HTTP/"
                    ).Str()
                );
            return result;
        }

        private static bool IsContentHeader(string header) =>
            contentHeaders.Contains(header, StringComparer.OrdinalIgnoreCase);
    }
}