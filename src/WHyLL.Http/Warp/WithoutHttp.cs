using System.Text.RegularExpressions;
using Tonga;
using Tonga.Enumerable;
using WHyLL.Message;

namespace WHyLL.Http.Warp
{
    /// <summary>
    /// A WHyLL message where all http related data is removed from.
    /// </summary>
    public sealed class WithoutHttp(string firstLine, IEnumerable<IPair<string, string>> parts, Stream body) : IWarp<IMessage>
    {
        private static readonly string[] methods =
        {
            "GET", "HEAD", "POST", "PUT", "DELETE", "CONNECT", "OPTIONS", "TRACE", "PATCH"
        };

        private static readonly string[] standardRequestHeaders =
        {
            "A-IM","Accept ","Accept-Charset","Accept-Datetime","Accept-Encoding","Accept-Language",
            "Access-Control-Request-Method","Access-Control-Request-Headers","Authorization","Cache-Control",
            "Connection", "Content-Encoding", "Content-Length", "Content-MD5", "Content-Type", "Cookie",
            "Date", "Expect", "Forwarded", "From", "Host", "HTTP2-Settings", "If-Match", "If-Modified-Since",
            "If-None-Match", "If-Range", "If-Unmodified-Since", "Max-Forwards", "Origin", "Pragma", "Prefer",
            "Proxy-Authorization", "Range", "Referer", "TE", "Trailer", "Transfer-Encoding", "User-Agent",
            "Upgrade", "Via", "Warning"
        };

        private static readonly string[] commonRequestHeaders =
        {
            "Upgrade-Insecure-Requests", "X-Requested-With", "DNT", "X-Forwarded-For", "X-Forwarded-Host",
            "X-Forwarded-Proto", "Front-End-Https", "X-Http-Method-Override", "X-ATT-DeviceId", "X-Wap-Profile",
            "Proxy-Connection", "X-UIDH", "X-Csrf-Token", "X-Request-ID", "X-Correlation-ID", "Save-Data", "Sec-GPC"
        };

        private static readonly string[] standardResponseHeaders =
        {
            "Accept-CH", "Access-Control-Allow-Origin", "Access-Control-Allow-Credentials", "Access-Control-Expose-Headers",
            "Access-Control-Max-Age", "Access-Control-Allow-Methods", "Access-Control-Allow-Headers", "Accept-Patch",
            "Accept-Ranges", "Age", "Allow", "Alt-Svc", "Cache-Control", "Connection", "Content-Disposition",
            "Content-Encoding", "Content-Language", "Content-Length", "Content-Location", "Content-MD5",
            "Content-Range", "Content-Type", "Date", "Delta-Base", "ETag", "Expires", "IM", "Last-Modified",
            "Link", "Location", "P3P", "Pragma", "Preference-Applied", "Proxy-Authenticate", "Public-Key-Pins",
            "Retry-After", "Server", "Set-Cookie", "Strict-Transport-Security", "Trailer", "Transfer-Encoding",
            "Tk", "Upgrade", "Vary", "Via", "Warning", "WWW-Authenticate", "X-Frame-Options"
        };
        private static readonly string[] commonResponseHeaders =
        {
            "Content-Security-Policy", "X-Content-Security-Policy", "X-WebKit-CSP", "Expect-CT", "NEL",
            "Permissions-Policy", "Refresh","Report-To", "Status", "Timing-Allow-Origin", "X-Content-Duration",
            "X-Content-Type-Options", "X-Powered-By", "X-Redirect-By", "X-Request-ID", "X-Correlation-ID",
            "X-UA-Compatible", "X-XSS-Protection"
        };

        /// <summary>
        /// A WHyLL message where all http related data is removed from.
        /// </summary>
        public WithoutHttp() : this(string.Empty, None._<IPair<string, string>>(), new MemoryStream())
        { }

        public IWarp<IMessage> Refine(string newFirstLine) =>
            new WithoutHttp(newFirstLine, parts, body);

        public IWarp<IMessage> Refine(IEnumerable<IPair<string, string>> newParts) =>
            this.Refine(parts.ToArray());

        public IWarp<IMessage> Refine(params IPair<string, string>[] header) =>
            new WithoutHttp(firstLine, Joined._(parts, header), body);

        public IWarp<IMessage> Refine(Stream body) =>
            new WithoutHttp(firstLine, parts, body);

        public Task<IMessage> Render() =>
            Task.Run<IMessage>(() =>
                new SimpleMessage(
                    IsHttpRequestLine(firstLine) ? String.Empty : firstLine,
                    Filtered._(
                        part => !IsHttpHeader(part),
                        parts
                    ),
                    body
                )
            );

        private static bool IsHttpHeader(IPair<string, string> part)
        {
            var key = part.Key();
            return standardRequestHeaders.Contains(key, StringComparer.InvariantCultureIgnoreCase)
                || standardResponseHeaders.Contains(key, StringComparer.InvariantCultureIgnoreCase)
                || commonRequestHeaders.Contains(key, StringComparer.InvariantCultureIgnoreCase)
                || commonResponseHeaders.Contains(key, StringComparer.InvariantCultureIgnoreCase);
        }

        private static bool IsHttpRequestLine(string firstLine)
        {
            var pattern = "\\s+([^?\\s]+)((?:[?&][^&\\s]+)*)\\s+(HTTP/.*)";
            var isHttp = false;
            foreach (var method in WithoutHttp.methods)
            {
                isHttp = Regex.IsMatch(firstLine, $"{method}{pattern}");
                if (isHttp) break;
            }
            return isHttp;
        }
    }
}

